using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Colors.ConcreteColors
{
    public class Opaque9006 : AluminumColor { 
        public override string Description => "9006 Opaco"; 
        public override decimal Price_sqm => 18M; 
        public override int Order => 100;
    }

}
