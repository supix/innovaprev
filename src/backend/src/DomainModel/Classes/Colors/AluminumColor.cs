using DomainModel.Classes.Products.Visitors;

namespace DomainModel.Classes.Colors
{
    public abstract class AluminumColor : AbstractColor
    {
        public override sealed decimal GetPrice_CAS_m(IColorVisitor visitor)
        {
            throw new NotImplementedException();
        }
        public override sealed decimal GetPrice_COP_m(IColorVisitor visitor)
        {
            throw new NotImplementedException();
        }
        public override sealed decimal GetPrice_FRO_m(IColorVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
