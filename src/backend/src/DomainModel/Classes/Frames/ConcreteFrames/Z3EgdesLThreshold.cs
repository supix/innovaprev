namespace DomainModel.Classes.Frames.ConcreteFrames
{
    public class Z3EgdesLThreshold : AbstractWoodFrame
    {
        public override string Description => "3 lati Z soglia L";
        public override int Order => 50;
        public override decimal GetPrice_sqm()
        {
            return 8;
        }
    }
}
