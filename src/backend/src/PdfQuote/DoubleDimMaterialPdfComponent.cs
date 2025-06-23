using DomainModel.Classes;
using DomainModel.Classes.Materials;
using DomainModel.Services.PriceCalculator;
using DomainModel.Services.WindowImageRenderer;
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
        private readonly IWindowImageRenderer wir;

        public DoubleDimMaterialPdfComponent(
            int index, 
            DoubleDimMaterial ddm, 
            WindowsData wd,
            DetailPrice detailPrice, 
            bool trimSectionVisible,
            IWindowImageRenderer wir)
        {
            this.index = index;
            this.ddm = ddm ?? throw new ArgumentNullException(nameof(ddm));
            this.wd = wd ?? throw new ArgumentNullException(nameof(wd));
            this.detailPrice = detailPrice ?? throw new ArgumentNullException(nameof(detailPrice));
            this.trimSectionVisible = trimSectionVisible;
            this.wir = wir ?? throw new ArgumentNullException(nameof(wir));
        }

        public void Compose(IContainer container)
        {
            container.Row(row => {
                row.Spacing(10);

                if (wir.CanRender(wd.WindowType))
                    row.ConstantItem(100).Image(wir.Render(wd.Height, wd.Width, wd.WindowType, wd.WireCover, wd.OpeningType!, wd.GlassType!));
                else
                    row.ConstantItem(100).Text(string.Empty);

                    row.RelativeItem().Column(c =>
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
                            r.RelativeItem(2).Text($"L (mm): {ddm.Width_mm}");
                            r.RelativeItem(2).Text($"H (mm): {ddm.Height_mm}");
                            r.RelativeItem(5).Text(string.Empty);
                        });
                        
                        c.Item().Row(r =>
                        {
                            r.ConstantItem(200).Text(string.Empty);
                            r.RelativeItem(25).ShowIf(trimSectionVisible).AlignRight().Text($"sx: {wd.LeftTrim}");
                            r.RelativeItem(25).ShowIf(trimSectionVisible).AlignRight().Text($"dx: {wd.RightTrim}");
                            r.RelativeItem(25).ShowIf(trimSectionVisible).AlignRight().Text($"sop: {wd.UpperTrim}");
                            r.RelativeItem(25).ShowIf(trimSectionVisible).AlignRight().Text($"sot: {wd.BelowThreshold}");
                        });

                        // General properties
                        c.Item().Row(r =>
                        {
                            r.ConstantItem(25).Text(string.Empty);
                            r.RelativeItem(3).ShowIf(ddm.openingTypeVisible).Text($"Apert. (vista interna): {ddm.OpeningType}");
                        });

                        c.Item().Row(r =>
                        {
                            r.ConstantItem(25).Text(string.Empty);
                            r.RelativeItem(3).Text($"Vetro: {(ddm.OpaqueGlass ? "Opaco" : "Trasparente")}");
                        });

                        c.Item().Row(r =>
                        {
                            r.ConstantItem(25).Text(string.Empty);
                            r.RelativeItem(3).Text($"Coprifilo: {(ddm.WireCover ? "SI" : "NO")}");
                        });

                        // Prices
                        c.Item().Row(r =>
                        {
                            r.RelativeItem(4).Text(string.Empty);
                            r.RelativeItem(2).AlignRight().Text($"{detailPrice.NetPrice:c}").Bold();
                        });
                    });
            });
        }
    }
}
