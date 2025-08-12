namespace DomainModel.Classes.Frames
{
    public abstract class AbstractFrame : IFrame
    {
        public string Code => GetType().Name;
        public abstract string Description { get; }
        public abstract int Order { get; }
        public abstract string[] FrameForProduct { get; }
        public virtual decimal GetPrice_sqm()
        {
            return 0;
        }
    }
}
