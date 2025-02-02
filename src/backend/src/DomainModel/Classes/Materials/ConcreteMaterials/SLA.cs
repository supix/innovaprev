namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class SLA : DoubleDimMaterial
    {
        public SLA(long height_mm, long width_mm) : base(height_mm, width_mm)
        {
        }

        public override string Code => "SLA";

        public override string Description => "Sopraluce apribile";

        protected override long ClampMinValue => 1500000;
    }

}
