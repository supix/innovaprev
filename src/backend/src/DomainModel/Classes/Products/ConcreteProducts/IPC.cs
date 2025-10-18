using DomainModel.Classes.Colors;
using DomainModel.Classes.Materials;

namespace DomainModel.Classes.Products.ConcreteProducts
{
    public class IPC(IColor color) : PvcInnovaAbstractProduct(color), IAntaMaxCompatible
    {
        public override string Description => "Innova PVC/A Classic";

        public override bool TrimSectionVisible => true;

        public override string ExtendedDescriptionTitle => "Innova PVC/A Classic (anta infilo)";

        public override string ExtendedDescription => "7 camere - Tre guarnizioni di cui una centrale in Dutral a giunto aperto.\nI profili di PVC sono coestrusi direttamente sul profilo in alluminio progettato per dare struttura portante all`infisso con gli accessori e guarnizioni appositamente studiate per questo sistema.\n\nDOTAZIONE DI SERIE: \n - Martellina: oro / argento Hoppe modello: Roissy / toulon\n - Ferramenta Seigenia\n - Chiusure supplementari anta apribile\n - Anta semifissa con asta a leva e chiusure supplementari\n - Imballo.";

        public override int Order => 50;
        public override decimal GetPrice_DoubleDim(DoubleDimMaterial m, long area_sqmm)
        {
            var price_sqm = m.ForceAntaMaxPrice() ? 635M : 528M;
            return GetFullDoubleDimensionPrice(price_sqm, area_sqmm, m.OpaqueGlass, m.WireCover, m.Height_mm, m.Width_mm, m.FrameType);
        }
        public override decimal GetPrice_DoubleDimFixed(DoubleDimFixedMaterial m, long area_sqmm)
        {
            return GetFullDoubleDimensionPrice(385M, area_sqmm, m.OpaqueGlass, m.WireCover, m.Height_mm, m.Width_mm, m.FrameType);
        }
    }
}
