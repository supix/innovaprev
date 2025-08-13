using DomainModel.Classes.Products.Visitors;

namespace DomainModel.Classes.Colors
{
    public abstract class BrushedDecapeToulipierWoodColor : WoodColor
    {
        public override sealed decimal GetPrice_CAS_m(IColorVisitor visitor)
        {
            return visitor.GetPrice_CAS_BrushedDecapeToulipier_m();
        }
        public override sealed decimal GetPrice_COP_m(IColorVisitor visitor)
        {
            return visitor.GetPrice_COP_BrushedDecapeToulipier_m();
        }
        public override sealed decimal GetPrice_FRO_m(IColorVisitor visitor)
        {
            return visitor.GetPrice_FRO_BrushedDecapeToulipier_m();
        }
    }
}
