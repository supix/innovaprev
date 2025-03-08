using DomainModel.Classes.Products;

namespace DomainModel.Classes.Colors
{
    public abstract class AbstractColor : IColor
    {
        public string Code => GetType().Name;
        public abstract string Description { get; }
        public abstract decimal Price_mq { get; }
        public abstract int Order { get; }
        public string[] InternalColorForProducts
        {
            get
            {
                return ProductFactory.GetAll()
                    .Where(p =>
                      (p is WoodAbstractProduct && this is WoodColor) ||
                      (p is PvcAbstractProduct && this is PvcColor)
                    )
                    .Select(p => p.Code)
                    .ToArray();
            }
        }
        public string[] ExternalColorForProducts
        {
            get
            {
                return ProductFactory.GetAll()
                    .Where(p =>
                      (p is WoodAbstractProduct && this is AluminumColor) ||
                      (p is PvcAbstractProduct && this is PvcColor)
                    )
                    .Select(p => p.Code)
                    .ToArray();
            }
        }
    }
}
