using DomainModel.Classes.Frames;
using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class VASM : DoubleDimMaterial
    {
        public VASM(long height_mm, long width_mm, string openingType, bool opaqueGlass, bool wireCover, IFrame frameType) : base(height_mm, width_mm, openingType, opaqueGlass, wireCover, frameType)
        {
        }
        public override bool openingTypeVisible => false;
        public override string Description => "Vasistas (apertura a motore)";
        protected override long? ClampMinValue => 1500000;
        public override int Order => 54;
        public override string[] MaterialForProduct => base.GetNotAntaMaxAndNotScorrevoleProductCodes();
        public override decimal GetPrice(IMaterialVisitor visitor)
        {
            return base.GetPrice(visitor) + 180M;
        }
    }
}
