namespace DomainModel.Classes.Products
{
    public interface IProduct
    {
        string Code { get; }
        string Description { get; }
        bool TrimSectionVisible { get; }
        string ExtendedDescriptionTitle { get; }
        string ExtendedDescription { get; }
        decimal StandardPrice { get; }
    }
}
