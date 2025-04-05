using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Materials
{
    public abstract class NeedsLockMaterial(long height_mm, long width_mm, bool opaqueGlass) : DoubleDimMaterial(height_mm, width_mm, opaqueGlass)
    {
        public override decimal GetPrice(IMaterialVisitor visitor)
        {
            return visitor.GetPrice_DoubleDim(this, GetAllowedArea_sqmm) + 616M;
        }
    }
}
