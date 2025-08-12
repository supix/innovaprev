using DomainModel.Classes.Frames;

namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class SRAF : ShiftableFlap
    {
        public SRAF(long height_mm, long width_mm, string openingType, bool opaqueGlass, bool wireCover, IFrame frameType) : base(height_mm, width_mm, openingType, opaqueGlass, wireCover, frameType)
        {
        }
        public override string Description =>
            "Scorrevole ribalta con anta fissa" +
            base.ForcedMechanismText;
        protected override long? ClampMinValue => 2500000;
        public override int Order => 80;
        public override string[] MaterialForProduct => base.GetNotAntaMaxAndNotScorrevoleProductCodes();
    }
}
