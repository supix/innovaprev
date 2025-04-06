using DomainModel.Classes.Products;
using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Materials
{
    public abstract class SingleDimMaterial : AbstractMaterial
    {
        public SingleDimMaterial(long length_mm)
        {
            Length_mm = length_mm;
        }

        public override sealed int NumberOfDimensions => 1;
        public override sealed bool openingTypeVisible => false;
        public sealed override bool glassTypeVisible => false;
        public sealed override bool wireCoverVisible => false;
        public long Length_mm { get; set; }
        public override sealed long DimensionToQuote
        {
            get
            {
                if (!ClampMinValue.HasValue)
                    return Length_mm;
                return Length_mm >= ClampMinValue.Value ? Length_mm : ClampMinValue.Value;
            }
        }
        public long GetAllowedLength_mm
        {
            get
            {
                if (ClampMinValue.HasValue && Length_mm < ClampMinValue.Value)
                    return ClampMinValue.Value;
                else
                    return Length_mm;
            }
        }
        public override decimal GetPrice(IMaterialVisitor visitor)
        {
            return 0;
        }
        protected string[] GetAllProductCodes()
        {
            return ProductFactory.GetAll()
                .Select(p => p.Code)
                .ToArray();
        }
    }
}
