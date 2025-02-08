using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Classes.Products
{
    public abstract class AbstractProduct : IProduct
    {
        public string Code => this.GetType().Name;
        public abstract string Description { get; }
        public abstract bool TrimSectionVisible { get; }
        public abstract string ExtendedDescriptionTitle { get; }
        public abstract string ExtendedDescription { get; }
        public abstract decimal StandardPrice { get; }
        public abstract int Order { get; }
    }
}
