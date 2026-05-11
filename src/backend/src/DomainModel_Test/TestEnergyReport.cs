using DomainModel.Classes;
using DomainModel.Services.EnergyReport;

namespace DomainModel_Test
{
    public class TestEnergyReport
    {
        private IEnergyDatasetProvider datasetProvider = null!;
        private IEnergySavingsCalculator calculator = null!;

        [SetUp]
        public void Setup()
        {
            datasetProvider = new EnergyDatasetProviderCsv();
            calculator = new EnergySavingsCalculator(datasetProvider);
        }

        [Test]
        public void Test_EnergyCollections_AreLoadedFromCsv()
        {
            var collections = datasetProvider.GetCollections();

            Assert.That(collections.Fuels.Length, Is.GreaterThanOrEqualTo(7));
            Assert.That(collections.Deductions.Any(x => x.Id == "ecobonus_50pct"), Is.True);
            Assert.That(collections.OldFrameTypes.Any(x => x.Id == "pvc_a_due_camere"), Is.True);
            Assert.That(collections.OldGlassTypes.Any(x => x.Id == "vetrata_4_12_4"), Is.True);
            Assert.That(collections.FrameAreaRatios.Select(x => x.Ratio), Does.Contain(0.2M));
            Assert.That(collections.BuildingTypes.Any(x => x.Id == "residenziale"), Is.True);
            Assert.That(collections.ExposureTypes.Any(x => x.Id == "verso_esterno"), Is.True);
        }

        [Test]
        public void Test_SearchMunicipalities_PrioritizesExactComuneMatch()
        {
            var results = datasetProvider.SearchMunicipalities("torino", 5);

            Assert.That(results, Is.Not.Empty);
            Assert.That(results[0].Comune, Is.EqualTo("Torino"));
            Assert.That(results[0].Provincia, Is.EqualTo("TO"));
            Assert.That(results[0].GradiGiorno, Is.GreaterThan(0));
        }

        [Test]
        public void Test_GetMunicipalityById_ReturnsClimateData()
        {
            var municipality = datasetProvider.GetMunicipalityById("torino__to");

            Assert.That(municipality, Is.Not.Null);
            Assert.That(municipality!.Comune, Is.EqualTo("Torino"));
            Assert.That(municipality.ZonaClimatica, Is.EqualTo("E"));
            Assert.That(municipality.GradiGiorno, Is.GreaterThan(0));
        }

        [Test]
        public void Test_Calculate_ComputesOldWindowUwFromDatasets()
        {
            var result = calculator.Calculate(new EnergyCalculationInput
            {
                MunicipalityId = "torino__to",
                FuelId = "4",
                DeductionId = "ecobonus_50pct",
                BuildingTypeId = "residenziale",
                ExposureTypeId = "verso_esterno",
                OldFrameTypeId = "pvc_a_due_camere",
                OldGlassTypeId = "vetrata_4_12_4",
                FrameAreaRatio = 0.3M,
                NewWindowUw = 1.2M,
                WindowSurfaceSqm = 12.5M,
                InvestmentAmount = 15000M
            });

            Assert.That(result.OldWindowUw, Is.EqualTo(2.69M).Within(0.001M));
            Assert.That(result.DeltaUw, Is.EqualTo(1.49M).Within(0.001M));
            Assert.That(result.AnnualPrimaryEnergySavedKwh, Is.GreaterThan(1000M));
            Assert.That(result.AnnualEconomicSaving, Is.GreaterThan(100M));
        }

        [Test]
        public void Test_Calculate_UsesWindowRowsToInferSurface()
        {
            var result = calculator.Calculate(new EnergyCalculationInput
            {
                MunicipalityId = "torino__to",
                FuelId = "4",
                DeductionId = "ecobonus_50pct",
                BuildingTypeId = "residenziale",
                ExposureTypeId = "verso_esterno",
                OldWindowUw = 3.0M,
                NewWindowUw = 1.2M,
                InvestmentAmount = 9000M,
                WindowsData = new[]
                {
                    new WindowsData
                    {
                        Position = 1,
                        Height = 1500,
                        Width = 1200,
                        Length = 0,
                        Quantity = 2,
                        WindowType = "F1A"
                    }
                }
            });

            Assert.That(result.WindowSurfaceSqm, Is.EqualTo(3.6M).Within(0.01M));
            Assert.That(result.PaybackYearsWithoutDeduction, Is.Not.Null);
            Assert.That(result.PaybackYearsWithDeduction, Is.Not.Null);
        }

        [Test]
        public void Test_Calculate_WithDeduction_HasBetterPaybackThanWithout()
        {
            var result = calculator.Calculate(new EnergyCalculationInput
            {
                MunicipalityId = "torino__to",
                FuelId = "4",
                DeductionId = "ecobonus_50pct",
                BuildingTypeId = "residenziale",
                ExposureTypeId = "verso_esterno",
                OldWindowUw = 3.0M,
                NewWindowUw = 1.2M,
                WindowSurfaceSqm = 14M,
                InvestmentAmount = 12000M
            });

            Assert.That(result.DeductionTotal, Is.EqualTo(6000M).Within(0.01M));
            Assert.That(result.AnnualDeductionQuota, Is.EqualTo(600M).Within(0.01M));
            Assert.That(result.PaybackYearsWithDeduction, Is.LessThan(result.PaybackYearsWithoutDeduction));
        }

        [Test]
        public void Test_Calculate_ThrowsWhenMunicipalityIsMissing()
        {
            var exception = Assert.Throws<InvalidOperationException>(() => calculator.Calculate(new EnergyCalculationInput
            {
                FuelId = "4",
                OldWindowUw = 3M,
                NewWindowUw = 1.2M,
                WindowSurfaceSqm = 10M,
                InvestmentAmount = 10000M
            }));

            Assert.That(exception!.Message, Does.Contain("comune").IgnoreCase);
        }
    }
}
