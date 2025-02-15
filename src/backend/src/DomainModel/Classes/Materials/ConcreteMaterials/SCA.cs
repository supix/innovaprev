using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class SCA : DoubleDimMaterial
    {
        public SCA(long height_mm, long width_mm) : base(height_mm, width_mm)
        {
        }
        public override string Description => "Scorrevole alzante";
        protected override long? ClampMinValue => 2500000;
        public override int Order => 100;
    }
}
