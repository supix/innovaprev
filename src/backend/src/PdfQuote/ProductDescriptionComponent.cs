using DomainModel.Classes;
using DomainModel.Services.CollectionsProvider;
using QuestPDF.Fluent;
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
            container.Column(c =>
            {
                var theProduct = coll.Product.Single(p => p.Id == pd.Product);
                c.Item().PaddingBottom(10).Text(theProduct.Desc).FontSize(14).AlignCenter();
                if (!string.IsNullOrWhiteSpace(theProduct.ExtDesc))
                    c.Item().PaddingBottom(10).Text(theProduct.ExtDesc).FontSize(8).Justify();
                c.Item().DefaultTextStyle(x => x.FontSize(9)).Column(c =>
                {
                    c.Item().Text($"Colore");
                    c.Item().PaddingLeft(5).Text($"Interno: {coll.Colors.Single(ic => ic.Id == pd.InternalColor).Desc}");
                    c.Item().PaddingLeft(5).Text($"Esterno: {coll.Colors.Single(ec => ec.Id == pd.ExternalColor).Desc}");
                    c.Item().PaddingLeft(5).Text($"Accessori: {coll.AccessoryColors.Single(ac => ac.Id == pd.AccessoryColor).Desc}");
                });

                if (!string.IsNullOrWhiteSpace(pd.Notes))
                {
                    c.Item().PaddingTop(10).Text($"NOTE").FontSize(8).Underline();
                    c.Item().Text($"{pd.Notes}").FontSize(8).Justify();
                }
            });
        }
    }
}