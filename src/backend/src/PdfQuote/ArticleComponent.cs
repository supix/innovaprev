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
                c.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(25);
                        columns.RelativeColumn(3);
                        columns.RelativeColumn(3);
                        columns.RelativeColumn(2);
                        columns.RelativeColumn(1);
                        columns.RelativeColumn(1);
                        columns.RelativeColumn(1);
                        columns.RelativeColumn(1);
                    });

                    table.Cell().Element(CellStyle).Text($"#{index.ToString()}").Bold();
                    table.Cell().Element(CellStyle).Text($"l (mm): {this.windowsData.Width}");
                    table.Cell().Element(CellStyle).Text($"h (mm): {this.windowsData.Height}");
                    table.Cell().Element(CellStyle).Text($"Q.tà: {this.windowsData.Quantity}");
                    table.Cell().Element(CellStyle).AlignRight().Text($"sx: {this.windowsData.LeftTrim}");
                    table.Cell().Element(CellStyle).AlignRight().Text($"dx: {this.windowsData.RightTrim}");
                    table.Cell().Element(CellStyle).AlignRight().Text($"sop: {this.windowsData.UpperTrim}");
                    table.Cell().Element(CellStyle).AlignRight().Text($"sot: {this.windowsData.BelowThreshold}");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container; //.BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                });

                c.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(25);
                        columns.RelativeColumn(3);
                        columns.RelativeColumn(3);
                        columns.RelativeColumn(2);
                        columns.RelativeColumn(2);
                    });

                    table.Cell().Element(CellStyle).Text(string.Empty);
                    table.Cell().Element(CellStyle).Text($"Tipologia: {this.windowsData.WindowType}");
                    table.Cell().Element(CellStyle).Text($"Apertura (vista interna): {this.windowsData.OpeningType}");
                    table.Cell().Element(CellStyle).Text($"Vetro: {this.windowsData.GlassType}");
                    table.Cell().Element(CellStyle).AlignRight().Text($"Traverso: {this.windowsData.Crosspiece}");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.FontSize(8));
                    }
                });

                c.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(4);
                        columns.RelativeColumn(2);
                        columns.RelativeColumn(2);
                    });

                    table.Cell().Element(CellStyle).Text(string.Empty);
                    table.Cell().Element(CellStyle).AlignRight().Text($"Prezzo unitario: 1.234,56€");
                    table.Cell().Element(CellStyle).AlignRight().Text($"Tot.: {1234.56*this.windowsData.Quantity}€").Bold();

                    static IContainer CellStyle(IContainer container)
                    {
                        return container;
                    }
                });
            });
        }
    }
}
