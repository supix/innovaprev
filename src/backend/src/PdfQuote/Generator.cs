using DomainModel.Classes;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PdfQuote
{
    public class Generator
    {
        public byte[] Generate(Project project)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var doc = new QuoteTemplate(project);
            return Document.Create(container => doc.Compose(container)).GeneratePdf();
            //return Document.Create(container =>
            //{
            //    container.Page(page =>
            //    {
            //        page.Size(PageSizes.A4);
            //        page.Margin(2, Unit.Centimetre);
            //        page.PageColor(Colors.White);
            //        page.DefaultTextStyle(x => x.FontSize(8));



            //        page.Content()
            //            .Shrink()
            //            .Border(1)
            //            .Table(table =>
            //            {
            //                table.ColumnsDefinition(columns =>
            //                {
            //                    columns.ConstantColumn(100);
            //                    columns.RelativeColumn();
            //                    columns.ConstantColumn(100);
            //                    columns.RelativeColumn();
            //                });

            //                table.ExtendLastCellsToTableBottom();

            //                table.Cell().RowSpan(3).LabelCell("Cliente");
            //                table.Cell().RowSpan(3).ShowEntire().ValueCell(project.PersonalData.CompanyName);

            //                table.Cell().LabelCell("Preventivo n.");
            //                table.Cell().ValueCell(project.PersonalData.OrderNumber);

            //                table.Cell().LabelCell("Data");
            //                table.Cell().ValueCell(DateTime.Now.ToString("dd-MM-yyyy"));

            //                table.Cell().LabelCell("Indirizzo");
            //                table.Cell().ValueCell(project.PersonalData.Address);

            //                table.Cell().ColumnSpan(4).LabelCell("Dettagli");

            //                foreach (var w in project.WindowsData)
            //                {
            //                    table.Cell().ValueCell(w.WindowType);
            //                    table.Cell().ValueCell(w.GlassType);
            //                    table.Cell().ValueCell(w.Width.ToString());
            //                    table.Cell().ValueCell(w.Height.ToString());
            //                }

            //                table.Cell().LabelCell("Remarks");
            //                table.Cell().ColumnSpan(3).ValueCell(Placeholders.LoremIpsum());
            //            });

            //        page.Footer()
            //            .AlignCenter()
            //            .Text(x =>
            //            {
            //                x.Span("Pag. ");
            //                x.CurrentPageNumber();
            //            });
            //    });
            //})
            //.GeneratePdf();
        }
    }
}

static class SimpleExtension
{
    private static IContainer Cell(this IContainer container, bool dark)
    {
        return container
            .Border(.3f)
            .Background(dark ? Colors.Grey.Lighten2 : Colors.White)
            .Padding(10);
    }

    // displays only text label
    public static void LabelCell(this IContainer container, string text) => container.Cell(true).Text(text).Medium();
    public static void ValueCell(this IContainer container, string text) => container.Cell(false).Text(text).FontSize(9);

    // allows you to inject any type of content, e.g. image
    //public static IContainer ValueCell(this IContainer container, string v) => container.Cell(false);
}