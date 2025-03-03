namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class SLF : DoubleDimMaterial
    {
        public SLF(long height_mm, long width_mm) : base(height_mm, width_mm)
        {
        }
        public override string Description => "Sopraluce fisso";
        protected override long? ClampMinValue => 1000000;
        public override int Order => 130;
    }

}
