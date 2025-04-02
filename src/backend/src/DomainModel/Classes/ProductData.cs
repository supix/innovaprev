namespace DomainModel.Classes
{
    public partial class ProductData
    {
        private string? externalColor;
        public string? OrderNumber { get; set; }
        public required string Product { get; set; }
        public required string InternalColor { get; set; }
        public string? ExternalColor { get { return externalColor ?? InternalColor; } set { externalColor = value; } }
        public required string AccessoryColor { get; set; }
        public string? Notes { get; set; }
    }
}