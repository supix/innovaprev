namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class PRT2A : NeedsLockMaterial
    {
        public PRT2A(long height_mm, long width_mm, bool opaqueGlass) : base(height_mm, width_mm, opaqueGlass)
        {
        }
        public override string Description => "Portoncino 2 ante";
        protected override long? ClampMinValue => 1800000;
        public override int Order => 120;
        public override string[] MaterialForProduct => base.GetAntaMaxProductCodes();
    }
}
