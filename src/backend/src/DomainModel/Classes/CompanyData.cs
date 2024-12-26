namespace DomainModel.Classes
{
    public partial class CompanyData
    {
        public required string CompanyName { get; set; }
        public string? Address { get; set; }
        public string? taxCode { get; set; }
        public string? Phone { get; set; }
        public string? Mail { get; set; }
        public string? Iban { get; set; }
    }
}