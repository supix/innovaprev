using DomainModel.Classes.Colors;
using DomainModel.Classes.Frames;
using DomainModel.Classes.Materials;
using DomainModel.Classes.Materials.ConcreteMaterials;
using DomainModel.Classes.Products.Visitor;
using DomainModel.Services.WireCoverLenCalculator;

namespace DomainModel.Classes.Products
{
    public abstract class AbstractProduct : IProduct, IMaterialVisitor, IColorVisitor
    {
        protected readonly IColor ic;
        protected readonly IColor ec;

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
        public decimal GetPrice_FRO(FRO m, long length_mm)
        {
            var price_m = ic.GetPrice_FRO_m(this);
            return GetFullSingleDimensionPrice(price_m, length_mm);
        }
        public abstract decimal GetPrice_DoubleDim(DoubleDimMaterial m, long area_sqmm);
        public abstract decimal GetPrice_DoubleDimFixed(DoubleDimFixedMaterial m, long area_sqmm);

        protected decimal GetFullDoubleDimensionPrice(decimal price_sqm, long area_sqmm, bool opaqueGlass, bool wireCover, long height_mm, long width_mm, IFrame frame)
        {
            const decimal TRANSPARENT_GLASS_PRICE_SQM = 38M;
            const decimal OPAQUE_GLASS_PRICE_SQM = 48M;
            decimal glassPrice_sqm = opaqueGlass ? OPAQUE_GLASS_PRICE_SQM : TRANSPARENT_GLASS_PRICE_SQM;
            decimal framePrice_sqm = frame.GetPrice_sqm();

            var wireCoverPrice = 0M;
            if (wireCover)
            {
                var totLen = WireCoverLenCalculator.GetLen(height_mm, width_mm);
                wireCoverPrice = totLen / 1000M * ic.GetPrice_COP_m(this);
            }

            return (price_sqm + ic.Price_sqm + ec.Price_sqm + glassPrice_sqm + framePrice_sqm) * area_sqmm / 1e6M + wireCoverPrice;
        }

        protected decimal GetFullSingleDimensionPrice(decimal price_m, long length_mm)
        {
            return price_m * length_mm / 1e3M;
        }
        public abstract decimal GetPrice_COP_Standard_m();
        public abstract decimal GetPrice_COP_OpenPore_m();
        public abstract decimal GetPrice_COP_BrushedDecapeToulipier_m();
        public abstract decimal GetPrice_FRO_Standard_m();
        public abstract decimal GetPrice_FRO_OpenPore_m();
        public abstract decimal GetPrice_FRO_BrushedDecapeToulipier_m();
        public abstract decimal GetPrice_CAS_Standard_m();
        public abstract decimal GetPrice_CAS_OpenPore_m();
        public abstract decimal GetPrice_CAS_BrushedDecapeToulipier_m();
        public abstract decimal GetPrice_CAS_PvcWhite_m();
        public abstract decimal GetPrice_CAS_PvcWoodEffect_m();
        public abstract decimal GetPrice_FRO_PvcWhite_m();
        public abstract decimal GetPrice_FRO_PvcWoodEffect_m();
        public abstract decimal GetPrice_COP_PvcWhite_m();
        public abstract decimal GetPrice_COP_PvcWoodEffect_m();
        protected abstract IWireCoverLenCalculator WireCoverLenCalculator { get; }
    }
}
