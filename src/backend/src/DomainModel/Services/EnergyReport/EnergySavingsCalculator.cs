namespace DomainModel.Services.EnergyReport
{
    internal class EnergySavingsCalculator : IEnergySavingsCalculator
    {
        private const decimal PlantEfficiency = 0.8M;
        private const decimal Co2FactorKgPerKwh = 0.216M;
        private const int DeductionYears = 10;

        private readonly IEnergyDatasetProvider datasetProvider;

        public EnergySavingsCalculator(IEnergyDatasetProvider datasetProvider)
        {
            this.datasetProvider = datasetProvider ?? throw new ArgumentNullException(nameof(datasetProvider));
        }

        public EnergyCalculationResult Calculate(EnergyCalculationInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            var municipality = ResolveMunicipality(input);
            var fuel = ResolveFuel(input);
            var buildingType = ResolveOption(input.BuildingTypeId, datasetProvider.GetBuildingTypeById, "residenziale", "tipologia edificio");
            var exposureType = ResolveOption(input.ExposureTypeId, datasetProvider.GetExposureTypeById, "verso_esterno", "esposizione");

            var windowSurfaceSqm = ResolveWindowSurface(input);
            var investmentAmount = RequirePositive(input.InvestmentAmount, "importo investimento");
            var oldWindowUw = ResolveOldWindowUw(input);
            var newWindowUw = RequirePositive(input.NewWindowUw, "trasmittanza nuovi infissi");
            var deltaUw = Math.Max(0M, oldWindowUw - newWindowUw);
            var degreeDays = input.DegreeDaysOverride ?? municipality.GradiGiorno
                ?? throw new InvalidOperationException("Gradi giorno non disponibili per il comune selezionato.");

            var buildingFactor = buildingType.Factor ?? 1M;
            var exposureFactor = exposureType.Factor ?? 1M;
            var annualDispersionSavedKwh = degreeDays * 24M * buildingFactor * exposureFactor * deltaUw * windowSurfaceSqm / 1000M;
            var annualPrimaryEnergySavedKwh = annualDispersionSavedKwh / PlantEfficiency;

            var fuelPci = input.FuelPciOverride.GetValueOrDefault(fuel.Pci);
            var fuelUnitCost = input.FuelUnitCostOverride.GetValueOrDefault(fuel.PricePerUnit);
            if (fuelPci <= 0M)
            {
                throw new InvalidOperationException("Il PCI del combustibile deve essere maggiore di zero.");
            }

            var annualEconomicSaving = (annualPrimaryEnergySavedKwh / fuelPci) * fuelUnitCost;
            var annualCo2SavedKg = annualPrimaryEnergySavedKwh * Co2FactorKgPerKwh;

            var deduction = string.IsNullOrWhiteSpace(input.DeductionId) ? null : datasetProvider.GetDeductionById(input.DeductionId);
            var deductionPercentage = input.DeductionPercentageOverride
                ?? (deduction?.IsApplicable == true ? deduction.Percentage : 0M);
            var deductionMaxExpense = input.DeductionMaxExpenseOverride
                ?? (deduction?.IsApplicable == true ? deduction.MaxExpense : 0M);
            var deductionBase = deductionMaxExpense > 0M ? Math.Min(investmentAmount, deductionMaxExpense) : 0M;
            var deductionTotal = deductionPercentage > 0M ? deductionBase * deductionPercentage / 100M : 0M;
            var annualDeductionQuota = deductionTotal > 0M ? deductionTotal / DeductionYears : 0M;

            return new EnergyCalculationResult
            {
                MunicipalityLabel = municipality.Provincia is null
                    ? municipality.Comune
                    : $"{municipality.Comune} ({municipality.Provincia})",
                ClimateZone = municipality.ZonaClimatica,
                DegreeDays = degreeDays,
                FuelLabel = fuel.Label,
                BuildingTypeLabel = buildingType.Label,
                ExposureTypeLabel = exposureType.Label,
                WindowSurfaceSqm = Round(windowSurfaceSqm),
                InvestmentAmount = Round(investmentAmount),
                OldWindowUw = Round(oldWindowUw, 3),
                NewWindowUw = Round(newWindowUw, 3),
                DeltaUw = Round(deltaUw, 3),
                AnnualDispersionSavedKwh = Round(annualDispersionSavedKwh),
                AnnualPrimaryEnergySavedKwh = Round(annualPrimaryEnergySavedKwh),
                AnnualEconomicSaving = Round(annualEconomicSaving),
                AnnualCo2SavedKg = Round(annualCo2SavedKg),
                DeductionPercentage = Round(deductionPercentage, 2),
                DeductionTotal = Round(deductionTotal),
                AnnualDeductionQuota = Round(annualDeductionQuota),
                PaybackYearsWithoutDeduction = CalculatePayback(investmentAmount, annualEconomicSaving),
                PaybackYearsWithDeduction = CalculatePayback(investmentAmount, annualEconomicSaving + annualDeductionQuota)
            };
        }

        private EnergyMunicipality ResolveMunicipality(EnergyCalculationInput input)
        {
            if (string.IsNullOrWhiteSpace(input.MunicipalityId))
            {
                throw new InvalidOperationException("Il comune è obbligatorio.");
            }

            return datasetProvider.GetMunicipalityById(input.MunicipalityId)
                ?? throw new InvalidOperationException("Comune non trovato.");
        }

        private EnergyFuelOption ResolveFuel(EnergyCalculationInput input)
        {
            if (string.IsNullOrWhiteSpace(input.FuelId))
            {
                throw new InvalidOperationException("Il combustibile è obbligatorio.");
            }

            return datasetProvider.GetFuelById(input.FuelId)
                ?? throw new InvalidOperationException("Combustibile non trovato.");
        }

        private EnergyOption ResolveOption(string? id, Func<string, EnergyOption?> resolver, string fallbackId, string label)
        {
            var resolved = resolver(id ?? fallbackId) ?? resolver(fallbackId);
            return resolved ?? throw new InvalidOperationException($"Impossibile risolvere {label}.");
        }

        private decimal ResolveWindowSurface(EnergyCalculationInput input)
        {
            if (input.WindowSurfaceSqm is > 0M)
            {
                return input.WindowSurfaceSqm.Value;
            }

            if (input.WindowsData is null || input.WindowsData.Length == 0)
            {
                throw new InvalidOperationException("La superficie dell'intervento è obbligatoria.");
            }

            var surface = input.WindowsData
                .Where(item => item.Height > 0 && item.Width > 0 && item.Quantity > 0)
                .Sum(item => item.Height * item.Width * item.Quantity / 1_000_000M);

            if (surface <= 0M)
            {
                throw new InvalidOperationException("La superficie dell'intervento deve essere maggiore di zero.");
            }

            return surface;
        }

        private decimal ResolveOldWindowUw(EnergyCalculationInput input)
        {
            if (input.OldWindowUw is > 0M)
            {
                return input.OldWindowUw.Value;
            }

            if (string.IsNullOrWhiteSpace(input.OldFrameTypeId) ||
                string.IsNullOrWhiteSpace(input.OldGlassTypeId) ||
                input.FrameAreaRatio is null)
            {
                throw new InvalidOperationException("Servono i dati del vecchio infisso oppure una trasmittanza ante operam manuale.");
            }

            var frame = datasetProvider.GetOldFrameTypeById(input.OldFrameTypeId)
                ?? throw new InvalidOperationException("Tipo telaio esistente non trovato.");
            var glass = datasetProvider.GetOldGlassTypeById(input.OldGlassTypeId)
                ?? throw new InvalidOperationException("Tipo vetrazione esistente non trovato.");

            var frameAreaRatio = input.FrameAreaRatio.Value;
            if (frameAreaRatio <= 0M || frameAreaRatio >= 1M)
            {
                throw new InvalidOperationException("La percentuale area telaio deve essere compresa tra 0 e 1.");
            }

            return (frame.Uw * frameAreaRatio) + (glass.Uw * (1M - frameAreaRatio));
        }

        private static decimal RequirePositive(decimal? value, string label)
        {
            if (value is null || value <= 0M)
            {
                throw new InvalidOperationException($"Il campo '{label}' deve essere maggiore di zero.");
            }

            return value.Value;
        }

        private static decimal? CalculatePayback(decimal investmentAmount, decimal annualBenefit)
        {
            if (annualBenefit <= 0M)
            {
                return null;
            }

            return Round(investmentAmount / annualBenefit, 1);
        }

        private static decimal Round(decimal value, int decimals = 2)
        {
            return Math.Round(value, decimals, MidpointRounding.AwayFromZero);
        }
    }
}
