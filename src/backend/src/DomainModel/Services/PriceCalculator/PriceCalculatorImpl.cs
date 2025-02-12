using DomainModel.Classes;
using DomainModel.Classes.Materials;
using DomainModel.Classes.Products;

namespace DomainModel.Services.PriceCalculator
{
    internal class PriceCalculatorImpl : IPriceCalculator
    {
        public PriceInfo getPrices(ProductData productData, WindowsData[] windowsData, CustomData[] customData)
        {
            var product = ProductFactory.CreateByCode(productData.Product);
            
            var productPriceInfo = windowsData.Aggregate(
                new PriceInfo
                {
                    GrandTotal = 0M,
                    DetailPrices = new List<DetailPrice>()
                }, 
                (Func<PriceInfo, WindowsData, PriceInfo>)((acc, x) =>
                {
                    var material = MaterialFactory.CreateByCode(x.WindowType, x.Length != 0 ? [x.Length] : [x.Height, x.Width]);
                    var netPrice = product.getMaterialPrice(material);
                    var totalMaterialPrice = netPrice * x.Quantity;
                    var detailPrice = new DetailPrice() { NetPrice = totalMaterialPrice, Vat = 0.22M };
                    acc.DetailPrices.Add(detailPrice);
                    acc.Total += totalMaterialPrice;
                    acc.Tax += detailPrice.Tax;
                    acc.GrandTotal += detailPrice.TotalPrice;
                    return acc;
                })
            );

            return customData.Aggregate(productPriceInfo, ((acc, x) =>
            {
                var detailPrice = new DetailPrice() { NetPrice = x.Price, Vat = 0.22M };
                acc.DetailPrices.Add(detailPrice);
                acc.Total += detailPrice.NetPrice;
                acc.Tax += detailPrice.Tax;
                acc.GrandTotal += detailPrice.TotalPrice;
                return acc;
            }));
        }
    }
}
