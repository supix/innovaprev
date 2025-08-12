using DomainModel.Classes.Colors;
using DomainModel.Classes.Materials;

namespace DomainModel.Classes.Products.ConcreteProducts
{
    public class IPCAM(IColor color) : PvcInnovaAbstractProduct(color), IAntaMaxAbstractProduct
    {
        public override string Description => "Innova PVC/A Classic Anta Max";

        public override bool TrimSectionVisible => true;

        public override string ExtendedDescriptionTitle => "Innova PVC/A Classic Anta Max";

        public override string ExtendedDescription => "7 camere - Tre guarnizioni di cui una centrale in Dutral a giunto aperto.\nI profili di PVC sono coestrusi direttamente sul profilo in alluminio progettato per dare struttura portante all`infisso con gli accessori e guarnizioni appositamente studiate per questo sistema.\n\nDOTAZIONE DI SERIE:\n - Doppia maniglia modello Toulon e serratura\n - Ferramente Seigenia\n - Imballo";

        public override int Order => 50;
        public override decimal GetPrice_DoubleDim(DoubleDimMaterial m, long area_sqmm)
        {
            return GetFullDoubleDimensionPrice(635M, area_sqmm, m.OpaqueGlass, m.WireCover, m.Height_mm, m.Width_mm, m.FrameType);
        }
        public override decimal GetPrice_DoubleDimFixed(DoubleDimFixedMaterial m, long area_sqmm)
        {
            throw new InvalidOperationException($"Fixed materials not allowed with this product. Product: {Code} - Material: {m.Code}");
        }
    }
}
