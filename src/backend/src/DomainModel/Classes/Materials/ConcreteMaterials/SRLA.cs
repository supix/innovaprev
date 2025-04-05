namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class SRLA : DoubleDimMaterial
    {
        public SRLA(long height_mm, long width_mm, bool opaqueGlass) : base(height_mm, width_mm, opaqueGlass)
        {
        }
        public override string Description => "Scorrevole Ribalta con laterale apribile";
        protected override long? ClampMinValue => 2500000;
        public override int Order => 90;
        public override string[] MaterialForProduct => base.GetNotAntaMaxAndNotScorrevoleProductCodes();
    }
}
