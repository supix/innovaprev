using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Colors
{
    public abstract class WhitePvcColor : AbstractColor
    {
        public override sealed decimal GetPrice_CAS_m(IColorVisitor visitor)
        {
            return visitor.GetPrice_CAS_PvcWhite_m();
        }
        public override sealed decimal GetPrice_COP_m(IColorVisitor visitor)
        {
            return visitor.GetPrice_COP_PvcWhite_m();
        }
        public override sealed decimal GetPrice_FRO_m(IColorVisitor visitor)
        {
            return visitor.GetPrice_FRO_PvcWhite_m();
        }
    }
}
