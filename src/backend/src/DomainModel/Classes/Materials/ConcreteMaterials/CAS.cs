using DomainModel.Classes.Products.Visitors;

namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class CAS : AbstractMaterial
    {
        private const int _minAllowedLength = 500;
        private const int _maxAllowedLength = 1500;

        public CAS(long height_mm, long width_mm)
        {
            if (height_mm < _minAllowedLength)
                throw new InvalidOperationException($"CAS material cannot have height less than {_minAllowedLength}. It is {height_mm}.");
            Height_mm = height_mm;
            Width_mm = width_mm;
        }
        public long Height_mm { get; }
        public long Width_mm { get; }
        public override string Description => "Cassonetti";
        protected override long? ClampMinValue => 1000;
        public override int Order => 200;
        public override decimal GetPrice(IMaterialVisitor visitor)
        {
            return visitor.GetPrice_CAS(this, DimensionToQuote);
        }
        public override bool glassTypeVisible => false;
        public override bool wireCoverVisible => false;
        public override bool frameTypeVisible => false;
        public override string[] MaterialForProduct => base.GetAllProductCodes();
        public override long? MaxAllowedHeight_mm => _maxAllowedLength;
        public override long? MinAllowedHeight_mm => _minAllowedLength;

        public override int NumberOfDimensions => 2;

        public override bool openingTypeVisible => false;

        public override long DimensionToQuote
        {
            get
            {
                var allowedWidth = ClampMinValue.HasValue && Width_mm < ClampMinValue ? ClampMinValue.Value : Width_mm;
                return allowedWidth;
            }
        }
    }

}
