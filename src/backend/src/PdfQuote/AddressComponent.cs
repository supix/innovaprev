using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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
                        .Text(this.title).SemiBold();
                }

                column.Item().Text(this.companyData.CompanyName);
                if (!string.IsNullOrWhiteSpace(this.companyData.Address))
                    column.Item().Text(this.companyData.Address);
                if (!string.IsNullOrWhiteSpace(this.companyData.taxCode))
                    column.Item().Text($"Partita IVA: {this.companyData.taxCode}");
                if (!string.IsNullOrWhiteSpace(this.companyData.Phone))
                    column.Item().Text($"Tel.: {this.companyData.Phone}");
                if (!string.IsNullOrWhiteSpace(this.companyData.Mail))
                    column.Item().Text($"Email: {this.companyData.Mail}");
                if (!string.IsNullOrWhiteSpace(this.companyData.Iban))
                    column.Item().Text($"IBAN: {this.companyData.Iban}");
            });
        }
    }
}
