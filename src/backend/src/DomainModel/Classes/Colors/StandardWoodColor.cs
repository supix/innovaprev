using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Colors
{
    public abstract class StandardWoodColor : WoodColor
    {
        public override sealed decimal GetPrice_CAS_m(IColorVisitor visitor)
        {
            return visitor.GetPrice_CAS_Standard_m();
        }
        public override sealed decimal GetPrice_COP_m(IColorVisitor visitor)
        {
            return visitor.GetPrice_COP_Standard_m();
        }
        public override sealed decimal GetPrice_FRO_m(IColorVisitor visitor)
        {
            return visitor.GetPrice_FRO_Standard_m();
        }
    }
}
