using DomainModel.Classes.Products;
using DomainModel.Classes.Products.Visitors;

namespace DomainModel.Classes.Frames
{
    public abstract class AbstractInnovaFrame : AbstractFrame
    {
        public override sealed string[] FrameForProduct
        {
            get
            {
                return ProductFactory.GetAll()
                    .Where(p => p.GetType().IsAssignableTo(typeof(PvcInnovaAbstractProduct)))
                    .Select(p => p.Code)
                    .ToArray();
            }
        }
        public override decimal GetPrice_sqm(IFrameVisitor visitor)
        {
            return visitor.GetPrice_AbstractInnovaFrame_sqm();
        }
    }
}
