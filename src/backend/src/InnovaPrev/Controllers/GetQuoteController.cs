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
        public GetQuoteOutputDto Post([FromBody] Dto dto)
        {
            var total_cost = dto.WindowsData.Aggregate(0d, (acc, x) =>
            {
                const int sqm_price = 985;
                var area_sqm = x.Height * x.Width / 1e6;
                if (area_sqm < 1.5d)
                    area_sqm = 1.5d;
                var total_area = area_sqm * x.Quantity;
                acc += sqm_price * total_area;
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

    public class Dto
    {
        public required WindowsData[] WindowsData { get; set; }
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
