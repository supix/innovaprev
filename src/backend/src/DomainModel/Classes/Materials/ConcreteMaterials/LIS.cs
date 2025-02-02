namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class LIS : SingleDimMaterial
    {
        public LIS(long length_mm) : base(length_mm)
        {
        }

        public override string Code => "LIS";

        public override string Description => "Listelli";

        protected override long ClampMinValue => 1000000;
    }

}
