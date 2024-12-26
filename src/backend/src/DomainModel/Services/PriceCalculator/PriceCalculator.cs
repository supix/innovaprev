using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Classes;

namespace DomainModel.Services.PriceCalculator
{
    public class PriceCalculator
    {
        private readonly ProductData productData;
        private readonly WindowsData[] windowsData;

        public PriceCalculator(ProductData productData, WindowsData[] windowsData)
        {
            this.productData = productData ?? throw new ArgumentNullException(nameof(productData));
            this.windowsData = windowsData ?? throw new ArgumentNullException(nameof(windowsData));
        }

        public Price getPrices()
        {
            var price = new Price
            {
                GrandTotal = 0M,
                DetailPrices = new List<DetailPrice>()
            };

            return this.windowsData.Aggregate(price, (Func<Price, WindowsData, Price>)((acc, x) =>
            {
                const int sqm_price = 985;
                var area_sqm = decimal.Divide(x.Height * x.Width, 1e6M);
                if (area_sqm < 1.5M)
                    area_sqm = 1.5M;
                var total_area = area_sqm * x.Quantity;
                decimal netPrice = sqm_price * total_area;
                var detailPrice = new DetailPrice() { NetPrice = netPrice, Vat = 0.22M };
                acc.DetailPrices.Add(detailPrice);
                acc.Total += detailPrice.NetPrice;
                acc.Tax += detailPrice.Tax;
                acc.GrandTotal += detailPrice.TotalPrice;
                return acc;
            }));
        }
    }
}
