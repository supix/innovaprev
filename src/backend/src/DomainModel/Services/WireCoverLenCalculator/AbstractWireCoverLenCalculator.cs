using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Services.WireCoverLenCalculator
{
    abstract class AbstractWireCoverLenCalculator : IWireCoverLenCalculator
    {
        protected abstract long GetBarLen();
        public long GetLen(long height_mm, long width_mm)
        {
            long barLen = this.GetBarLen();
            decimal totLen = (height_mm + 100) * 2 + width_mm + 100;
            int numberOfBars = (int)Math.Ceiling(totLen / barLen);
            return barLen * numberOfBars;
        }
    }
}
