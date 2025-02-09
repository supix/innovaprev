namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class F1A : DoubleDimMaterial
    {
        public F1A(long height_mm, long width_mm) : base(height_mm, width_mm)
        {
        }

        public override string Description => "Finestra 1 Anta";

        protected override long? ClampMinValue => 1500000;
        public override int Order => 10;
    }
}
