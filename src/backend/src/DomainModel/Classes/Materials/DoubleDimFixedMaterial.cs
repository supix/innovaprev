using DomainModel.Classes.Frames;
using DomainModel.Classes.Products.Visitors;

namespace DomainModel.Classes.Materials
{
    public abstract class DoubleDimFixedMaterial : DoubleDimMaterial
    {
        protected DoubleDimFixedMaterial(long height_mm, long width_mm, string openingType, bool opaqueGlass, bool wireCover, IFrame frameType) : base(height_mm, width_mm, openingType, opaqueGlass, wireCover, frameType)
        {
        }
        public override decimal GetPrice(IMaterialVisitor visitor)
        {
            return visitor.GetPrice_DoubleDimFixed(this, GetAllowedArea_sqmm);
        }
        public override sealed bool openingTypeVisible => false;
        public override string GetGlassDescription(IMaterialVisitor product)
        {
            return base.GetGlassDescriptionFixedAndAntaMax(product);
        }
    }
}
