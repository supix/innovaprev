using DomainModel.Classes.Frames;
using DomainModel.Classes.Products.ConcreteProducts;
using DomainModel.Classes.Products.Visitors;

namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class SIL : DoubleDimMaterial
    {
        public SIL(long height_mm, long width_mm, string openingType, bool opaqueGlass, bool wireCover, IFrame frameType) : base(height_mm, width_mm, openingType, opaqueGlass, wireCover, frameType)
        {
        }
        public override string Description => "Scorrevole in linea";
        protected override long? ClampMinValue => 2500000;
        public override int Order => 30;
        public override string[] MaterialForProduct => new[] { typeof(SP).Name , typeof(IPC).Name, typeof(IPN).Name };
        public override bool ForceScorrevolePrice()
        {
            return true;
        }
        public override decimal GetPrice(IMaterialVisitor visitor)
        {
            return visitor.GetPrice_DoubleDimScorrevole(this, GetAllowedArea_sqmm);
        }
    }
}
