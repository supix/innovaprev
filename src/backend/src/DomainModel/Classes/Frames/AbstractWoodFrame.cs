using DomainModel.Classes.Products;
using DomainModel.Classes.Products.Visitors;

namespace DomainModel.Classes.Frames
{
    public abstract class AbstractWoodFrame : AbstractFrame
    {
        public override sealed string[] FrameForProduct
        {
            get
            {
                return ProductFactory.GetAll()
                    .Where(p => p.GetType().IsAssignableTo(typeof(WoodAbstractProduct)))
                    .Select(p => p.Code)
                    .ToArray();
            }
        }
        public override decimal GetPrice_sqm(IFrameVisitor visitor)
        {
            return visitor.GetPrice_AbstractWoodFrame_sqm();
        }
    }
}
