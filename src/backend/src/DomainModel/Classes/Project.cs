namespace DomainModel.Classes
{
    public partial class Project
    {
        public required CompanyData SupplierData { get; set; }
        public required CompanyData CustomerData { get; set; }
        public required ProductData ProductData { get; set; }
        public required WindowsData[] WindowsData { get; set; }
        public required CustomData[] CustomData { get; set; }
        public string? logoDataUrl { get; set; }
        public int? DiscountPercentage { get; set; }
        public string[]? SalesConditions { get; set; }
    }
}