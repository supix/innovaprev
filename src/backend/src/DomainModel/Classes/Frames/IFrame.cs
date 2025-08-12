namespace DomainModel.Classes.Frames
{
    public interface IFrame
    {
        string Code { get; }
        string Description { get; }
        int Order { get; }
        string[] FrameForProduct { get; }
        decimal GetPrice_sqm();
    }
}
