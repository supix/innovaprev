namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class CEL : SingleDimMaterial
    {
        public CEL(long length_mm) : base(length_mm)
        {
        }

        public override string Description => "Celetti";

        protected override long? ClampMinValue => null;
        public override int Order => 210;
    }

}
