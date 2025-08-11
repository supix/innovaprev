namespace DomainModel.Services.WireCoverLenCalculator
{
    class PvcWireCoverLenCalculator : AbstractWireCoverLenCalculator
    {
        protected override long GetBarLen() => 6500;
    }
}
