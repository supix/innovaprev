﻿namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class FIX : DoubleDimFixedMaterial
    {
        public FIX(long height_mm, long width_mm, string openingType, bool opaqueGlass, bool wireCover) : base(height_mm, width_mm, openingType, opaqueGlass, wireCover)
        {
        }
        public override string Description => "Fisso con fermavetro";
        protected override long? ClampMinValue => 1000000;
        public override int Order => 0;
        public override string[] MaterialForProduct => base.GetNotAntaMaxAndNotScorrevoleProductCodes();
    }
}
