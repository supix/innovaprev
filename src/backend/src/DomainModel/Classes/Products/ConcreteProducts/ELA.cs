using DomainModel.Classes.Colors;
using DomainModel.Classes.Materials;

namespace DomainModel.Classes.Products.ConcreteProducts
{
    public class ELA(IColor ic, IColor ec) : WoodAbstractProduct(ic, ec)
    {
        public override string Description => "Emblema Legno/Alluminio";

        public override bool TrimSectionVisible => false;

        public override string ExtendedDescriptionTitle => "Sistema EMBLEMA Legno/alluminio";

        public override string ExtendedDescription => "Scocca esterna in alluminio estruso lega 60601 conformemente EN 5736-3 e EN 755-2.\n Struttura portante in legno, composta da due elementi in legno frassino incollati tra loro con umidità relativa di circa il 12%.";
        public override int Order => 10;
        public override decimal GetPrice_DoubleDim(DoubleDimMaterial m, long area_sqmm)
        {
            return GetFullDoubleDimensionPrice(985M, area_sqmm, m.OpaqueGlass, m.WireCover, m.Height_mm, m.Width_mm);
        }
        public override decimal GetPrice_DoubleDimFixed(DoubleDimFixedMaterial m, long area_sqmm)
        {
            return GetFullDoubleDimensionPrice(568M, area_sqmm, m.OpaqueGlass, m.WireCover, m.Height_mm, m.Width_mm);
        }
    }
}
