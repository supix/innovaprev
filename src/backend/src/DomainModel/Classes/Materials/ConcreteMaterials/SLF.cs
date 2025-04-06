namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class SLF : DoubleDimFixedMaterial
    {
        public SLF(long height_mm, long width_mm, bool opaqueGlass, bool wireCover) : base(height_mm, width_mm, opaqueGlass, wireCover)
        {
        }
        public override string Description => "Sopraluce fisso";
        protected override long? ClampMinValue => 1000000;
        public override int Order => 130;
        public override string[] MaterialForProduct => base.GetNotAntaMaxAndNotScorrevoleProductCodes();
    }

}
