using System.Runtime.Intrinsics.Arm;
using DomainModel.Classes;
using DomainModel.Classes.Materials;
using DomainModel.Classes.Materials.ConcreteMaterials;
using DomainModel.Services.PriceCalculator;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace PdfQuote
{
    internal class CasPdfComponent : IComponent
    {
        private readonly int index;
        private readonly CAS cas;
        private readonly WindowsData wd;
        private readonly DetailPrice detailPrice;

        public CasPdfComponent(
            int index, 
            CAS cas, 
            WindowsData wd,
            DetailPrice detailPrice)
        {
            this.index = index;
            this.cas = cas;
            this.wd = wd;
            this.detailPrice = detailPrice;
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
                    r.RelativeItem(1).Text($"{cas.Description}");
                });

                // Lengths
                c.Item().Row(r =>
                {
                    r.ConstantItem(25).Text(string.Empty);
                    r.RelativeItem(2).Text($"q.tà: {this.wd.Quantity}");

                    // Height + Width or Length
                    r.RelativeItem(3).Text($"L (mm): {cas.Width_mm}");
                    r.RelativeItem(3).Text($"H (mm): {cas.Height_mm}");
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
