namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class SRLA : ShiftableFlap
    {
        public SRLA(long height_mm, long width_mm, string openingType, bool opaqueGlass, bool wireCover) : base(height_mm, width_mm, openingType, opaqueGlass, wireCover)
        {
        }
        public override string Description =>
            "Scorrevole ribalta con laterale apribile" +
            base.ForcedMechanismText;
        protected override long? ClampMinValue => 2500000;
        public override int Order => 90;
        public override string[] MaterialForProduct => base.GetNotAntaMaxAndNotScorrevoleProductCodes();
    }
}
