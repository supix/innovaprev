using DomainModel.Classes;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace PdfQuote
{
    internal class CustomProductComponent : IComponent
    {
        private readonly int index;
        private readonly CustomData customData;

        public CustomProductComponent(int index, CustomData customData)
        {
            this.index = index;
            this.customData = customData ?? throw new ArgumentNullException(nameof(customData));
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

                    c.Item().DefaultTextStyle(x => x.FontSize(16)).Row(r =>
                    {
                        r.ConstantItem(25).Text($"{index.ToString()}.");
                        r.RelativeItem(3).Text(customData.Description);
                    });

                    c.Item().Row(r =>
                    {
                        r.ConstantItem(25).Text(string.Empty);
                        r.RelativeItem(2).Text(customData.Quantity);
                        r.RelativeItem(2).AlignRight().Text($"{customData.Price:c}").Bold();
                    });
                });
            });
        }
    }
}
