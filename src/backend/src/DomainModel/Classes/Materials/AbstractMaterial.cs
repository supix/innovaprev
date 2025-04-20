using DomainModel.Classes.Products;
using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Materials
{
    public abstract class AbstractMaterial : IMaterial
    {
        public string Code => GetType().Name;
        public abstract string Description { get; }
        public abstract int NumberOfDimensions { get; }
        public abstract bool openingTypeVisible { get; }
        public abstract bool glassTypeVisible { get; }
        public abstract bool wireCoverVisible { get; }
        protected abstract long? ClampMinValue { get; }
        public abstract long DimensionToQuote { get; }
        public abstract int Order { get; }
        public abstract string[] MaterialForProduct { get; }
        public abstract decimal GetPrice(IMaterialVisitor visitor);
        protected string[] GetAllProductCodes()
        {
            return ProductFactory.GetAll()
                .Select(p => p.Code)
                .ToArray();
        }
        public virtual long? MinAllowedWidth_mm => null;
        public virtual long? MinAllowedHeight_mm => null;
        public virtual long? MinAllowedLength_mm => null;
        public virtual long? MaxAllowedWidth_mm => null;
        public virtual long? MaxAllowedHeight_mm => null;
        public virtual long? MaxAllowedLength_mm => null;
    }
}
