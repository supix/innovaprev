namespace DomainModel.Classes.Materials
{
    public interface IMaterial
    {
        string Code { get; }
        string Description { get; }
        int NumberOfDimensions { get; }
        decimal GetArea_sqm { get; }
        decimal GetLength_m { get; }
    }
}
