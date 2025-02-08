namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class FLD : DoubleDimMaterial
    {
        public FLD(long height_mm, long width_mm) : base(height_mm, width_mm)
        {
        }

        public override string Description => "Fisso laterale dx";
        protected override long ClampMinValue => 1500000;
        public override int Order => 150;
    }

}
