using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class FRO : SingleDimMaterial
    {
        public FRO(long length_mm) : base(length_mm)
        {
        }

        public override string Description => "Frontalini";

        protected override long? ClampMinValue => 1000;
        public override int Order => 210;
        public override decimal GetPrice(IMaterialVisitor visitor)
        {
            return visitor.GetPrice_FRO(this, GetAllowedLength_mm);
        }
        public override string[] MaterialForProduct => base.GetAllProductCodes();
    }

}
