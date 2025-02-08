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
                    r.ConstantItem(25).Text($"#{index.ToString()}");
                    r.RelativeItem(2).Text($"q.tà: {windowsData.Quantity}");
                    r.RelativeItem(3).Text($"L (mm): {windowsData.Width}");
                    r.RelativeItem(3).Text($"H (mm): {windowsData.Height}");
                    r.RelativeItem(1).ShowIf(trimSectionVisible).AlignRight().Text($"sx: {windowsData.LeftTrim}");
                    r.RelativeItem(1).ShowIf(trimSectionVisible).AlignRight().Text($"dx: {windowsData.RightTrim}");
                    r.RelativeItem(1).ShowIf(trimSectionVisible).AlignRight().Text($"sop: {windowsData.UpperTrim}");
                    r.RelativeItem(1).ShowIf(trimSectionVisible).AlignRight().Text($"sot: {windowsData.BelowThreshold}");
                });

                // Types
                c.Item().DefaultTextStyle(x => x.FontSize(8)).Row(r =>
                {
                    r.ConstantItem(25).Text(string.Empty);
                    r.RelativeItem(6).Text($"Tipol.: {coll.WindowTypes.Single(wt => wt.Id == windowsData.WindowType).Desc}");
                    r.RelativeItem(3).Text($"Apert. (vista interna): {coll.OpeningTypes.Single(ot => ot.Id == windowsData.OpeningType).Desc}");
                    r.RelativeItem(2).Text($"Vetro: {coll.GlassTypes.Single(gt => gt.Id == windowsData.GlassType).Desc}");
                    r.RelativeItem(2).AlignRight().Text($"Trav.: {coll.Crosspieces.Single(cp => cp.Id == windowsData.Crosspiece).Desc}");
                });

                // Prices
                c.Item().Row(r =>
                {
                    r.RelativeItem(4).Text(string.Empty);
                    r.RelativeItem(2).AlignRight().Text($"Prezzo mq: {detailPrice.UnitPrice:c}");
                    r.RelativeItem(2).AlignRight().Text($"{detailPrice.NetPrice:c}").Bold();
                });
            });
        }
    }
}
