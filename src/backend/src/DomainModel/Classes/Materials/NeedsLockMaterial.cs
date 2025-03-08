using DomainModel.Classes.Products;
using DomainModel.Classes.Products.ConcreteProducts;
using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Materials
{
    public abstract class NeedsLockMaterial(long height_mm, long width_mm) : DoubleDimMaterial(height_mm, width_mm)
    {
        public override decimal GetPrice(IVisitor visitor)
        {
            return visitor.GetPrice_DoubleDim(this, GetAllowedArea_sqmm) + 616M;
        }
    }
}
