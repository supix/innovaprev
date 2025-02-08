namespace DomainModel.Classes.Materials
{
    public abstract class SingleDimMaterial : AbstractMaterial
    {
        protected SingleDimMaterial(long length_mm)
        {
            this.Length_mm = length_mm;
        }

        public override int NumberOfDimensions => 1;
        public long Length_mm { get; set; }
        public override sealed long DimensionToQuote => this.Length_mm >= this.ClampMinValue ? this.Length_mm : this.ClampMinValue;
    }
}
