using DomainModel.Classes;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PdfQuote
{
    internal class QuoteTemplate : IDocument
    {
        private readonly Project project;

        public QuoteTemplate(Project project) {
            this.project = project ?? throw new ArgumentNullException(nameof(project));
        }
        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(50);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);
                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.CurrentPageNumber();
                        x.Span(" / ");
                        x.TotalPages();
                    });
                });
        }

        private void ComposeHeader(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Component(new AddressComponent(string.Empty, this.project.PersonalData));
                
                row.ConstantItem(50);
                
                row.RelativeItem().Column(column =>
                {
                    column.Item()
                        .Text($"Preventivo #{this.project.PersonalData.OrderNumber}")
                        .FontSize(12).SemiBold().FontColor(Colors.Black);

                    column.Item().Text(text =>
                    {
                        text.Span("Data: ").SemiBold();
                        text.Span($"{DateTime.Now:d}");
                    });
                });
            });
        }

        private void ComposeContent(IContainer container)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Spacing(5);

                column.Item().Row(row =>
                {
                    row.RelativeItem().Text(string.Empty);
                    row.ConstantItem(50);
                    row.RelativeItem()
                        .PaddingBottom(10)
                        .Component(new AddressComponent("DATI CLIENTE", this.project.PersonalData));
                });

                column.Item().Element(ComposeTable);

                if (!string.IsNullOrWhiteSpace(this.project.PersonalData.CompanyName))
                    column.Item().PaddingTop(25).Element(ComposeComments);
            });
        }

        private void ComposeTable(IContainer container)
        {
            container
                .DefaultTextStyle(x => x.FontSize(8))
                .Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(25);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(6);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(2);
                });

                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("#");
                    header.Cell().Element(CellStyle).Text("Mis. (mm)");
                    header.Cell().Element(CellStyle).AlignRight().PaddingRight(10).Text("Q.tà");
                    header.Cell().Element(CellStyle).Text("Articolo");
                    header.Cell().Element(CellStyle).AlignRight().Text("Prezzo Un.");
                    header.Cell().Element(CellStyle).AlignRight().Text("Totale");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });

                var idx = 0;
                foreach (var wd in this.project.WindowsData)
                {
                    table.Cell().Element(CellStyle).Text((++idx).ToString());
                    table.Cell().Element(CellStyle).Text($"{wd.Width}x{wd.Height}");
                    table.Cell().Element(CellStyle).AlignRight().PaddingRight(10).Text($"{wd.Quantity}");
                    table.Cell().Element(CellStyle).Text(wd.WindowType);
                    table.Cell().Element(CellStyle).AlignRight().Text($"€1.234,56");
                    table.Cell().Element(CellStyle).AlignRight().Text($"{1234.56 * wd.Quantity}€");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                }
            });
        }

        private void ComposeComments(IContainer container)
        {
            container.Background(Colors.Grey.Lighten3).Padding(10).AlignRight().Column(column =>
            {
                column.Spacing(5);
                column.Item().Text("Tot.: 1.234,56€").FontSize(14);
            });
        }
    }
}
