﻿using System.Collections;

namespace DomainModel.Services.PriceCalculator
{
    public class Price
    {
        public decimal Total { get; set; }
        public decimal Tax { get; set; }
        public decimal GrandTotal { get; set; }
        public required IList<DetailPrice> DetailPrices { get; set; }
    }
}