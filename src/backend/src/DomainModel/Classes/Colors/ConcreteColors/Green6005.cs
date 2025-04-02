using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Colors.ConcreteColors
{
    public class Green6005 : AluminumColor
    {
        public override string Description => "Verde 6005";

        public override decimal Price_sqm => 18M;

        public override int Order => 20;
    }

}
