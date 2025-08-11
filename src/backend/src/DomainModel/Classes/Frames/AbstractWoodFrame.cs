using DomainModel.Classes.Products;

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
    }
}
