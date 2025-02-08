namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class PRT1A : DoubleDimMaterial
    {
        public PRT1A(long height_mm, long width_mm) : base(height_mm, width_mm)
        {
        }
        public override string Description => "Portoncino 1 anta";
        protected override long ClampMinValue => 1500000;
        public override int Order => 110;
    }
}
