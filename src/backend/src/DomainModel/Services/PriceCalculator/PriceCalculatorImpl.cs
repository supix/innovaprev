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
            const decimal sqm_glassPrice = 38M;
            var sqm_price = product.StandardPrice + sqm_glassPrice;

            var productPriceInfo = windowsData.Aggregate(
                new PriceInfo
                {
                    GrandTotal = 0M,
                    DetailPrices = new List<DetailPrice>()
                }, 
                (Func<PriceInfo, WindowsData, PriceInfo>)((acc, x) =>
                {
                    var material = MaterialFactory.CreateByCode(x.WindowType, x.Length != 0 ? [x.Length] : [x.Height, x.Width]);
                    var numOfDims = material.NumberOfDimensions;
                    throw new NotImplementedException();
                    //var total_measure = material.EffectiveArea * x.Quantity;
                    //decimal netPrice = sqm_price * total_area;
                    //var detailPrice = new DetailPrice() { UnitPrice = sqm_price, NetPrice = netPrice, Vat = 0.22M };
                    //acc.DetailPrices.Add(detailPrice);
                    //acc.Total += detailPrice.NetPrice;
                    //acc.Tax += detailPrice.Tax;
                    //acc.GrandTotal += detailPrice.TotalPrice;
                    //return acc;
                })
            );

            return customData.Aggregate(productPriceInfo, ((acc, x) =>
            {
                var detailPrice = new DetailPrice() { UnitPrice = 0M, NetPrice = x.Price, Vat = 0.22M };
                acc.DetailPrices.Add(detailPrice);
                acc.Total += detailPrice.NetPrice;
                acc.Tax += detailPrice.Tax;
                acc.GrandTotal += detailPrice.TotalPrice;
                return acc;
            }));
        }
    }
}
