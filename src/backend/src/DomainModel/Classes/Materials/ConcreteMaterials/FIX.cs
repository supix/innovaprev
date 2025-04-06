namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class FIX : DoubleDimFixedMaterial
    {
        public FIX(long height_mm, long width_mm, bool opaqueGlass) : base(height_mm, width_mm, opaqueGlass)
        {
        }
        public override string Description => "Fisso con fermavetro";
        protected override long? ClampMinValue => 1000000;
        public override int Order => 0;
        public override string[] MaterialForProduct => base.GetNotAntaMaxAndNotScorrevoleProductCodes();
    }
}
