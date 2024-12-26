using System;
using DomainModel.Classes;
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

        public QuoteTemplate(Project project, PriceInfo priceInfo)
        {
            this.project = project ?? throw new ArgumentNullException(nameof(project));
            this.priceInfo = priceInfo ?? throw new ArgumentNullException(nameof(priceInfo));
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
                    column.Item()
                        .Text($"Preventivo #{this.project.ProductData.OrderNumber}")
                        .FontSize(12).SemiBold().FontColor(Colors.Black);

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
                column.Item().Background(Colors.Grey.Lighten4).Padding(5).Column(c => {
                    c.Item().PaddingBottom(10).Text(pd.Product).FontSize(14).AlignCenter();
                    c.Item().Row(row =>
                    {
                        row.RelativeItem(1).Column(c =>
                        {
                            c.Item().Text($"Tipo Anta");
                            c.Item().PaddingLeft(5).Text($"Fermavetro: {(pd.GlassStopper ? "SI" : "NO")}");
                            c.Item().PaddingLeft(5).Text($"Infilo: {(pd.WindowSlide ? "SI" : "NO")}");
                        });
                        row.RelativeItem(1).Column(c => {
                            c.Item().Text($"Colore");
                            c.Item().PaddingLeft(5).Text($"Interno: {pd.InternalColor}");
                            c.Item().PaddingLeft(5).Text($"Esterno: {pd.ExternalColor}");
                            c.Item().PaddingLeft(5).Text($"Accessori: {pd.AccessoryColor}");
                        });
                        row.RelativeItem(1).AlignRight().Text($"Zona climatica: {pd.ClimateZone}");
                    });

                    if (!string.IsNullOrWhiteSpace(pd.Notes))
                        c.Item().PaddingTop(10).Text($"Note: {pd.Notes}");
                });

                // Measures
                column.Item().PaddingTop(10).Background(Colors.Grey.Lighten2).Padding(2).AlignCenter().DefaultTextStyle(x => x.FontSize(8)).Text("MISURE");
                var idx = 0;
                foreach (var wd in this.project.WindowsData)
                {
                    var detailPrice = priceInfo.DetailPrices[idx];
                    column.Item().PaddingBottom(3).Component(new ArticleComponent(++idx, wd, detailPrice));
                }

                // Total
                column.Item()
                    .PaddingTop(10)
                    .Background(Colors.Grey.Lighten3)
                    .Padding(2)
                    .Column(c =>
                    {
                        c.Item().Text($"Imponibile: {priceInfo.Total:n}€").AlignRight();
                        c.Item().Text($"Imposta: {priceInfo.Tax:n}€").AlignRight();
                        c.Item().Text($"TOTALE: {priceInfo.GrandTotal:n}€").FontSize(14).Bold().AlignRight();
                    });
            });
        }
    }
}
