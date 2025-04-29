using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Materials
{
    public abstract class NeedsLockMaterial(long height_mm, long width_mm, string openingType, bool opaqueGlass, bool wireCover) : DoubleDimMaterial(height_mm, width_mm, openingType, opaqueGlass, wireCover)
    {
        public override decimal GetPrice(IMaterialVisitor visitor)
        {
            return visitor.GetPrice_DoubleDim(this, GetAllowedArea_sqmm) + 616M;
        }
    }
}
