namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class FLS : DoubleDimFixedMaterial
    {
        public FLS(long height_mm, long width_mm, bool opaqueGlass, bool wireCover) : base(height_mm, width_mm, opaqueGlass, wireCover)
        {
        }

        public override string Description => "Fisso laterale sx";
        protected override long? ClampMinValue => 1000000;
        public override int Order => 160;
        public override string[] MaterialForProduct => base.GetNotAntaMaxAndNotScorrevoleProductCodes();
    }
}
