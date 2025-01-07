using DomainModel.Classes;
using DomainModel.Services.CollectionsProvider;
using DomainModel.Services.PriceCalculator;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace PdfQuote
{
    internal class StandardProductComponent : IComponent
    {
        private readonly int index;
        private readonly DetailPrice detailPrice;
        private readonly Collections coll;
        private readonly bool trimSectionVisible;
        private readonly WindowsData windowsData;

        public StandardProductComponent(int index, WindowsData windowsData, DetailPrice detailPrice, Collections coll, bool trimSectionVisible)
        {
            this.index = index;
            this.windowsData = windowsData ?? throw new ArgumentNullException(nameof(windowsData));
            this.detailPrice = detailPrice ?? throw new ArgumentNullException(nameof(detailPrice));
            this.coll = coll ?? throw new ArgumentNullException(nameof(coll));
            this.trimSectionVisible = trimSectionVisible;
        }

        public void Compose(IContainer container)
        {
            container.Column(c =>
            {
                c.Spacing(3);
                // Lengths
                c.Item().Row(r =>
                {
                    r.ConstantItem(25).Text($"#{this.index.ToString()}");
                    r.RelativeItem(2).Text($"q.tà: {this.windowsData.Quantity}");
                    r.RelativeItem(3).Text($"L (mm): {this.windowsData.Width}");
                    r.RelativeItem(3).Text($"H (mm): {this.windowsData.Height}");
                    r.RelativeItem(1).ShowIf(this.trimSectionVisible).AlignRight().Text($"sx: {this.windowsData.LeftTrim}");
                    r.RelativeItem(1).ShowIf(this.trimSectionVisible).AlignRight().Text($"dx: {this.windowsData.RightTrim}");
                    r.RelativeItem(1).ShowIf(this.trimSectionVisible).AlignRight().Text($"sop: {this.windowsData.UpperTrim}");
                    r.RelativeItem(1).ShowIf(this.trimSectionVisible).AlignRight().Text($"sot: {this.windowsData.BelowThreshold}");
                });

                // Types
                c.Item().DefaultTextStyle(x => x.FontSize(8)).Row(r =>
                {
                    r.ConstantItem(25).Text(string.Empty);
                    r.RelativeItem(6).Text($"Tipol.: {coll.WindowTypes.Single(wt => wt.Id == this.windowsData.WindowType).Desc}");
                    r.RelativeItem(3).Text($"Apert. (vista interna): {coll.OpeningTypes.Single(ot => ot.Id == this.windowsData.OpeningType).Desc}");
                    r.RelativeItem(2).Text($"Vetro: {coll.GlassTypes.Single(gt => gt.Id == this.windowsData.GlassType).Desc}");
                    r.RelativeItem(2).AlignRight().Text($"Trav.: {coll.Crosspieces.Single(cp => cp.Id == this.windowsData.Crosspiece).Desc}");
                });

                // Prices
                c.Item().Row(r =>
                {
                    r.RelativeItem(4).Text(string.Empty);
                    r.RelativeItem(2).AlignRight().Text($"Prezzo mq: {this.detailPrice.UnitPrice:c}");
                    r.RelativeItem(2).AlignRight().Text($"{this.detailPrice.NetPrice:c}").Bold();
                });
            });
        }
    }
}
