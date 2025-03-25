using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Colors.ConcreteColors
{
    public class OakColoredAsh : StandardWoodColor { 
        public override string Description => "Frassino Tinto Rovere"; 
        public override decimal Price_sqm => 0M; 
        public override int Order => 240;
    }

}
