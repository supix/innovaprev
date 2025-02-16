using DomainModel.Classes;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace PdfQuote
{
    internal class AddressComponent : IComponent
    {
        private string title { get; }
        private CompanyData companyData { get; }

        public AddressComponent(string title, CompanyData companyData)
        {
            this.title = title;
            this.companyData = companyData;
        }

        public void Compose(IContainer container)
        {
            container.Column(column =>
            {
                column.Spacing(2);

                if (!string.IsNullOrEmpty(title))
                {
                    column.Item()
                        .BorderBottom(1)
                        .PaddingBottom(5)
                        .DefaultTextStyle(x => x.FontSize(8))
                        .Text(title).SemiBold();
                }

                column.Item().Text(companyData.CompanyName);
                if (!string.IsNullOrWhiteSpace(companyData.Address))
                    column.Item().Text(companyData.Address);
                if (!string.IsNullOrWhiteSpace(companyData.taxCode))
                    column.Item().Text($"Partita IVA: {companyData.taxCode}");
                if (!string.IsNullOrWhiteSpace(companyData.Phone))
                    column.Item().Text($"Tel.: {companyData.Phone}");
                if (!string.IsNullOrWhiteSpace(companyData.Mail))
                    column.Item().Text($"Email: {companyData.Mail}");
                if (!string.IsNullOrWhiteSpace(companyData.Iban))
                    column.Item().Text($"IBAN: {companyData.Iban}");
            });
        }
    }
}
