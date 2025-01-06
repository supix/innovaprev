using System;
using System.Reflection;
using DomainModel.Classes;
using DomainModel.Services;
using DomainModel.Services.CollectionsProvider;
using DomainModel.Services.PriceCalculator;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PdfQuote
{
    internal class QuoteTemplate : IDocument
    {
        private readonly Project project;
        private readonly PriceInfo priceInfo;
        private readonly ICollectionProvider collectionProvider;
        private readonly IProductImageProvider imageProvider;

        public QuoteTemplate(Project project, PriceInfo priceInfo, ICollectionProvider collectionProvider, IProductImageProvider imageProvider)
        {
            this.project = project ?? throw new ArgumentNullException(nameof(project));
            this.priceInfo = priceInfo ?? throw new ArgumentNullException(nameof(priceInfo));
            this.collectionProvider = collectionProvider ?? throw new ArgumentNullException(nameof(collectionProvider));
            this.imageProvider = imageProvider ?? throw new ArgumentNullException(nameof(imageProvider));
        }
        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(50);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);
                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.CurrentPageNumber();
                        x.Span(" / ");
                        x.TotalPages();
                    });
                });
        }
        private void ComposeHeader(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Component(new AddressComponent(string.Empty, this.project.SupplierData));
                
                row.ConstantItem(50);
                
                row.RelativeItem().Column(column =>
                {
                    column.Item().DefaultTextStyle(x => x.FontSize(12).SemiBold().FontColor(Colors.Black))
                        .Text(x =>
                        {
                            x.Span("Preventivo");
                            if (!string.IsNullOrWhiteSpace(this.project.ProductData.OrderNumber))
                                x.Span($" #{this.project.ProductData.OrderNumber}");
                        });

                    column.Item().Text(text =>
                    {
                        text.Span("Data: ").SemiBold();
                        text.Span($"{DateTime.Now:d}");
                    });
                });
            });
        }

        private void ComposeContent(IContainer container)
        {
            var coll = this.collectionProvider.Get();
            container.PaddingVertical(10).Column(column =>
            {
                column.Spacing(5);

                // Customer data
                column.Item().Row(row =>
                {
                    row.RelativeItem().Text(string.Empty);
                    row.RelativeItem()
                        .PaddingBottom(10)
                        .Component(new AddressComponent("DATI CLIENTE", project.CustomerData));
                });

                // Product
                var pd = this.project.ProductData;

                column.Item().Background(Colors.Grey.Lighten4).Padding(10).Row(row =>
                {
                    row.RelativeItem(2).PaddingRight(10).Component(new ProductDescriptionComponent(pd, coll));
                    row.RelativeItem(1).Image(this.imageProvider.Get(pd.Product, false));
                });

                // Measures
                column.Item().PaddingTop(10).Background(Colors.Grey.Lighten2).Padding(2).AlignCenter().DefaultTextStyle(x => x.FontSize(8)).Text("MISURE");
                var idx = 0;
                foreach (var wd in this.project.WindowsData)
                {
                    var detailPrice = priceInfo.DetailPrices[idx];
                    column.Item()
                        .PaddingBottom(3)
                        .BorderBottom(1)
                        .BorderColor(Colors.Grey.Lighten2)
                        .Component(new ArticleComponent(++idx, wd, detailPrice, collectionProvider));
                }

                // Total
                column.Item()
                    .PaddingTop(10)
                    .Background(Colors.Grey.Lighten3)
                    .Padding(2)
                    .Column(c =>
                    {
                        c.Item().Text($"TOTALE: {priceInfo.Total:c}").FontSize(14).Bold().AlignRight();
                        c.Item().Text($"Imposta: {priceInfo.Tax:c}").AlignRight();
                    });
            });
        }
    }
}
