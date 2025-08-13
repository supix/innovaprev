using DomainModel.Classes.Products.Visitors;

namespace DomainModel.Classes.Frames.ConcreteFrames
{
    public class Z3EgdesLThreshold : AbstractWoodFrame
    {
        public override string Description => "3 lati Z soglia L";
        public override int Order => 50;
        public override decimal GetPrice_sqm(IFrameVisitor visitor)
        {
            return visitor.GetPrice_Z3EgdesLThreshold_sqm();
        }
    }
}
