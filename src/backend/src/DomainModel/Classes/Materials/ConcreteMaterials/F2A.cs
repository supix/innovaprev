namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class F2A : DoubleDimMaterial
    {
        public F2A(long height_mm, long width_mm) : base(height_mm, width_mm)
        {
        }

        public override string Code => "F2A";

        public override string Description => "Finestra 2 Ante";

        protected override long ClampMinValue => 1800000;
    }
}
