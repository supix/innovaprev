using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Classes;

namespace DomainModel.Services
{
    public interface IPdfReportGenerator
    {
        public byte[] Generate(Project project);
    }
}
