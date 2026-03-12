using DomainModel.Classes;
using DomainModel.Services;
using Microsoft.AspNetCore.Mvc;

namespace InnovaPrev.Controllers
{
    [Route("api/pdf")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly IPdfReportGenerator pdfReportGenerator;

        public PdfController(IPdfReportGenerator pdfReportGenerator)
        {
            this.pdfReportGenerator = pdfReportGenerator ?? throw new ArgumentNullException(nameof(pdfReportGenerator));
        }

        // POST api/<PdfController>
        [HttpPost]
        public IActionResult Post([FromBody] Project project)
        {
            var buffer = pdfReportGenerator.Generate(project);
            var streamResult = new MemoryStream(buffer);
            return File(streamResult, "application/pdf", $"preventivo{DateTime.Now.ToString("_yyyyMMdd_HHmmss")}.pdf");
        }
    }
}
