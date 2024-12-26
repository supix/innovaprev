namespace DomainModel.Services.PriceCalculator
{
    public class DetailPrice
    {
        public decimal NetPrice { get; set; } 
        public decimal Vat { get; set; }

        public decimal Tax {
            get
            {
                return NetPrice * Vat;
            }
        }
        public decimal TotalPrice {
            get
            { 
                return Tax + NetPrice;
            } 
        }
    }
}