namespace InnovaPrev.DomainModel
{
    public partial class WindowsData
    {
        public long Position { get; set; }
        public long Height { get; set; }
        public long Width { get; set; }
        public long Quantity { get; set; }
        public required string WindowType { get; set; }
        public required string OpeningType { get; set; }
        public required string GlassType { get; set; }
        public required string Crosspiece { get; set; }
        public long LeftTrim { get; set; }
        public long RightTrim { get; set; }
        public long UpperTrim { get; set; }
        public long BelowThreshold { get; set; }
    }
}