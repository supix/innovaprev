﻿namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class SLA : DoubleDimMaterial
    {
        public SLA(long height_mm, long width_mm, string openingType, bool opaqueGlass, bool wireCover) : base(height_mm, width_mm, openingType, opaqueGlass, wireCover)
        {
        }
        public override string Description => "Sopraluce apribile";
        protected override long? ClampMinValue => 1500000;
        public override int Order => 140;
        public override string[] MaterialForProduct => base.GetNotAntaMaxAndNotScorrevoleProductCodes();
    }

}
