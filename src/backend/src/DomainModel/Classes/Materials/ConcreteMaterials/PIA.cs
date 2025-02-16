namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class PIA : SingleDimMaterial
    {
        public PIA(long length_mm) : base(length_mm)
        {
        }
        public override string Description => "Piatte";
        protected override long? ClampMinValue => null;
        public override int Order => 180;
    }

}
