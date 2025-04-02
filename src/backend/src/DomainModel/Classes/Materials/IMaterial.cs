using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Materials
{
    public interface IMaterial
    {
        string Code { get; }
        string Description { get; }
        decimal GetPrice(IMaterialVisitor visitor);
        int NumberOfDimensions { get; }
        string[] MaterialForProduct { get; }
    }
}
