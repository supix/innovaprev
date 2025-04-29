using DomainModel.Classes;
using DomainModel.Classes.Colors;
using DomainModel.Classes.Materials;
using DomainModel.Classes.Products;
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

                    page.Header().PaddingBottom(10).Element(ComposeHeader);
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
                row.RelativeItem().Component(new AddressComponent(string.Empty, project.SupplierData));

                row.ConstantItem(50);

                row.RelativeItem().Column(column =>
                {
                    column.Item().DefaultTextStyle(x => x.FontSize(12).SemiBold().FontColor(Colors.Black))
                        .Text(x =>
                        {
                            x.Span("Preventivo");
                            if (!string.IsNullOrWhiteSpace(project.ProductData.OrderNumber))
                                x.Span($" {project.ProductData.OrderNumber}");
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
            var coll = collectionProvider.Get();
            container.PaddingVertical(10).Column(column =>
            {
                column.Spacing(5);

                // Customer data
                column.Item().Row(row =>
                {
                    row.RelativeItem().Text(string.Empty);
                    row.RelativeItem()
                        .PaddingBottom(30)
                        .Component(new AddressComponent("DATI CLIENTE", project.CustomerData));
                });

                // Product
                var pd = project.ProductData;

                column.Item().Background(Colors.Grey.Lighten4).Padding(10).Row(row =>
                {
                    row.RelativeItem(2).PaddingRight(10).Component(new ProductDescriptionComponent(pd, coll));
                    row.RelativeItem(1).Image(imageProvider.Get(pd.Product, false));
                });

                // Details
                column.Item().PaddingTop(10).Background(Colors.Grey.Lighten2).Padding(2).AlignCenter().DefaultTextStyle(x => x.FontSize(8)).Text("MISURE");
                // windows data
                var idx = 0;
                foreach (var wd in project.WindowsData)
                {
                    var detailPrice = priceInfo.DetailPrices[idx];

                    // create product
                    var internalColor = ColorFactory.CreateByCode(pd.InternalColor);
                    var externalColor = ColorFactory.CreateByCode(pd.ExternalColor ?? pd.InternalColor);
                    var product = ProductFactory.CreateByCode(pd.Product, internalColor, externalColor);

                    // create material
                    long m1 = wd.Length != 0 ? wd.Length : wd.Height;
                    long m2 = wd.Width;
                    var material = MaterialFactory.CreateByCode(wd.WindowType, m1, m2, (wd.OpeningType != null && wd.OpeningType.Contains("SX")) ? "SX" : "DX", wd.GlassType == "GT_OPACO", wd.WireCover);

                    var component = PdfComponentFactory.CreateComponent(++idx, material, wd, detailPrice, product.TrimSectionVisible);
                    column.Item()
                        .BorderBottom(1)
                        .BorderColor(Colors.Grey.Lighten2)
                        .PaddingBottom(7)
                        .Component(component);
                }
                // custom data
                foreach (var cd in project.CustomData)
                {
                    column.Item()
                        .BorderBottom(1)
                        .BorderColor(Colors.Grey.Lighten2)
                        .PaddingBottom(5)
                        .Component(new CustomProductComponent(++idx, cd));
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
