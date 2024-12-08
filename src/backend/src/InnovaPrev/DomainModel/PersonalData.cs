namespace InnovaPrev.DomainModel
{
    public partial class PersonalData
    {
        public required string CompanyName { get; set; }
        public required string Address { get; set; }
        public required string Vat { get; set; }
        public required string Phone { get; set; }
        public required string Mail { get; set; }
        public required string OrderNumber { get; set; }
    }
}