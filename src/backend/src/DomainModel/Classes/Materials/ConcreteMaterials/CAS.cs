namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class CAS : SingleDimMaterial
    {
        public CAS(long length_mm) : base(length_mm)
        {
        }

        public override string Description => "Cassonetti";

        protected override long? ClampMinValue => null;
        public override int Order => 200;
    }

}
