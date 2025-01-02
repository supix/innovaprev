using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Classes
{
    public class CustomData
    {
       public required string Description { get; set; }
       public required string Quantity { get; set; }
       public required decimal Price { get; set; }
    }
}
