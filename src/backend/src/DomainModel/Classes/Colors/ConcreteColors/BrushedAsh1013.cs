using System.Reflection.Metadata.Ecma335;
using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Colors.ConcreteColors
{
    public class BrushedAsh1013 : BrushedDecapeToulipierWoodColor
    { 
        public override string Description => "Frassino Spazzolato 1013";
        public override decimal Price_sqm => 84M;
        public override int Order => 300;
    }
}
