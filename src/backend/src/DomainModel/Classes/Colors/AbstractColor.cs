using DomainModel.Classes.Products;
using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Colors
{
    public abstract class AbstractColor : IColor
    {
        public string Code => GetType().Name;
        public abstract string Description { get; }
        public abstract decimal Price_sqm { get; }
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

        public abstract decimal GetPrice_CAS_m(IColorVisitor visitor);
        public abstract decimal GetPrice_COP_m(IColorVisitor visitor);
        public abstract decimal GetPrice_FRO_m(IColorVisitor visitor);
    }
}
