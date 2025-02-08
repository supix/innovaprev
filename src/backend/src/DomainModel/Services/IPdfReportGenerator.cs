using DomainModel.Classes;

namespace DomainModel.Services
{
    public interface IPdfReportGenerator
    {
        public byte[] Generate(Project project);
    }
}
