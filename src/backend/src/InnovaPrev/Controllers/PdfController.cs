using DomainModel.Classes;
using Microsoft.AspNetCore.Mvc;
using PdfQuote;

namespace InnovaPrev.Controllers
{
    [Route("api/pdf")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        // POST api/<PdfController>
        [HttpPost]
        public IActionResult Post([FromBody] Project project)
        {
            var pdfGenerator = new Generator();
            var buffer = pdfGenerator.Generate(project);
            var streamResult = new MemoryStream(buffer);
            return File(streamResult, "application/pdf", $"preventivo{ DateTime.Now.ToString("_yyyyMMdd_HHmmss") }.pdf");
        }
    }
}
