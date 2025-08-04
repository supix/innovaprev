using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Classes.Frames
{
    public abstract class AbstractFrame : IFrame
    {
        public string Code => GetType().Name;
        public abstract string Description { get; }
    }
}
