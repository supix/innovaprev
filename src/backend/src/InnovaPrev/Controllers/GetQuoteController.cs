using DomainModel.Classes;
using Microsoft.AspNetCore.Mvc;

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
            var total_cost = project.WindowsData.Aggregate(0d, (acc, x) => 
            {
                var area_sqm = x.Height * x.Width / 1e6;
                if (area_sqm < 1.5d)
                    area_sqm = 1.5d;
                var total_area = area_sqm * x.Quantity;
                acc += 985 * total_area;
                return acc;
            });

            return new GetQuoteOutputDto()
            {
                Quotation = new Quotation()
                {
                    Amount = Math.Round(total_cost, 2)
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
        public double Amount { get; set; }
    }
}
