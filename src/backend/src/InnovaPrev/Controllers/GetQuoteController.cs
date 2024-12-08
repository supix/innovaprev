using InnovaPrev.DomainModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InnovaPrev.Controllers
{
    [Route("api/getQuote")]
    [ApiController]
    public class GetQuoteController : ControllerBase
    {
        // POST api/<GetQuoteController>
        [HttpPost]
        public OutputDto Post([FromBody] Project project)
        {
            return new OutputDto()
            {
                Quotation = new Quotation()
                {
                    Amount = 12345
                }
            };
        }
    }

    public class OutputDto
    {
        public required Quotation Quotation { get; set; }
    }

    public class Quotation
    {
        public long Amount { get; set; }
    }
}
