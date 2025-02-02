namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class SIL : DoubleDimMaterial
    {
        public SIL(long height_mm, long width_mm) : base(height_mm, width_mm)
        {
        }

        public override string Code => "SIL";

        public override string Description => "Scorrevole in linea";

        protected override long ClampMinValue => 2500000;
    }
}
