﻿namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class VAS : DoubleDimMaterial
    {
        public VAS(long height_mm, long width_mm) : base(height_mm, width_mm)
        {
        }
        public override string Description => "Vasistas";
        protected override long? ClampMinValue => 1500000;
        public override int Order => 50;
        public override string[] MaterialForProduct => base.GetNotAntaMaxAndNotScorrevoleProductCodes();
    }
}
