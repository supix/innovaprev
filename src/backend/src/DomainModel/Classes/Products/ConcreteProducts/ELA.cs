using DomainModel.Classes.Materials;

namespace DomainModel.Classes.Products.ConcreteProducts
{
    public class ELA : WoodAbstractProduct
    {
        public override string Description => "Emblema Legno/Alluminio";

        public override bool TrimSectionVisible => false;

        public override string ExtendedDescriptionTitle => "Sistema EMBLEMA Legno/alluminio";

        public override string ExtendedDescription => "Scocca esterna in alluminio estruso lega 60601 conformemente EN 5736-3 e EN 755-2.\n Struttura portante in legno, composta da due elementi in legno frassino incollati tra loro con umidità relativa di circa il 12%.";

        public override decimal StandardPrice => 985M;

        public override int Order => 10;
        public override decimal GetMaterialPrice(IMaterial material)
        {
            const decimal GLASS_PRICE_SQM = 38M;
            const decimal FIXED_PRICE_PER_SQM = 568M + GLASS_PRICE_SQM;

            if (material.IsFixed)
                return material.GetAllowedArea_sqm * FIXED_PRICE_PER_SQM;

            return base.GetMaterialPrice(material);
        }
    }
}
