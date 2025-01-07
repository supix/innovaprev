using DomainModel.Classes;
using DomainModel.Services.CollectionsProvider;
using DomainModel.Services.PriceCalculator;
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
            container.Row(r =>
            {
                r.ConstantItem(25).Text($"#{this.index.ToString()}");
                r.RelativeItem(2).Text(customData.Quantity);
                r.RelativeItem(3).Text(customData.Description);
                r.RelativeItem(2).AlignRight().Text($"{this.customData.Price:c}").Bold();
            });
        }
    }
}
