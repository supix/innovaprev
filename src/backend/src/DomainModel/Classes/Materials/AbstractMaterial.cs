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
        protected abstract long? ClampMinValue { get; }
        public abstract long DimensionToQuote { get; }
        public abstract int Order { get; }
        public abstract string[] MaterialForProduct { get; }
        public abstract decimal GetPrice(IMaterialVisitor visitor);
    }
}
