using DomainModel.Classes;
using DomainModel.Services.PriceCalculator;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace PdfQuote
{
    internal class ArticleComponent : IComponent
    {
        private readonly int index;
        private readonly DetailPrice detailPrice;
        private readonly WindowsData windowsData;

        public ArticleComponent(int index, WindowsData windowsData, DetailPrice detailPrice)
        {
            this.index = index;
            this.windowsData = windowsData ?? throw new ArgumentNullException(nameof(windowsData));
            this.detailPrice = detailPrice ?? throw new ArgumentNullException(nameof(detailPrice));
        }

        public void Compose(IContainer container)
        {
            container.Column(c =>
            {
                // Lengths
                c.Item().Row(r =>
                {
                    r.ConstantItem(25).Text($"#{index.ToString()}").Bold();
                    r.RelativeItem(3).Text($"L (mm): {this.windowsData.Width}");
                    r.RelativeItem(3).Text($"H (mm): {this.windowsData.Height}");
                    r.RelativeItem(2).Text($"Q.tà: {this.windowsData.Quantity}");
                    r.RelativeItem(1).AlignRight().Text($"sx: {this.windowsData.LeftTrim}");
                    r.RelativeItem(1).AlignRight().Text($"dx: {this.windowsData.RightTrim}");
                    r.RelativeItem(1).AlignRight().Text($"sop: {this.windowsData.UpperTrim}");
                    r.RelativeItem(1).AlignRight().Text($"sot: {this.windowsData.BelowThreshold}");
                });

                // Types
                c.Item().DefaultTextStyle(x => x.FontSize(8)).Row(r =>
                {
                    r.ConstantItem(25).Text(string.Empty);
                    r.RelativeItem(3).Text($"Tipologia: {this.windowsData.WindowType}");
                    r.RelativeItem(3).Text($"Apertura (vista interna): {this.windowsData.OpeningType}");
                    r.RelativeItem(2).Text($"Vetro: {this.windowsData.GlassType}");
                    r.RelativeItem(2).AlignRight().Text($"Traverso: {this.windowsData.Crosspiece}");
                });

                // Prices
                c.Item().Row(r =>
                {
                    r.RelativeItem(4).Text(string.Empty);
                    r.RelativeItem(2).AlignRight().Text($"Prezzo mq: {this.detailPrice.UnitPrice:n}€");
                    r.RelativeItem(2).AlignRight().Text($"Tot.: {this.detailPrice.NetPrice:n}€").Bold();
                });
            });
        }
    }
}
