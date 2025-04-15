using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class CAS : DoubleDimFixedMaterial
    {
        public CAS(long height_mm, long width_mm) : base(height_mm, width_mm, false, false)
        {
            if (height_mm < 500)
                throw new InvalidOperationException("CAS height must be greater or equal than 500 mm");
        }

        public override string Description => "Cassonetti";

        protected override long? ClampMinValue => 1000;
        public override int Order => 200;
        public override decimal GetPrice(IMaterialVisitor visitor)
        {
            var allowedWidth = this.ClampMinValue.HasValue && Width_mm < this.ClampMinValue ? this.ClampMinValue.Value : Width_mm;
            return visitor.GetPrice_CAS(this, allowedWidth);
        }
        public override bool glassTypeVisible => false;
        public override bool wireCoverVisible => false;
        public override string[] MaterialForProduct => base.GetAllProductCodes();
    }

}
