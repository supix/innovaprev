using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Services.WireCoverLenCalculator
{
    public interface IWireCoverLenCalculator
    {
        long GetLen(long height_mm, long width_mm);
    }
}
