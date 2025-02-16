using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Materials
{
    public abstract class DoubleDimFixedMaterial : DoubleDimMaterial
    {
        protected DoubleDimFixedMaterial(long height_mm, long width_mm) : base(height_mm, width_mm)
        {
        }
        public override decimal GetPrice(IVisitor visitor)
        {
            return visitor.GetPrice_DoubleDimFixed(this, GetAllowedArea_sqmm);
        }
    }
}
