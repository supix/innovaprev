namespace DomainModel.Classes
{
    public partial class ProductData
    {
        public string? OrderNumber { get; set; }
        public required string Product { get; set; }
        public required string InternalColor { get; set; }
        public required string ExternalColor { get; set; }
        public required string AccessoryColor { get; set; }
        public required string ClimateZone { get; set; }
        public string? Notes { get; set; }
    }
}