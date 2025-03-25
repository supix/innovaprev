using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Colors.ConcreteColors
{
    public class WhiteWood : WoodEffectPvcColor
    {
        public override string Description => "Legno Bianco";
        public override decimal Price_sqm => 89M / 2;
        public override int Order => 360;
    }
}
