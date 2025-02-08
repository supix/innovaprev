namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class AD : DoubleDimMaterial
    {
        public AD(long height_mm, long width_mm) : base(height_mm, width_mm)
        {
        }

        public override string Description => "A Disegno allegato";
        protected override long ClampMinValue => 1500000;
        public override int Order => 170;
    }

}
