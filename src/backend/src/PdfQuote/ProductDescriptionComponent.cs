using System.Data.Common;
using DomainModel.Classes;
using DomainModel.Services.CollectionsProvider;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PdfQuote
{
    internal class ProductDescriptionComponent : IComponent
    {
        private ProductData pd;
        private Collections coll;

        public ProductDescriptionComponent(ProductData pd, Collections coll)
        {
            this.pd = pd ?? throw new ArgumentNullException(nameof(pd));
            this.coll = coll ?? throw new ArgumentNullException(nameof(coll));
        }

        public void Compose(IContainer container)
        {
            container.Column(c => {
                c.Item().PaddingBottom(10).Text(coll.Product.Single(p => p.Id == pd.Product).Desc).FontSize(14).AlignCenter();
                c.Item().DefaultTextStyle(x => x.FontSize(9)).Row(row =>
                {
                    row.RelativeItem(1).Column(c => {
                        c.Item().Text($"Colore");
                        c.Item().PaddingLeft(5).Text($"Interno: {coll.InternalColors.Single(ic => ic.Id == pd.InternalColor).Desc}");
                        c.Item().PaddingLeft(5).Text($"Esterno: {coll.ExternalColors.Single(ec => ec.Id == pd.ExternalColor).Desc}");
                        c.Item().PaddingLeft(5).Text($"Accessori: {coll.AccessoryColors.Single(ac => ac.Id == pd.AccessoryColor).Desc}");
                    });
                    row.RelativeItem(1).AlignRight().Text($"Zona climatica: {coll.ClimateZones.Single(cz => cz.Id == pd.ClimateZone).Desc}");
                });

                if (!string.IsNullOrWhiteSpace(pd.Notes))
                    c.Item().PaddingTop(10).Text($"Note: {pd.Notes}").FontSize(9);
            });
        }
    }
}