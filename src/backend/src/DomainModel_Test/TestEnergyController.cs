using DomainModel.Services.EnergyReport;
using InnovaPrev.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DomainModel_Test
{
    public class TestEnergyController
    {
        private EnergyController controller = null!;

        [SetUp]
        public void Setup()
        {
            var datasetProvider = new EnergyDatasetProviderCsv();
            var calculator = new EnergySavingsCalculator(datasetProvider);
            controller = new EnergyController(datasetProvider, calculator);
        }

        [Test]
        public void Test_GetCollections_ReturnsEnergyCollectionsPayload()
        {
            var result = controller.GetCollections();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Fuels.Any(x => x.Id == "4"), Is.True);
            Assert.That(result.Deductions.Any(x => x.Id == "ecobonus_50pct"), Is.True);
            Assert.That(result.OldFrameTypes, Is.Not.Empty);
        }

        [Test]
        public void Test_GetMunicipalities_ReturnsFilteredResults()
        {
            var result = controller.GetMunicipalities("torino", 5);

            Assert.That(result, Is.Not.Empty);
            Assert.That(result[0].Comune, Is.EqualTo("Torino"));
            Assert.That(result.Count, Is.LessThanOrEqualTo(5));
        }

        [Test]
        public void Test_Calculate_ReturnsOkForValidPayload()
        {
            var response = controller.Calculate(new EnergyCalculationInput
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

            Assert.That(response.Result, Is.InstanceOf<OkObjectResult>());
            var ok = (OkObjectResult)response.Result!;
            var payload = ok.Value as EnergyCalculationResult;
            Assert.That(payload, Is.Not.Null);
            Assert.That(payload!.MunicipalityLabel, Does.Contain("Torino"));
            Assert.That(payload.PaybackYearsWithDeduction, Is.LessThan(payload.PaybackYearsWithoutDeduction));
        }

        [Test]
        public void Test_Calculate_ReturnsBadRequestForInvalidPayload()
        {
            var response = controller.Calculate(new EnergyCalculationInput
            {
                FuelId = "4",
                NewWindowUw = 1.2M,
                WindowSurfaceSqm = 12M,
                InvestmentAmount = 10000M
            });

            Assert.That(response.Result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequest = (BadRequestObjectResult)response.Result!;
            Assert.That(badRequest.Value, Is.Not.Null);
            var message = badRequest.Value!.GetType().GetProperty("message")?.GetValue(badRequest.Value)?.ToString();
            Assert.That(message, Is.Not.Null.And.Contains("comune").IgnoreCase);
        }
    }
}
