using DomainModel.Classes.Colors;
using DomainModel.Classes.Materials;

namespace DomainModel.Classes.Products.ConcreteProducts
{
    public class RALT(IColor ic, IColor ec) : WoodAbstractProduct(ic, ec)
    {
        public override string Description => "Review Alluminio/Legno Termico";

        public override bool TrimSectionVisible => false;

        public override string ExtendedDescriptionTitle => "Review Alluminio/Legno a taglio termico";

        public override string ExtendedDescription => "Profilo esterno in alluminio estruso lega 60601 conformemente EN 5736-3 e EN 755-2 \n Materiale isolante in PVC-U ecologico- stabilizzato con Calcio e Zinco  di colore Nero\n Rivestimento interno in legno massello di frassino.";


        public override int Order => 20;
        public override decimal GetPrice_DoubleDim(DoubleDimMaterial m, long area_sqmm)
        {
            return GetFullDoubleDimensionPrice(842M, area_sqmm, m.OpaqueGlass);
        }
        public override decimal GetPrice_DoubleDimFixed(DoubleDimFixedMaterial m, long area_sqmm)
        {
            return GetFullDoubleDimensionPrice(526M, area_sqmm, m.OpaqueGlass);
        }
    }
}
