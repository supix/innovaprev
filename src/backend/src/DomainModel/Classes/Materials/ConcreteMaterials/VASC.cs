namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class VASC : DoubleDimMaterial
    {
        public VASC(long height_mm, long width_mm, string openingType, bool opaqueGlass, bool wireCover) : base(height_mm, width_mm, openingType, opaqueGlass, wireCover)
        {
        }
        public override bool openingTypeVisible => false;
        public override string Description => "Vasistas (apertura a cricchetto)";
        protected override long? ClampMinValue => 1500000;
        public override int Order => 52;
        public override string[] MaterialForProduct => base.GetNotAntaMaxAndNotScorrevoleProductCodes();
    }
}
