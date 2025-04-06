using DomainModel.Classes.Colors;
using DomainModel.Classes.Materials;

namespace DomainModel.Classes.Products.ConcreteProducts
{
    public class AALAM(IColor ic, IColor ec) : WoodAbstractProduct(ic, ec), IAntaMaxAbstractProduct
    {
        public override string Description => "Armonia Alluminio/Legno anta max";

        public override bool TrimSectionVisible => false;

        public override string ExtendedDescriptionTitle => "Armonia";

        public override string ExtendedDescription => "Alluminio/Legno a taglio termico: ISOLAMENTO TERMICO e perfetta tenuta alle intemperie sono i valori di pregio di questo nuovo sistema a cui vanno aggiunti elementi di design come lo stile MINIMAL e il particolare costruttivo degli angoli interni a 90°.\n Scocca esterna in alluminio estruso lega 60601 conformemente EN 5736-3 e EN 755-2.\n Materiale isolante in PVC-U ecologico - stabilizzato con Calcio e Zinco di colore BIANCO o NERO.\n Rivestimento interno in legno massello di frassino con umidità relativa di circa il 12% CON GIUNZIONI a 90°.";

        public override int Order => 40;

        public override decimal GetPrice_DoubleDim(DoubleDimMaterial m, long area_sqmm)
        {
            return GetFullDoubleDimensionPrice(876M, area_sqmm, m.OpaqueGlass, m.WireCover, m.Height_mm, m.Width_mm);
        }
        public override decimal GetPrice_DoubleDimFixed(DoubleDimFixedMaterial m, long area_sqmm)
        {
            throw new InvalidOperationException($"Fixed materials not allowed with this product. Product: {Code} - Material: {m.Code}");
        }
    }
}
