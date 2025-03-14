﻿using DomainModel.Classes.Colors;
using DomainModel.Classes.Materials;
using DomainModel.Classes.Materials.ConcreteMaterials;
using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Products
{
    public abstract class AbstractProduct : IProduct, IVisitor
    {
        private readonly IColor ic;
        private readonly IColor ec;

        public AbstractProduct(IColor ic, IColor ec)
        {
            this.ic = ic;
            this.ec = ec;
        }
        public string Code => GetType().Name;
        public abstract string Description { get; }
        public abstract bool TrimSectionVisible { get; }
        public abstract bool SingleColor { get; }
        public abstract string ExtendedDescriptionTitle { get; }
        public abstract string ExtendedDescription { get; }
        public abstract int Order { get; }
        public virtual decimal GetMaterialPrice(IMaterial material)
        {
            return material.GetPrice(this);
        }
        public abstract decimal GetPrice_CAS(CAS m, long length_mm);
        public abstract decimal GetPrice_COP(COP m, long length_mm);
        public decimal GetPrice_FRO(FRO m, long length_mm)
        {
            return GetFullSingleDimensionPrice(142M, length_mm);
        }
        public abstract decimal GetPrice_DoubleDim(DoubleDimMaterial m, long area_sqmm);
        public abstract decimal GetPrice_DoubleDimFixed(DoubleDimFixedMaterial m, long area_sqmm);

        protected decimal GetFullDoubleDimensionPrice(decimal price_sqm, long area_sqmm)
        {
            const decimal GLASS_PRICE_SQM = 38M;
            return (price_sqm + GLASS_PRICE_SQM) * area_sqmm / 1e6M;
        }

        protected decimal GetFullSingleDimensionPrice(decimal price_m, long length_mm)
        {
            return price_m * length_mm / 1e3M;
        }
    }
}
