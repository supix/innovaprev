using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Materials
{
    public interface IMaterial
    {
        string Code { get; }
        string Description { get; }
        decimal GetPrice(IMaterialVisitor visitor);
        int NumberOfDimensions { get; }
        bool openingTypeVisible { get; }
        bool glassTypeVisible { get; }
        bool wireCoverVisible { get; }
        string[] MaterialForProduct { get; }
        long? MinAllowedWidth_mm => null;
        long? MinAllowedHeight_mm => null;
        long? MinAllowedLength_mm => null;
        long? MaxAllowedWidth_mm => null;
        long? MaxAllowedHeight_mm => null;
        long? MaxAllowedLength_mm => null;
    }
}
