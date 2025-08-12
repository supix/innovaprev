using DomainModel.Classes;
using DomainModel.Classes.Materials;
using DomainModel.Classes.Materials.ConcreteMaterials;
using DomainModel.Services.PriceCalculator;
using DomainModel.Services.WindowImageRenderer;
using QuestPDF.Infrastructure;

namespace PdfQuote
{
    internal static class PdfComponentFactory
    {
        internal static IComponent CreateComponent(int idx, IMaterial material, WindowsData wd, DetailPrice detailPrice, bool trimSectionVisible, string frameDescription, IWindowImageRenderer wir)
        {
            if (material is DoubleDimMaterial)
                return new DoubleDimMaterialPdfComponent(idx, (material as DoubleDimMaterial)!, wd, detailPrice, trimSectionVisible, frameDescription, wir);

            if (material is SingleDimMaterial)
                return new SingleDimMaterialPdfComponent(idx, (material as SingleDimMaterial)!, wd, detailPrice, trimSectionVisible);

            if (material is CAS)
                return new CasPdfComponent(idx, (material as CAS)!, wd, detailPrice);

            throw new InvalidOperationException($"Unable to create pdf component with given material code: {wd.WindowType}");
        }
    }
}