﻿namespace DomainModel.Classes
{
    public partial class WindowsData
    {
        public long Position { get; set; }
        public long Height { get; set; }
        public long Width { get; set; }
        public long Length { get; set; }
        public long Quantity { get; set; }
        public required string WindowType { get; set; }
        public string? OpeningType { get; set; }
        public string? GlassType { get; set; }
        public bool WireCover { get; set; }
        public long LeftTrim { get; set; }
        public long RightTrim { get; set; }
        public long UpperTrim { get; set; }
        public long BelowThreshold { get; set; }
    }
}