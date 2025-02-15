﻿using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class FIX : DoubleDimFixedMaterial
    {
        public FIX(long height_mm, long width_mm) : base(height_mm, width_mm)
        {
        }
        public override string Description => "Fisso con fermavetro";
        protected override long? ClampMinValue => 1500000;
        public override int Order => 0;
    }
}
