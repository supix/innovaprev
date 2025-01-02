﻿using System;
using DomainModel.Classes;

namespace DomainModel.Services.PriceCalculator
{
    internal class PriceCalculatorImpl : IPriceCalculator
    {
        public PriceInfo getPrices(ProductData productData, WindowsData[] windowsData, CustomData[] customData)
        {
            var price = new PriceInfo
            {
                GrandTotal = 0M,
                DetailPrices = new List<DetailPrice>()
            };

            var productPriceInfo = windowsData.Aggregate(price, (Func<PriceInfo, WindowsData, PriceInfo>)((acc, x) =>
            {
                const int sqm_price = 985;
                var area_sqm = decimal.Divide(x.Height * x.Width, 1e6M);
                if (area_sqm < 1.5M)
                    area_sqm = 1.5M;
                var total_area = area_sqm * x.Quantity;
                decimal netPrice = sqm_price * total_area;
                var detailPrice = new DetailPrice() { UnitPrice = sqm_price, NetPrice = netPrice, Vat = 0.22M };
                acc.DetailPrices.Add(detailPrice);
                acc.Total += detailPrice.NetPrice;
                acc.Tax += detailPrice.Tax;
                acc.GrandTotal += detailPrice.TotalPrice;
                return acc;
            }));

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
