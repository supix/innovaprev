namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class CAS : SingleDimMaterial
    {
        public CAS(long length_mm) : base(length_mm)
        {
        }

        public override string Description => "Cassonetti";

        protected override long? ClampMinValue => 1000;
        public override int Order => 200;
    }

}
