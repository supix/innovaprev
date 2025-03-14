using DomainModel.Classes;
using DomainModel.Classes.Colors;
using DomainModel.Classes.Materials;
using DomainModel.Classes.Products;

namespace DomainModel.Services.PriceCalculator
{
    internal class PriceCalculatorImpl : IPriceCalculator
    {
        public PriceInfo getPrices(ProductData productData, WindowsData[] windowsData, CustomData[] customData)
        {
            var internalColor = ColorFactory.CreateByCode(productData.InternalColor);
            var externalColor = ColorFactory.CreateByCode(productData.ExternalColor ?? productData.InternalColor);
            var product = ProductFactory.CreateByCode(productData.Product, internalColor, externalColor);

            var productPriceInfo = windowsData.Aggregate(
                new PriceInfo
                {
                    GrandTotal = 0M,
                    DetailPrices = new List<DetailPrice>()
                },
                (Func<PriceInfo, WindowsData, PriceInfo>)((acc, x) =>
                {
                    long m1 = x.Length != 0 ? x.Length : x.Height;
                    long m2 = x.Width;
                    var material = MaterialFactory.CreateByCode(x.WindowType, m1, m2);
                    var netPrice = product.GetMaterialPrice(material);
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
