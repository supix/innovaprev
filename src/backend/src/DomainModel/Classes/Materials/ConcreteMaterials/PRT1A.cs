using DomainModel.Classes.Frames;

namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class PRT1A : NeedsLockMaterial
    {
        public PRT1A(long height_mm, long width_mm, string openingType, bool opaqueGlass, bool wireCover, IFrame frameType) : base(height_mm, width_mm, openingType, opaqueGlass, wireCover, frameType)
        {
        }
        public override string Description => "Portoncino 1 anta";
        protected override long? ClampMinValue => 1500000;
        public override int Order => 110;
        public override string[] MaterialForProduct => base.GetAntaMaxProductCodes().Concat([ "AATT", "IPC" ]).ToArray();
        public override bool ForceAntaMaxPrice()
        {
            return true;
        }
    }
}
