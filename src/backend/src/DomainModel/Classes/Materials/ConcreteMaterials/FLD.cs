namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class FLD : DoubleDimFixedMaterial
    {
        public FLD(long height_mm, long width_mm, bool opaqueGlass, bool wireCover) : base(height_mm, width_mm, opaqueGlass, wireCover)
        {
        }

        public override string Description => "Fisso laterale dx";
        protected override long? ClampMinValue => 1000000;
        public override int Order => 150;
        public override string[] MaterialForProduct => base.GetNotAntaMaxAndNotScorrevoleProductCodes();
    }
}
