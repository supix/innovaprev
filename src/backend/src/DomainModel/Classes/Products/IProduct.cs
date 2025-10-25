using DomainModel.Classes.Materials;

namespace DomainModel.Classes.Products
{
    public interface IProduct
    {
        string Code { get; }
        string Description { get; }
        bool TrimSectionVisible { get; }
        bool SingleColor { get; }
        string ExtendedDescriptionTitle { get; }
        string ExtendedDescription { get; }
        decimal GetMaterialPrice(IMaterial material);
        string GetGlassDescription(IMaterial material);
    }
}
