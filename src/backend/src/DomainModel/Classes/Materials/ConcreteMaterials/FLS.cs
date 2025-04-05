﻿namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class FLS : DoubleDimFixedMaterial
    {
        public FLS(long height_mm, long width_mm, bool opaqueGlass) : base(height_mm, width_mm, opaqueGlass)
        {
        }

        public override string Description => "Fisso laterale sx";
        protected override long? ClampMinValue => 1000000;
        public override int Order => 160;
        public override string[] MaterialForProduct => base.GetNotAntaMaxAndNotScorrevoleProductCodes();
    }
}
