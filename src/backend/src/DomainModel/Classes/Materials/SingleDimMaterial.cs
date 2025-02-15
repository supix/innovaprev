namespace DomainModel.Classes.Materials
{
    public abstract class SingleDimMaterial : AbstractMaterial
    {
        public SingleDimMaterial(long length_mm)
        {
            Length_mm = length_mm;
        }

        public override sealed int NumberOfDimensions => 1;
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
        public override sealed decimal GetAllowedArea_sqm => throw new InvalidOperationException($"Cannot compute area for a single dimension material. Code: {Code}");
        public override sealed decimal GetAllowedLength_m
        {
            get
            {
                if (ClampMinValue.HasValue && Length_mm < ClampMinValue.Value)
                    return ClampMinValue.Value / 1e3M;
                else
                    return Length_mm / 1e3M;
            }
        }
    }
}
