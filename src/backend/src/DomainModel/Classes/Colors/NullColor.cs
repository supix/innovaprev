using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Colors
{
    public class NullColor : AbstractColor
    {
        public override string Description => throw new NotImplementedException();

        public override decimal Price_sqm => throw new NotImplementedException();

        public override int Order => throw new NotImplementedException();

        public override decimal GetPrice_CAS_m(IColorVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public override decimal GetPrice_COP_m(IColorVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public override decimal GetPrice_FRO_m(IColorVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
