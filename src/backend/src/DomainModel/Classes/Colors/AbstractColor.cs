namespace DomainModel.Classes.Colors
{
    public abstract class AbstractColor : IColor
    {
        public string Code => GetType().Name;
        public abstract string Description { get; }
        public abstract decimal Price_mq { get; }
        public abstract int Order { get; }
    }
}
