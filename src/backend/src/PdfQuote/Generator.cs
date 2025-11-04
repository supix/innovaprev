using DomainModel.Classes;
using DomainModel.Services;
using DomainModel.Services.CollectionsProvider;
using DomainModel.Services.PriceCalculator;
using DomainModel.Services.WindowImageRenderer;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace PdfQuote
{
    public class Generator : IPdfReportGenerator
    {
        private static Random random = new Random();
        private readonly IPriceCalculator priceCalculator;
        private readonly ICollectionProvider collectionProvider;
        private readonly IProductImageProvider imageProvider;
        private readonly IWindowImageRenderer wir;

        public Generator(IPriceCalculator priceCalculator, ICollectionProvider collectionProvider, IProductImageProvider imageProvider, IWindowImageRenderer wir)
        {
            this.priceCalculator = priceCalculator ?? throw new ArgumentNullException(nameof(priceCalculator));
            this.collectionProvider = collectionProvider ?? throw new ArgumentNullException(nameof(collectionProvider));
            this.imageProvider = imageProvider ?? throw new ArgumentNullException(nameof(imageProvider));
            this.wir = wir ?? throw new ArgumentNullException(nameof(wir));
        }

        public byte[] Generate(Project project)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var priceInfo = priceCalculator.getPrices(project.ProductData, project.WindowsData, project.CustomData);
            var doc = new QuoteTemplate(project, priceInfo, collectionProvider, imageProvider, wir);
            var quotation = Document.Create(container => doc.Compose(container)).GeneratePdf();
            var quotationFileName = GetRandomFileName();
            var fullQuotationFileName = GetRandomFileName();
            try
            {
                using (var s = new FileStream(quotationFileName, FileMode.CreateNew))
                {
                    s.Write(quotation);
                }
                var headingFile = Path.Combine(AppContext.BaseDirectory, "heading.pdf");
                DocumentOperation
                    .LoadFile(headingFile)
                    .MergeFile(quotationFileName)
                    .Save(fullQuotationFileName);
                byte[] result = File.ReadAllBytes(fullQuotationFileName);
                return result;
            }
            finally
            {
                File.Delete(quotationFileName);
                File.Delete(fullQuotationFileName);
            }
        }

        private string GetRandomFileName()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var fileName = new char[20];

            for (int i = 0; i < fileName.Length; i++)
            {
                fileName[i] = chars[random.Next(chars.Length)];
            }

            return new string(fileName) + ".pdf";
        }
    }
}