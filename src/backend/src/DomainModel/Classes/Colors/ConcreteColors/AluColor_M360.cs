﻿using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Colors.ConcreteColors
{
    public class AluColor_M360 : AluminumColor
    {
        public override string Description => "Effetto Legno M360"; 
        public override decimal Price_sqm => 101M; 
        public override int Order => 120;
    }
}
