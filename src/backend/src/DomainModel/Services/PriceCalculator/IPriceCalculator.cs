using DomainModel.Classes;

namespace DomainModel.Services.PriceCalculator
{
    public interface IPriceCalculator
    {
        PriceInfo getPrices(ProductData productData, WindowsData[] windowsData, CustomData[] customData);
    }
}