using DomainModel.Classes;
using DomainModel.Classes.Materials;
using DomainModel.Services.PriceCalculator;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace PdfQuote
{
    internal class DoubleDimMaterialPdfComponent : IComponent
    {
        private readonly int index;
        private readonly DoubleDimMaterial ddm;
        private readonly WindowsData wd;
        private readonly DetailPrice detailPrice;
        private readonly bool trimSectionVisible;

        public DoubleDimMaterialPdfComponent(
            int index, 
            DoubleDimMaterial ddm, 
            WindowsData wd,
            DetailPrice detailPrice, 
            bool trimSectionVisible)
        {
            this.index = index;
            this.ddm = ddm;
            this.wd = wd;
            this.detailPrice = detailPrice;
            this.trimSectionVisible = trimSectionVisible;
        }

        public void Compose(IContainer container)
        {
            container.Column(c =>
            {
                c.Spacing(3);

                // Type
                c.Item().DefaultTextStyle(x => x.FontSize(16)).Row(r =>
                {
                    r.ConstantItem(25).Text($"{index.ToString()}.");
                    r.RelativeItem(1).Text($"{ddm.Description}");
                });

                // Lengths
                c.Item().Row(r =>
                {
                    r.ConstantItem(25).Text(string.Empty);
                    r.RelativeItem(2).Text($"q.tà: {this.wd.Quantity}");

                    // Height + Width or Length
                    r.RelativeItem(3).Text($"L (mm): {ddm.Width_mm}");
                    r.RelativeItem(3).Text($"H (mm): {ddm.Height_mm}");

                    r.RelativeItem(1).ShowIf(trimSectionVisible).AlignRight().Text($"sx: {wd.LeftTrim}");
                    r.RelativeItem(1).ShowIf(trimSectionVisible).AlignRight().Text($"dx: {wd.RightTrim}");
                    r.RelativeItem(1).ShowIf(trimSectionVisible).AlignRight().Text($"sop: {wd.UpperTrim}");
                    r.RelativeItem(1).ShowIf(trimSectionVisible).AlignRight().Text($"sot: {wd.BelowThreshold}");
                });

                // General properties
                c.Item().Row(r =>
                {
                    r.ConstantItem(25).Text(string.Empty);
                    r.RelativeItem(3).ShowIf(ddm.openingTypeVisible).Text($"Apert. (vista interna): {ddm.OpeningType}");
                    r.RelativeItem(3).Text($"Vetro: {(ddm.OpaqueGlass ? "Opaco" : "Trasparente")}");
                    r.RelativeItem(3).Text($"Coprifilo: {(ddm.WireCover ? "SI" : "NO")}");
                });

                // Prices
                c.Item().Row(r =>
                {
                    r.RelativeItem(4).Text(string.Empty);
                    r.RelativeItem(2).AlignRight().Text($"{detailPrice.NetPrice:c}").Bold();
                });
            });
        }
    }
}
