using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Classes.Materials
{
    public interface IMaterial
    {
        string Code { get; }
        string Description { get; }
        int NumberOfDimensions { get; }
    }
}
