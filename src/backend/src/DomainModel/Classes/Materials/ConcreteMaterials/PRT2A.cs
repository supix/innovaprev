namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class PRT2A : DoubleDimMaterial
    {
        public PRT2A(long height_mm, long width_mm) : base(height_mm, width_mm)
        {
        }

        public override string Code => "PRT2A";

        public override string Description => "Portoncino 2 ante";

        protected override long ClampMinValue => 1800000;
    }
}
