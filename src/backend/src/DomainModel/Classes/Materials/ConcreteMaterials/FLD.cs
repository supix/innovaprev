﻿namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class FLD : DoubleDimFixedMaterial
    {
        public FLD(long height_mm, long width_mm) : base(height_mm, width_mm)
        {
        }

        public override string Description => "Fisso laterale dx";
        protected override long? ClampMinValue => 1000000;
        public override int Order => 150;
        public override string[] MaterialForProduct => base.GetNotAntaMaxAndNotScorrevoleProductCodes();
    }
}
