﻿namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class PRT1A : NeedsLockMaterial
    {
        public PRT1A(long height_mm, long width_mm, bool opaqueGlass) : base(height_mm, width_mm, opaqueGlass)
        {
        }
        public override string Description => "Portoncino 1 anta";
        protected override long? ClampMinValue => 1500000;
        public override int Order => 110;
        public override string[] MaterialForProduct => base.GetAntaMaxProductCodes();
    }
}
