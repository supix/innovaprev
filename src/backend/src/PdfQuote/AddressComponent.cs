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
        private PersonalData personalData { get; }

        public AddressComponent(string title, PersonalData personalData)
        {
            this.title = title;
            this.personalData = personalData;
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

                column.Item().Text(this.personalData.CompanyName);
                column.Item().Text(this.personalData.Address);
                column.Item().Text($"Partita IVA: {this.personalData.Vat}");
                column.Item().Text($"Tel.: {this.personalData.Phone}");
                column.Item().Text($"Email: {this.personalData.Mail}");
            });
        }
    }
}
