using DomainModel.Classes.Products.Visitors;

namespace DomainModel.Classes.Frames.ConcreteFrames
{
    public class NullFrame : AbstractFrame
    {
        public override string Description => string.Empty;

        public override int Order => 9999;

        public override string[] FrameForProduct => [];

        public override decimal GetPrice_sqm(IFrameVisitor visitor)
        {
            return visitor.GetPrice_NullFrame_sqm();
        }
    }
}
