namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class FIXA : DoubleDimMaterial
    {
        public FIXA(long height_mm, long width_mm) : base(height_mm, width_mm)
        {
        }

        public override string Code => "FIXA";

        public override string Description => "Fisso con anta fissa";

        protected override long ClampMinValue => 1500000;
    }
}
