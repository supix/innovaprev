namespace InnovaPrev.DomainModel
{
    public partial class ProductData
    {
        public required string Product { get; set; }
        public bool GlassStopper { get; set; }
        public bool WindowSlide { get; set; }
        public required string InternalColor { get; set; }
        public required string ExternalColor { get; set; }
        public required string AccessoryColor { get; set; }
        public required string ClimateZone { get; set; }
        public required string Notes { get; set; }
    }
}