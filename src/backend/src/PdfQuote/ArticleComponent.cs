using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Classes;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PdfQuote
{
    internal class ArticleComponent : IComponent
    {
        private readonly int index;
        private WindowsData windowsData { get; }

        public ArticleComponent(int index, WindowsData windowsData)
        {
            this.index = index;
            this.windowsData = windowsData;
        }

        public void Compose(IContainer container)
        {
            container.Column(c =>
            {
                c.Item().Row(r =>
                {
                    r.ConstantItem(25).Text($"#{index.ToString()}").Bold();
                    r.RelativeItem(3).Text($"L (mm): {this.windowsData.Width}");
                    r.RelativeItem(3).Text($"H (mm): {this.windowsData.Height}");
                    r.RelativeItem(2).Text($"Q.tà: {this.windowsData.Quantity}");
                    r.RelativeItem(1).AlignRight().Text($"sx: {this.windowsData.LeftTrim}");
                    r.RelativeItem(1).AlignRight().Text($"dx: {this.windowsData.RightTrim}");
                    r.RelativeItem(1).AlignRight().Text($"sop: {this.windowsData.UpperTrim}");
                    r.RelativeItem(1).AlignRight().Text($"sot: {this.windowsData.BelowThreshold}");
                });

                c.Item().DefaultTextStyle(x => x.FontSize(8)).Row(r =>
                {
                    r.ConstantItem(25).Text(string.Empty);
                    r.RelativeItem(3).Text($"Tipologia: {this.windowsData.WindowType}");
                    r.RelativeItem(3).Text($"Apertura (vista interna): {this.windowsData.OpeningType}");
                    r.RelativeItem(2).Text($"Vetro: {this.windowsData.GlassType}");
                    r.RelativeItem(2).AlignRight().Text($"Traverso: {this.windowsData.Crosspiece}");
                });

                c.Item().Row(r =>
                {
                    r.RelativeItem(4).Text(string.Empty);
                    r.RelativeItem(2).AlignRight().Text($"Prezzo unitario: 1.234,56€");
                    r.RelativeItem(2).AlignRight().Text($"Tot.: {1234.56*this.windowsData.Quantity}€").Bold();
                });
            });
        }
    }
}
