﻿namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class F2A : DoubleDimMaterial
    {
        public F2A(long height_mm, long width_mm, string openingType, bool opaqueGlass, bool wireCover) : base(height_mm, width_mm, openingType, opaqueGlass, wireCover)
        {
        }

        public override string Description => "Finestra 2 Ante";

        protected override long? ClampMinValue => 1800000;
        public override int Order => 60;
        public override string[] MaterialForProduct => base.GetNotAntaMaxAndNotScorrevoleProductCodes();
    }
}
