using DomainModel.Classes;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PdfQuote
{
    public class Generator
    {
        public byte[] Generate(Project project)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var doc = new QuoteTemplate(project);
            return Document.Create(container => doc.Compose(container)).GeneratePdf();
        }
    }
}