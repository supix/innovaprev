﻿namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class F2A : DoubleDimMaterial
    {
        public F2A(long height_mm, long width_mm) : base(height_mm, width_mm)
        {
        }

        public override string Description => "Finestra 2 Ante";

        protected override long? ClampMinValue => 1800000;
        public override int Order => 60;
        public override string[] MaterialForProduct => base.GetNotAntaMaxAndNotScorrevoleProductCodes();
    }
}
