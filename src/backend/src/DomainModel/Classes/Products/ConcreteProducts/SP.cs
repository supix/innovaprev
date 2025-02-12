namespace DomainModel.Classes.Products.ConcreteProducts
{
    public class SP : PvcAbstractProduct
    {
        public override string Description => "Scorrevole PVC";

        public override bool TrimSectionVisible => false;

        public override string ExtendedDescriptionTitle => "Scorrevole PVC";

        public override string ExtendedDescription => "iSlide#neo è un sistema innovativo dal grande valore tecnologico.\nGiunture a 90° sia per le ante che per i telai con totale assenza di saldature.\nLo scorrevole iSlide#neo è il frutto dell’unione tra l’isolamento estremo e l’eleganza, ottenute grazie alla tecnologia Linktrusion.\nTelaio è coestruso con il profilo strutturale di alluminio mentre le ante sono coestruse con la Thermofibra sostituituendo completamente i classici rinforzi di lamierini in ferro.\nGrazie alle sue eccezionali prestazioni termiche, incontra i requisiti più stretti di efficienza energetica, stabiliti dalla norma RT2012, che posiziona iSlide#neo come lo scorrevole più performante del mercato.";

        public override decimal StandardPrice => 635M;

        public override int Order => 70;
    }
}
