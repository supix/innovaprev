using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Classes.Frames
{
    public interface IFrame
    {
        string Code { get; }
        string Description { get; }
    }
}
