using DomainModel.Classes.Materials;

namespace DomainModel.Classes.Products.ConcreteProducts
{
    public class RALT : WoodAbstractProduct
    {
        public override string Description => "Review Alluminio/Legno Termico";

        public override bool TrimSectionVisible => false;

        public override string ExtendedDescriptionTitle => "Review Alluminio/Legno a taglio termico";

        public override string ExtendedDescription => "Profilo esterno in alluminio estruso lega 60601 conformemente EN 5736-3 e EN 755-2 \n Materiale isolante in PVC-U ecologico- stabilizzato con Calcio e Zinco  di colore Nero\n Rivestimento interno in legno massello di frassino.";

        public override decimal StandardPrice => 842M;

        public override int Order => 20;
        public override decimal GetMaterialPrice(IMaterial material)
        {
            const decimal GLASS_PRICE_SQM = 38M;
            const decimal FIXED_PRICE_PER_SQM = 526M + GLASS_PRICE_SQM;

            if (material.IsFixed)
                return material.GetAllowedArea_sqm * FIXED_PRICE_PER_SQM;

            return base.GetMaterialPrice(material);
        }
    }
}
