namespace DomainModel.Classes.Products.ConcreteProducts
{
    public class ELA : AbstractProduct
    {
        public override string Description => "Emblema Legno/Alluminio";

        public override bool TrimSectionVisible => false;

        public override string ExtendedDescriptionTitle => "Sistema EMBLEMA Legno/alluminio";

        public override string ExtendedDescription => "Scocca esterna in alluminio estruso lega 60601 conformemente EN 5736-3 e EN 755-2.\n Struttura portante in legno, composta da due elementi in legno frassino incollati tra loro con umidità relativa di circa il 12%.";

        public override decimal StandardPrice => 985M;

        public override int Order => 10;
    }
}
