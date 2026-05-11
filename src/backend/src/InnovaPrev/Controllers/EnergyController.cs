using DomainModel.Services.EnergyReport;
using Microsoft.AspNetCore.Mvc;

namespace InnovaPrev.Controllers
{
    [Route("api/energy")]
    [ApiController]
    public class EnergyController : ControllerBase
    {
        private readonly IEnergyDatasetProvider datasetProvider;
        private readonly IEnergySavingsCalculator savingsCalculator;

        public EnergyController(IEnergyDatasetProvider datasetProvider, IEnergySavingsCalculator savingsCalculator)
        {
            this.datasetProvider = datasetProvider ?? throw new ArgumentNullException(nameof(datasetProvider));
            this.savingsCalculator = savingsCalculator ?? throw new ArgumentNullException(nameof(savingsCalculator));
        }

        [HttpGet("collections")]
        public EnergyCollections GetCollections()
        {
            return datasetProvider.GetCollections();
        }

        [HttpGet("municipalities")]
        public IReadOnlyList<EnergyMunicipality> GetMunicipalities([FromQuery] string? search, [FromQuery] int take = 50)
        {
            return datasetProvider.SearchMunicipalities(search, take);
        }

        [HttpPost("calculate")]
        public ActionResult<EnergyCalculationResult> Calculate([FromBody] EnergyCalculationInput input)
        {
            try
            {
                return Ok(savingsCalculator.Calculate(input));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }
}
