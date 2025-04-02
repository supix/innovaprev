using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Colors.ConcreteColors
{
    public class CherryColoredAsh : StandardWoodColor { 
        public override string Description => "Frassino Tinto Ciliegio";
        public override decimal Price_sqm => 0M;
        public override int Order => 230;
    }

}
