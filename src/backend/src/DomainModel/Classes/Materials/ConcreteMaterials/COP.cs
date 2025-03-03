using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class COP : SingleDimMaterial
    {
        public COP(long length_mm) : base(length_mm)
        {
        }
        public override string Description => "Coprifili";
        protected override long? ClampMinValue => null;
        public override int Order => 180;
        public override decimal GetPrice(IVisitor visitor)
        {
            return visitor.GetPrice_COP(this, GetAllowedLength_mm);
        }
    }

}
