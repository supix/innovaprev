using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class CAS : SingleDimMaterial
    {
        public CAS(long length_mm) : base(length_mm)
        {
        }

        public override string Description => "Cassonetti";

        protected override long? ClampMinValue => 1000;
        public override int Order => 200;
        public override decimal GetPrice(IMaterialVisitor visitor)
        {
            return visitor.GetPrice_CAS(this, GetAllowedLength_mm);
        }
        public override string[] MaterialForProduct => base.GetAllProductCodes();

    }

}
