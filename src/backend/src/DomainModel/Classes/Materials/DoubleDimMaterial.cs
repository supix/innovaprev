namespace DomainModel.Classes.Materials
{
    public abstract class DoubleDimMaterial : Material
    {
        protected DoubleDimMaterial(long height_mm, long width_mm)
        {
            this.Height_mm = height_mm;
            this.Width_mm = width_mm;
        }

        public override int NumberOfDimensions => 2;
        public long Height_mm { get; set; }
        public long Width_mm { get; set; }
        public override sealed long DimensionToQuote
        {
            get
            {
                var area_smm = Height_mm * Width_mm;
                return area_smm >= this.ClampMinValue ? area_smm : this.ClampMinValue;
            }
        }
    }
}
