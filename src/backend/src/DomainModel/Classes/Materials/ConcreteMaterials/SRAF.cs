﻿namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class SRAF : DoubleDimMaterial
    {
        public SRAF(long height_mm, long width_mm, bool opaqueGlass) : base(height_mm, width_mm, opaqueGlass)
        {
        }
        public override string Description => "Scorrevole Ribalta con anta fissa";
        protected override long? ClampMinValue => 2500000;
        public override int Order => 80;
        public override string[] MaterialForProduct => base.GetNotAntaMaxAndNotScorrevoleProductCodes();
    }
}
