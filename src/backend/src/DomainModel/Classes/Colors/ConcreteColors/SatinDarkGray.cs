using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Colors.ConcreteColors
{
    public class SatinDarkGray : AluminumColor { 
        public override string Description => "Grigio Scuro Satinato "; 
        public override decimal Price_sqm => 18M; 
        public override int Order => 80;
    }

}
