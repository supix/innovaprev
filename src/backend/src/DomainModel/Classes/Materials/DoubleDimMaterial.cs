using DomainModel.Classes.Products;
using DomainModel.Classes.Products.ConcreteProducts;
using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Materials
{
    public abstract class DoubleDimMaterial : AbstractMaterial
    {
        protected DoubleDimMaterial(long height_mm, long width_mm, bool opaqueGlass, bool wireCover)
        {
            Height_mm = height_mm;
            Width_mm = width_mm;
            OpaqueGlass = opaqueGlass;
            WireCover = wireCover;
        }

        public override sealed int NumberOfDimensions => 2;
        public long Height_mm { get; }
        public long Width_mm { get; }
        public bool OpaqueGlass { get; }
        public bool WireCover { get; }
        public override bool openingTypeVisible => true;
        public override sealed bool glassTypeVisible => true;
        public override sealed bool wireCoverVisible => true;
        public override sealed long DimensionToQuote
        {
            get
            {
                var area_smm = Height_mm * Width_mm;
                if (!ClampMinValue.HasValue)
                    return area_smm;
                return area_smm >= ClampMinValue.Value ? area_smm : ClampMinValue.Value;
            }
        }
        public long GetAllowedArea_sqmm
        {
            get
            {
                var netArea_sqmm = Height_mm * Width_mm;
                if (ClampMinValue.HasValue && netArea_sqmm < ClampMinValue.Value)
                    return ClampMinValue.Value;
                else
                    return netArea_sqmm;
            }
        }
        public override decimal GetPrice(IMaterialVisitor visitor)
        {
            return visitor.GetPrice_DoubleDim(this, GetAllowedArea_sqmm);
        }
        protected string[] GetNotAntaMaxAndNotScorrevoleProductCodes()
        {
            return ProductFactory.GetAll()
                .Where(p => !(p is IAntaMaxAbstractProduct) && (p is not SP))
                .Select(p => p.Code)
                .ToArray();
        }
        protected string[] GetAntaMaxProductCodes()
        {
            return ProductFactory.GetAll()
                .Where(p => p is IAntaMaxAbstractProduct)
                .Select(p => p.Code)
                .ToArray();
        }
    }
}
