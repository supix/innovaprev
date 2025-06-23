using System.Runtime.Intrinsics.Arm;
using DomainModel.Classes;
using DomainModel.Classes.Materials;
using DomainModel.Services.PriceCalculator;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace PdfQuote
{
    internal class SingleDimMaterialPdfComponent : IComponent
    {
        private readonly int index;
        private readonly SingleDimMaterial sdm;
        private readonly WindowsData wd;
        private readonly DetailPrice detailPrice;
        private readonly bool trimSectionVisible;

        public SingleDimMaterialPdfComponent(
            int index,
            SingleDimMaterial sdm,
            WindowsData wd,
            DetailPrice detailPrice,
            bool trimSectionVisible)
        {
            this.index = index;
            this.sdm = sdm;
            this.wd = wd;
            this.detailPrice = detailPrice;
            this.trimSectionVisible = trimSectionVisible;
        }

        public void Compose(IContainer container)
        {
            container.Row(row =>
            {
                row.Spacing(10);

                row.ConstantItem(100).Text(string.Empty);

                row.RelativeItem().Column(c =>
            {
                c.Spacing(3);

                // Type
                c.Item().DefaultTextStyle(x => x.FontSize(16)).Row(r =>
                {
                    r.ConstantItem(25).Text($"{index.ToString()}.");
                    r.RelativeItem(1).Text($"{sdm.Description}");
                });

                // Lengths
                c.Item().Row(r =>
                {
                    r.ConstantItem(25).Text(string.Empty);
                    r.RelativeItem(2).Text($"q.tà: {this.wd.Quantity}");

                    // Height + Width or Length
                    r.RelativeItem(3).Text($"L (mm): {sdm.Length_mm}");
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
