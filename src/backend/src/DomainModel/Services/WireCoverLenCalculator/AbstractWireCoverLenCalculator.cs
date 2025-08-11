namespace DomainModel.Services.WireCoverLenCalculator
{
    abstract class AbstractWireCoverLenCalculator : IWireCoverLenCalculator
    {
        protected abstract long GetBarLen();
        public long GetLen(long height_mm, long width_mm)
        {
            long barLen = GetBarLen();
            decimal totLen = (height_mm + 100) * 2 + width_mm + 100;
            int numberOfBars = (int)Math.Ceiling(totLen / barLen);
            return barLen * numberOfBars;
        }
    }
}
