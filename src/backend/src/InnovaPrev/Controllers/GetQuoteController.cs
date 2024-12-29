using DomainModel.Classes;
using DomainModel.Services.PriceCalculator;
using Microsoft.AspNetCore.Mvc;

namespace InnovaPrev.Controllers
{
    [Route("api/getQuote")]
    [ApiController]
    public class GetQuoteController : ControllerBase
    {
        private readonly IPriceCalculator priceCalculator;

        public GetQuoteController(IPriceCalculator priceCalculator)
        {
            this.priceCalculator = priceCalculator ?? throw new ArgumentNullException(nameof(priceCalculator));
        }

        // POST api/<GetQuoteController>
        [HttpPost]
        public GetQuoteOutputDto Post([FromBody] Dto dto)
        {
            PriceInfo prices = priceCalculator.getPrices(dto.ProductData, dto.WindowsData);
            return new GetQuoteOutputDto()
            {
                Quotation = new Quotation()
                {
                    Amount = Math.Round(prices.Total, 2),
                    Tax = Math.Round(prices.Tax, 2),
                    GrandTotal = Math.Round(prices.GrandTotal, 2),
                }
            };
        }
    }

    public class Dto
    {
        public required ProductData ProductData { get; set; }
        public required WindowsData[] WindowsData { get; set; }
    }

    public class GetQuoteOutputDto
    {
        public required Quotation Quotation { get; set; }
    }

    public class Quotation
    {
        public decimal Amount { get; set; }
        public decimal Tax { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
