using DomainModel.Classes;
using DomainModel.Services;
using DomainModel.Services.CollectionsProvider;
using DomainModel.Services.PriceCalculator;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace PdfQuote
{
    public class Generator : IPdfReportGenerator
    {
        private readonly IPriceCalculator priceCalculator;
        private readonly ICollectionProvider collectionProvider;

        public Generator(IPriceCalculator priceCalculator, ICollectionProvider collectionProvider)
        {
            this.priceCalculator = priceCalculator ?? throw new ArgumentNullException(nameof(priceCalculator));
            this.collectionProvider = collectionProvider ?? throw new ArgumentNullException(nameof(collectionProvider));
        }

        public byte[] Generate(Project project)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var priceInfo = this.priceCalculator.getPrices(project.ProductData, project.WindowsData);
            var doc = new QuoteTemplate(project, priceInfo, this.collectionProvider);
            return Document.Create(container => doc.Compose(container)).GeneratePdf();
        }
    }
}