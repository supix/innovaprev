namespace DomainModel.Classes.Colors
{
    public class NullColor : AbstractColor
    {
        public override string Description => throw new NotImplementedException();

        public override decimal Price_sqm => throw new NotImplementedException();

        public override int Order => throw new NotImplementedException();
    }
}
