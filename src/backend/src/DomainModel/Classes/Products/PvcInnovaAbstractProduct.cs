using DomainModel.Classes.Colors;

namespace DomainModel.Classes.Products
{
    public abstract class PvcInnovaAbstractProduct(IColor color) : PvcAbstractProduct(color)
    {
        public override decimal GetPrice_Z3EgdesLThreshold_sqm()
        {
            throw new InvalidOperationException("Frame not allowed for this product");
        }
        public override decimal GetPrice_AbstractWoodFrame_sqm()
        {
            throw new InvalidOperationException("Frame not allowed for this product");
        }
        public override decimal GetPrice_AbstractInnovaFrame_sqm()
        {
            return 0;
        }
        public override decimal GetPrice_NullFrame_sqm()
        {
            throw new InvalidOperationException("Frame not allowed for this product");
        }
    }
}
