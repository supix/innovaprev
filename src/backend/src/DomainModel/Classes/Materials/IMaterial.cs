namespace DomainModel.Classes.Materials
{
    public interface IMaterial
    {
        string Code { get; }
        string Description { get; }
        int NumberOfDimensions { get; }
        decimal GetAllowedArea_sqm { get; }
        decimal GetAllowedLength_m { get; }
    }
}
