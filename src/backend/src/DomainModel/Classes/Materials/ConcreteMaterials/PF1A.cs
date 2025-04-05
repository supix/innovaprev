namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class PF1A : DoubleDimMaterial
    {
        public PF1A(long height_mm, long width_mm, bool opaqueGlass) : base(height_mm, width_mm, opaqueGlass)
        {
        }
        public override string Description => "Portafinestra 1 Anta";
        protected override long? ClampMinValue => 1500000;
        public override int Order => 20;
        public override string[] MaterialForProduct => base.GetNotAntaMaxAndNotScorrevoleProductCodes();
    }
}
