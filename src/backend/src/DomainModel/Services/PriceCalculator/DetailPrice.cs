namespace DomainModel.Services.PriceCalculator
{
    public class DetailPrice
    {
        public decimal UnitPrice { get; set; }
        public decimal NetPrice { get; set; } 
        public decimal Vat { get; set; }
        public decimal Tax
        {
            get
            {
                return NetPrice * Vat;
            }
        }
        public decimal TotalPrice
        {
            get
            { 
                return NetPrice + Tax;
            } 
        }
    }
}