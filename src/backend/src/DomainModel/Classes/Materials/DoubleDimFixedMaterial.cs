﻿using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Materials
{
    public abstract class DoubleDimFixedMaterial : DoubleDimMaterial
    {
        protected DoubleDimFixedMaterial(long height_mm, long width_mm, bool opaqueGlass) : base(height_mm, width_mm, opaqueGlass)
        {
        }
        public override decimal GetPrice(IMaterialVisitor visitor)
        {
            return visitor.GetPrice_DoubleDimFixed(this, GetAllowedArea_sqmm);
        }
        public override sealed bool openingTypeVisible => false;
    }
}
