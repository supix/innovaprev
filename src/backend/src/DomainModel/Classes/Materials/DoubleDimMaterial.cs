using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Materials
{
    public abstract class DoubleDimMaterial : AbstractMaterial
    {
        protected DoubleDimMaterial(long height_mm, long width_mm)
        {
            Height_mm = height_mm;
            Width_mm = width_mm;
        }

        public override sealed int NumberOfDimensions => 2;
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
        public long GetAllowedArea_sqmm
        {
            get
            {
                var netArea_sqmm = Height_mm * Width_mm;
                if (ClampMinValue.HasValue && netArea_sqmm < ClampMinValue.Value)
                    return ClampMinValue.Value;
                else
                    return netArea_sqmm;
            }
        }
        public override decimal GetPrice(IVisitor visitor)
        {
            return visitor.GetPrice_DoubleDim(this, GetAllowedArea_sqmm);
        }
    }
}
