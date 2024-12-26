using DomainModel.Classes;
using DomainModel.Services.PriceCalculator;
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
            var priceCalculator = new PriceCalculator(project.ProductData, project.WindowsData);
            var priceInfo = priceCalculator.getPrices();
            var doc = new QuoteTemplate(project, priceInfo);
            return Document.Create(container => doc.Compose(container)).GeneratePdf();
        }
    }
}