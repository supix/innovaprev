using DomainModel.Classes.Frames;
using DomainModel.Classes.Products.Visitors;

namespace DomainModel.Classes.Materials
{
    public abstract class NeedsLockMaterial(long height_mm, long width_mm, string openingType, bool opaqueGlass, bool wireCover, IFrame frameType) : DoubleDimMaterial(height_mm, width_mm, openingType, opaqueGlass, wireCover, frameType)
    {
        public override decimal GetPrice(IMaterialVisitor visitor)
        {
            return base.GetPrice(visitor) + 616M;
        }
    }
}
