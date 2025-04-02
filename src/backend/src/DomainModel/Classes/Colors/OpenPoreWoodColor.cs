using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Colors
{
    public abstract class OpenPoreWoodColor : WoodColor
    {
        public override sealed decimal GetPrice_CAS_m(IColorVisitor visitor)
        {
            return visitor.GetPrice_CAS_OpenPore_m();
        }
        public override sealed decimal GetPrice_COP_m(IColorVisitor visitor)
        {
            return visitor.GetPrice_COP_OpenPore_m();
        }
        public override sealed decimal GetPrice_FRO_m(IColorVisitor visitor)
        {
            return visitor.GetPrice_FRO_OpenPore_m();
        }
    }
}
