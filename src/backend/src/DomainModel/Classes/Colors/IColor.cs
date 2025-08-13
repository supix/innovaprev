using DomainModel.Classes.Products.Visitors;

namespace DomainModel.Classes.Colors
{
    public interface IColor
    {
        string Code { get; }
        string Description { get; }
        decimal Price_sqm { get; }
        int Order { get; }
        string[] InternalColorForProducts { get; }
        string[] ExternalColorForProducts { get; }
        decimal GetPrice_COP_m(IColorVisitor visitor);
        decimal GetPrice_FRO_m(IColorVisitor visitor);
        decimal GetPrice_CAS_m(IColorVisitor visitor);
    }
}
