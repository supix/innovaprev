namespace DomainModel.Services.WireCoverLenCalculator
{
    public interface IWireCoverLenCalculator
    {
        long GetLen(long height_mm, long width_mm);
    }
}
