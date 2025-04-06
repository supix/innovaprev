using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Services.WireCoverLenCalculator
{
    class PvcWireCoverLenCalculator : AbstractWireCoverLenCalculator
    {
        protected override long GetBarLen() => 6500;
    }
}
