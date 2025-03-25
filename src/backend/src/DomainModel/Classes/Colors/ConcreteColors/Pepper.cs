﻿using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Colors.ConcreteColors
{
    public class Pepper : WoodEffectPvcColor {
        public override string Description => "Pepper ";
        public override decimal Price_sqm => 89M;
        public override int Order => 380;
    }
}
