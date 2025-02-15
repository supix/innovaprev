namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class SRAF : DoubleDimMaterial
    {
        public SRAF(long height_mm, long width_mm) : base(height_mm, width_mm)
        {
        }
        public override string Description => "Scorrevole Ribalta con anta fissa";
        protected override long? ClampMinValue => 2500000;
        public override int Order => 80;
    }
}
