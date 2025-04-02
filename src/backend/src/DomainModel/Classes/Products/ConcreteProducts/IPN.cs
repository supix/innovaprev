using DomainModel.Classes.Colors;
using DomainModel.Classes.Materials;

namespace DomainModel.Classes.Products.ConcreteProducts
{
    public class IPN(IColor color) : PvcAbstractProduct(color)
    {
        public override string Description => "Innova PVC/A New";

        public override bool TrimSectionVisible => true;

        public override string ExtendedDescriptionTitle => "Innova PVC/A New (anta fermavetro)";

        public override string ExtendedDescription => "7 camere - Tre guarnizioni di cui una centrale in Dutral a giunto aperto.\nI profili di PVC sono coestrusi direttamente sul profilo in alluminio progettato per dare struttura portante all`infisso con gli accessori e guarnizioni appositamente studiate per questo sistema/A New (anta fermavetro).\n\nDOTAZIONE DI SERIE: \n - Martellina: oro / argento Hoppe modello: Roissy / toulon\n - Ferramenta Seigenia\n - Chiusure supplementari anta apribile\n - Anta semifissa con asta a leva e chiusure supplementari\n - Imballo.";

        public override int Order => 60;
        public override decimal GetPrice_DoubleDim(DoubleDimMaterial m, long area_sqmm)
        {
            return GetFullDoubleDimensionPrice(528M, area_sqmm);
        }
        public override decimal GetPrice_DoubleDimFixed(DoubleDimFixedMaterial m, long area_sqmm)
        {
            return GetFullDoubleDimensionPrice(385M, area_sqmm);
        }
    }
}
