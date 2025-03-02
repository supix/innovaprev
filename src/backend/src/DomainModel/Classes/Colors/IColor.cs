using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Classes.Colors
{
    public interface IColor
    {
        string Code { get; }
        string Description { get; }
        decimal Price_mq { get; }
        int Order { get; }
        string[] InternalColorForProducts { get; }
        string[] ExternalColorForProducts { get; }
    }
}
