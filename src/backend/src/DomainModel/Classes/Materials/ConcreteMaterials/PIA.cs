namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class PIA : SingleDimMaterial
    {
        public PIA(long length_mm) : base(length_mm)
        {
        }

        public override string Code => "PIA";

        public override string Description => "Piatte";

        protected override long ClampMinValue => 1000000;
    }

}
