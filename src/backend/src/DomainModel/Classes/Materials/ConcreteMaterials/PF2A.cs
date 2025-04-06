namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class PF2A : DoubleDimMaterial
    {
        public PF2A(long height_mm, long width_mm, bool opaqueGlass, bool wireCover) : base(height_mm, width_mm, opaqueGlass, wireCover)
        {
        }
        public override string Description => "Finestra 2 Ante";
        protected override long? ClampMinValue => 1800000;
        public override int Order => 70;
        public override string[] MaterialForProduct => base.GetNotAntaMaxAndNotScorrevoleProductCodes();
    }
}
