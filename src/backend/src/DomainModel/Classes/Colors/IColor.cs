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
    }
}
