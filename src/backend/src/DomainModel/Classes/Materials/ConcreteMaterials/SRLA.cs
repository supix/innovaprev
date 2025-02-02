namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class SRLA : DoubleDimMaterial
    {
        public SRLA(long height_mm, long width_mm) : base(height_mm, width_mm)
        {
        }

        public override string Code => "SRLA";

        public override string Description => "Scorrevole Ribalta con laterale apribile";

        protected override long ClampMinValue => 2500000;
    }
}
