using System;
using DomainModel.Classes;
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

        public QuoteTemplate(Project project, PriceInfo priceInfo, ICollectionProvider collectionProvider)
        {
            this.project = project ?? throw new ArgumentNullException(nameof(project));
            this.priceInfo = priceInfo ?? throw new ArgumentNullException(nameof(priceInfo));
            this.collectionProvider = collectionProvider ?? throw new ArgumentNullException(nameof(collectionProvider));
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
                column.Item().Background(Colors.Grey.Lighten4).Padding(5).Column(c => {
                    c.Item().PaddingBottom(10).Text(coll.Product.Single(p => p.Id == pd.Product).Desc).FontSize(14).AlignCenter();
                    c.Item().DefaultTextStyle(x => x.FontSize(9)).Row(row =>
                    {
                        row.RelativeItem(1).Column(c =>
                        {
                            c.Item().Text($"Tipo Anta");
                            c.Item().PaddingLeft(5).Text($"Fermavetro: {(pd.GlassStopper ? "SI" : "NO")}");
                            c.Item().PaddingLeft(5).Text($"Infilo: {(pd.WindowSlide ? "SI" : "NO")}");
                        });
                        row.RelativeItem(1).Column(c => {
                            c.Item().Text($"Colore");
                            c.Item().PaddingLeft(5).Text($"Interno: {coll.InternalColors.Single(ic => ic.Id == pd.InternalColor).Desc}");
                            c.Item().PaddingLeft(5).Text($"Esterno: {coll.ExternalColors.Single(ec => ec.Id == pd.ExternalColor).Desc}");
                            c.Item().PaddingLeft(5).Text($"Accessori: {coll.AccessoryColors.Single(ac => ac.Id == pd.AccessoryColor).Desc}");
                        });
                        row.RelativeItem(1).AlignRight().Text($"Zona climatica: {coll.ClimateZones.Single(cz => cz.Id == pd.ClimateZone).Desc}");
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
                        c.Item().Text($"Imponibile: {priceInfo.Total:n}€").AlignRight();
                        c.Item().Text($"Imposta: {priceInfo.Tax:n}€").AlignRight();
                        c.Item().Text($"TOTALE: {priceInfo.GrandTotal:n}€").FontSize(14).Bold().AlignRight();
                    });
            });
        }
    }
}
