using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Services.WindowImageRenderer
{
    public interface IWindowImageRenderer
    {
        bool CanRender(string materialType);
        byte[] Render(long height_mm, long witdh_mm, string materialType, bool wireCover, string openingType, string glassType);
    }
}
