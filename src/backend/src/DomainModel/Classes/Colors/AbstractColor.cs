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
                      (p.GetType().IsAssignableTo(typeof(WoodAbstractProduct)) && GetType().IsAssignableTo(typeof(WoodColor))) ||
                      (p.GetType().IsAssignableTo(typeof(PvcAbstractProduct)) && GetType().IsAssignableTo(typeof(PvcColor)))
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
                      (p.GetType().IsAssignableTo(typeof(WoodAbstractProduct)) && GetType().IsAssignableTo(typeof(AluminumColor))) ||
                      (p.GetType().IsAssignableTo(typeof(PvcAbstractProduct)) && GetType().IsAssignableTo(typeof(PvcColor)))
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
