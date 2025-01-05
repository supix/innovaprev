namespace DomainModel.Services
{
    public interface IImageProvider
    {
        byte[] Get(string name);
    }
}
