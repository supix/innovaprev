namespace DomainModel.Services.WireCoverLenCalculator
{
    class WoodWireCoverLenCalculator : AbstractWireCoverLenCalculator
    {
        protected override long GetBarLen() => 3000;
    }
}
