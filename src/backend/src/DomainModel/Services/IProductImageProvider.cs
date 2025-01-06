namespace DomainModel.Services
{
    public interface IProductImageProvider
    {
        byte[] Get(string productCode, bool isThumb);
    }
}
