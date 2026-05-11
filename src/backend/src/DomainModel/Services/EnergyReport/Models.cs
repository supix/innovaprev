namespace DomainModel.Services.EnergyReport
{
    public class EnergyCollections
    {
        public required EnergyFuelOption[] Fuels { get; set; }
        public required EnergyDeductionOption[] Deductions { get; set; }
        public required EnergyOption[] BuildingTypes { get; set; }
        public required EnergyOption[] ExposureTypes { get; set; }
        public required EnergyWindowOption[] OldFrameTypes { get; set; }
        public required EnergyWindowOption[] OldGlassTypes { get; set; }
        public required EnergyRatioOption[] FrameAreaRatios { get; set; }
        public required EnergyOption[] PermeabilityClasses { get; set; }
        public required EnergyOption[] ShadingOptions { get; set; }
    }

    public class EnergyOption
    {
        public required string Id { get; set; }
        public required string Label { get; set; }
        public decimal? Factor { get; set; }
    }

    public class EnergyFuelOption
    {
        public required string Id { get; set; }
        public required string Label { get; set; }
        public string? Unit { get; set; }
        public decimal Pci { get; set; }
        public decimal PricePerUnit { get; set; }
        public string? PriceLabel { get; set; }
        public decimal? ExtraCoefficient { get; set; }
    }

    public class EnergyDeductionOption
    {
        public required string Id { get; set; }
        public required string Label { get; set; }
        public decimal Percentage { get; set; }
        public decimal MaxExpense { get; set; }
        public bool IsApplicable { get; set; }
    }

    public class EnergyWindowOption
    {
        public required string Id { get; set; }
        public required string Label { get; set; }
        public decimal Uw { get; set; }
        public string? Unit { get; set; }
    }

    public class EnergyRatioOption
    {
        public required string Id { get; set; }
        public required string Label { get; set; }
        public decimal Ratio { get; set; }
    }

    public class EnergyMunicipality
    {
        public required string Id { get; set; }
        public required string Comune { get; set; }
        public string? Cap { get; set; }
        public string? Provincia { get; set; }
        public string? Regione { get; set; }
        public int? AltitudineSlm { get; set; }
        public int? GradiGiorno { get; set; }
        public string? ZonaClimatica { get; set; }
        public string? ZonaVento { get; set; }
    }

    public class EnergyCalculationInput
    {
        public string? MunicipalityId { get; set; }
        public string? FuelId { get; set; }
        public string? DeductionId { get; set; }
        public string? BuildingTypeId { get; set; }
        public string? ExposureTypeId { get; set; }
        public string? OldFrameTypeId { get; set; }
        public string? OldGlassTypeId { get; set; }
        public decimal? FrameAreaRatio { get; set; }
        public decimal? OldWindowUw { get; set; }
        public decimal? NewWindowUw { get; set; }
        public decimal? WindowSurfaceSqm { get; set; }
        public decimal? InvestmentAmount { get; set; }
        public decimal? FuelPciOverride { get; set; }
        public decimal? FuelUnitCostOverride { get; set; }
        public decimal? DeductionPercentageOverride { get; set; }
        public decimal? DeductionMaxExpenseOverride { get; set; }
        public int? DegreeDaysOverride { get; set; }
        public Classes.WindowsData[]? WindowsData { get; set; }
    }

    public class EnergyCalculationResult
    {
        public required string MunicipalityLabel { get; set; }
        public string? ClimateZone { get; set; }
        public int DegreeDays { get; set; }
        public required string FuelLabel { get; set; }
        public required string BuildingTypeLabel { get; set; }
        public required string ExposureTypeLabel { get; set; }
        public decimal WindowSurfaceSqm { get; set; }
        public decimal InvestmentAmount { get; set; }
        public decimal OldWindowUw { get; set; }
        public decimal NewWindowUw { get; set; }
        public decimal DeltaUw { get; set; }
        public decimal AnnualDispersionSavedKwh { get; set; }
        public decimal AnnualPrimaryEnergySavedKwh { get; set; }
        public decimal AnnualEconomicSaving { get; set; }
        public decimal AnnualCo2SavedKg { get; set; }
        public decimal DeductionPercentage { get; set; }
        public decimal DeductionTotal { get; set; }
        public decimal AnnualDeductionQuota { get; set; }
        public decimal? PaybackYearsWithoutDeduction { get; set; }
        public decimal? PaybackYearsWithDeduction { get; set; }
    }
}
