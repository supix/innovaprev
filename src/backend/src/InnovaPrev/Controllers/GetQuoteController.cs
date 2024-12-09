using DomainModel.Classes;
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
        public GetQuoteOutputDto Post([FromBody] Project project)
        {
            return new GetQuoteOutputDto()
            {
                Quotation = new Quotation()
                {
                    Amount = 12345
                }
            };
        }
    }

    public class GetQuoteOutputDto
    {
        public required Quotation Quotation { get; set; }
    }

    public class Quotation
    {
        public long Amount { get; set; }
    }
}
