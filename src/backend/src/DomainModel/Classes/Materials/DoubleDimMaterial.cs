namespace DomainModel.Classes.Materials
{
    public abstract class DoubleDimMaterial : AbstractMaterial
    {
        protected DoubleDimMaterial(long height_mm, long width_mm)
        {
            Height_mm = height_mm;
            Width_mm = width_mm;
        }

        public override int NumberOfDimensions => 2;
        public long Height_mm { get; set; }
        public long Width_mm { get; set; }
        public override sealed long DimensionToQuote
        {
            get
            {
                var area_smm = Height_mm * Width_mm;
                if (!ClampMinValue.HasValue)
                    return area_smm;
                return area_smm >= ClampMinValue.Value ? area_smm : ClampMinValue.Value;
            }
        }
        public override sealed decimal GetAllowedArea_sqm {
            get {
                var netArea_sqmm = Height_mm * Width_mm;
                if (ClampMinValue.HasValue && netArea_sqmm < ClampMinValue.Value)
                    return ClampMinValue.Value / 1e6M;
                else
                    return netArea_sqmm / 1e6M;
            }
        }
        public override sealed decimal GetAllowedLength_m => throw new InvalidOperationException($"Cannot compute length for a double dimension material. Code: {Code}");
    }
}
