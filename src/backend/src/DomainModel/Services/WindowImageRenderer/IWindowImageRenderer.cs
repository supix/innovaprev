namespace DomainModel.Services.WindowImageRenderer
{
    public interface IWindowImageRenderer
    {
        bool CanRender(string materialType);
        byte[] Render(long height_mm, long witdh_mm, string materialType, bool wireCover, string openingType, string glassType);
    }
}
