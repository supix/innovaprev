using DomainModel.Classes.Products.ConcreteProducts;

namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class SIL : DoubleDimMaterial
    {
        public SIL(long height_mm, long width_mm, string openingType, bool opaqueGlass, bool wireCover) : base(height_mm, width_mm, openingType, opaqueGlass, wireCover)
        {
        }
        public override string Description => "Scorrevole in linea";
        protected override long? ClampMinValue => 2500000;
        public override int Order => 30;
        public override string[] MaterialForProduct => new[] { typeof(SP).Name };
    }
}
