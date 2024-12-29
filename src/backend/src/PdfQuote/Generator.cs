using DomainModel.Classes;
using DomainModel.Services;
using DomainModel.Services.PriceCalculator;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace PdfQuote
{
    public class Generator : IPdfReportGenerator
    {
        private readonly IPriceCalculator priceCalculator;

        public Generator(IPriceCalculator priceCalculator)
        {
            this.priceCalculator = priceCalculator ?? throw new ArgumentNullException(nameof(priceCalculator));
        }

        public byte[] Generate(Project project)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var priceInfo = this.priceCalculator.getPrices(project.ProductData, project.WindowsData);
            var doc = new QuoteTemplate(project, priceInfo);
            return Document.Create(container => doc.Compose(container)).GeneratePdf();
        }
    }
}