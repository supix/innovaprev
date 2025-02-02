namespace DomainModel.Classes.Materials
{
    public abstract class Material : IMaterial
    {
        public abstract string Code { get; }
        public abstract string Description { get; }
        public abstract int NumberOfDimensions { get; }
        protected abstract long ClampMinValue { get; }
        public abstract long DimensionToQuote { get; }
    }
}
