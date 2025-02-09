namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class LIS : SingleDimMaterial
    {
        public LIS(long length_mm) : base(length_mm)
        {
        }
        public override string Description => "Listelli";
        protected override long? ClampMinValue => null;
        public override int Order => 190;
    }

}
