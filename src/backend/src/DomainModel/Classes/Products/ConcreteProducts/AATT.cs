using DomainModel.Classes.Materials;

namespace DomainModel.Classes.Products.ConcreteProducts
{
    public class AATT : WoodAbstractProduct
    {
        public override string Description => "Armonia Alluminio/Legno";

        public override bool TrimSectionVisible => false;

        public override string ExtendedDescriptionTitle => "Armonia";

        public override string ExtendedDescription => "Alluminio/Legno a taglio termico: ISOLAMENTO TERMICO e perfetta tenuta alle intemperie sono i valori di pregio di questo nuovo sistema a cui vanno aggiunti elementi di design come lo stile MINIMAL e il particolare costruttivo degli angoli interni a 90°.\n Scocca esterna in alluminio estruso lega 60601 conformemente EN 5736-3 e EN 755-2.\n Materiale isolante in PVC-U ecologico - stabilizzato con Calcio e Zinco di colore BIANCO o NERO.\n Rivestimento interno in legno massello di frassino con umidità relativa di circa il 12% CON GIUNZIONI a 90°.";

        public override decimal StandardPrice => 694M;

        public override int Order => 30;
        public override decimal GetMaterialPrice(IMaterial material)
        {
            const decimal GLASS_PRICE_SQM = 38M;
            const decimal FIXED_PRICE_PER_SQM = 501M + GLASS_PRICE_SQM;

            if (material.IsFixed)
                return material.GetAllowedArea_sqm * FIXED_PRICE_PER_SQM;

            return base.GetMaterialPrice(material);
        }
    }
}
