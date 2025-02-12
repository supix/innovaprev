namespace DomainModel.Classes.Materials
{
    public abstract class SingleDimMaterial : AbstractMaterial
    {
        public SingleDimMaterial(long length_mm)
        {
            Length_mm = length_mm;
        }

        public override int NumberOfDimensions => 1;
        public long Length_mm { get; set; }
        public override sealed long DimensionToQuote
        {
            get
            {
                if (!ClampMinValue.HasValue)
                    return Length_mm;
                return Length_mm >= ClampMinValue.Value ? Length_mm : ClampMinValue.Value;
            }
        }
        public override sealed decimal GetArea_sqm => throw new InvalidOperationException($"Cannot compute area for a single dimension material. Code: {Code}");
        public override sealed decimal GetLength_m => Length_mm / 1e3M;
    }
}
