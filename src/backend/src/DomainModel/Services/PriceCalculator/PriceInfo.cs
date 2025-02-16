namespace DomainModel.Services.PriceCalculator
{
    public class PriceInfo
    {
        public decimal Total { get; set; }
        public decimal Tax { get; set; }
        public decimal GrandTotal { get; set; }
        public required IList<DetailPrice> DetailPrices { get; set; }
    }
}