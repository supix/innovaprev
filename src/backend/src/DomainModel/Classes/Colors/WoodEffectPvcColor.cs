using DomainModel.Classes.Products.Visitors;

namespace DomainModel.Classes.Colors
{
    public abstract class WoodEffectPvcColor : PvcColor
    {
        public override sealed decimal GetPrice_CAS_m(IColorVisitor visitor)
        {
            return visitor.GetPrice_CAS_PvcWoodEffect_m();
        }
        public override sealed decimal GetPrice_COP_m(IColorVisitor visitor)
        {
            return visitor.GetPrice_COP_PvcWoodEffect_m();
        }
        public override sealed decimal GetPrice_FRO_m(IColorVisitor visitor)
        {
            return visitor.GetPrice_FRO_PvcWoodEffect_m();
        }
    }
}
