namespace DomainModel.Classes.Materials
{
    public interface IMaterial
    {
        string Code { get; }
        string Description { get; }
        int NumberOfDimensions { get; }
    }
}
