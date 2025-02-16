using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Materials
{
    public interface IMaterial
    {
        string Code { get; }
        string Description { get; }
        decimal GetPrice(IVisitor visitor);
        int NumberOfDimensions { get; }
    }
}
