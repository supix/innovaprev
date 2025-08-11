using DomainModel.Classes.Products;

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
    }
}
