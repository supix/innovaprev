namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class PF2A : DoubleDimMaterial
    {
        public PF2A(long height_mm, long width_mm) : base(height_mm, width_mm)
        {
        }
        public override string Description => "Finestra 2 Ante";
        protected override long ClampMinValue => 1500000;
        public override int Order => 70;
    }
}
