namespace DomainModel.Classes.Materials
{
    public abstract class AbstractMaterial : IMaterial
    {
        public string Code => GetType().Name;
        public abstract string Description { get; }
        public abstract int NumberOfDimensions { get; }
        protected abstract long? ClampMinValue { get; }
        public abstract long DimensionToQuote { get; }
        public abstract int Order { get; }
    }
}
