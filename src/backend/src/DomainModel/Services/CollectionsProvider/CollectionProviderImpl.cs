using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Services.CollectionsProvider
{
    internal class CollectionProviderImpl : ICollectionProvider
    {
        public Collections Get()
        {
            return new Collections()
            {
                Product = new[]
                {
                    new ProductCollectionItem() { Id = "ELA", Desc = "Emblema Legno/Alluminio", TrimSectionVisible = false, DescTitle = "Sistema EMBLEMA Legno/alluminio", ExtDesc = "Scocca esterna in alluminio estruso lega 60601 conformemente EN 5736-3 e EN 755-2.\n Struttura portante in legno, composta da due elementi in legno frassino incollati tra loro con umidità relativa di circa il 12%." },
                    new ProductCollectionItem() { Id = "RALT", Desc = "Review Alluminio/Legno Termico", TrimSectionVisible = false, DescTitle = "Review Alluminio/Legno a taglio termico", ExtDesc = "Profilo esterno in alluminio estruso lega 60601 conformemente EN 5736-3 e EN 755-2 \n Materiale isolante in PVC-U ecologico- stabilizzato con Calcio e Zinco  di colore Nero\n Rivestimento interno in legno massello di frassino" },
                    new ProductCollectionItem() { Id = "AATT", Desc = "Armonia Alluminio/Legno", TrimSectionVisible = false, DescTitle = "ARMONIA", ExtDesc = "Alluminio/Legno a taglio termico: ISOLAMENTO TERMICO e perfetta tenuta alle intemperie sono i valori di pregio di questo nuovo sistema a cui vanno aggiunti elementi di design come lo stile MINIMAL e il particolare costruttivo degli angoli interni a 90°.\n Scocca esterna in alluminio estruso lega 60601 conformemente EN 5736-3 e EN 755-2\n Materiale isolante in PVC-U ecologico - stabilizzato con Calcio e Zinco di colore BIANCO o NERO\n Rivestimento interno in legno massello di frassino con umidità relativa di circa il 12% CON GIUNZIONI a 90°." },
                    new ProductCollectionItem() { Id = "AALAM", Desc = "Armonia Alluminio/Legno anta max", TrimSectionVisible = false, DescTitle = "ARMONIA", ExtDesc = "Alluminio/Legno a taglio termico: ISOLAMENTO TERMICO e perfetta tenuta alle intemperie sono i valori di pregio di questo nuovo sistema a cui vanno aggiunti elementi di design come lo stile MINIMAL e il particolare costruttivo degli angoli interni a 90°.\n Scocca esterna in alluminio estruso lega 60601 conformemente EN 5736-3 e EN 755-2\n Materiale isolante in PVC-U ecologico - stabilizzato con Calcio e Zinco di colore BIANCO o NERO\n Rivestimento interno in legno massello di frassino con umidità relativa di circa il 12% CON GIUNZIONI a 90°." },
                    new ProductCollectionItem() { Id = "IPC", Desc = "Innova PVC/A Classic", TrimSectionVisible = true, DescTitle = "Innova PVC/A Classic (anta infilo)", ExtDesc = "7 camere - Tre guarnizioni di cui una centrale in Dutral a giunto aperto.\nI profili di PVC sono coestrusi direttamente sul profilo in alluminio progettato per dare struttura portante all`infisso  con gli accessori e guarnizioni appositamente studiate per questo sistema.\n\nDOTAZIONE DI SERIE: \n - Martellina: oro / argento Hoppe modello: Roissy / toulon\n - Ferramenta Seigenia\n - Chiusure supplementari anta apribile\n - Anta semifissa con asta a leva e chiusure supplementari\n - Imballo" },
                    new ProductCollectionItem() { Id = "IPN", Desc = "Innova PVC/A New", TrimSectionVisible = true, DescTitle = "Innova PVC/A New (anta fermavetro)", ExtDesc = "7 camere - Tre guarnizioni di cui una centrale in Dutral a giunto aperto.\nI profili di PVC sono coestrusi direttamente sul profilo in alluminio progettato per dare struttura portante all`infisso con gli accessori e guarnizioni appositamente studiate per questo sistema/A New (anta fermavetro).\n\nDOTAZIONE DI SERIE: \n - Martellina: oro / argento Hoppe modello: Roissy / toulon\n - Ferramenta Seigenia\n - Chiusure supplementari anta apribile\n - Anta semifissa con asta a leva e chiusure supplementari\n - Imballo" },
                    new ProductCollectionItem() { Id = "SP", Desc = "Scorrevole PVC", TrimSectionVisible = false, DescTitle = "Scorrevole PVC", ExtDesc = "iSlide#neo è un sistema innovativo dal grande valore tecnologico.\nGiunture a 90° sia per le ante che per i telai con totale assenza di saldature.\nLo scorrevole iSlide#neo è il frutto dell’unione tra l’isolamento estremo e l’eleganza, ottenute grazie alla tecnologia Linktrusion.\nTelaio è coestruso con il profilo strutturale di alluminio mentre le ante sono coestruse con la Thermofibra sostituituendo completamente i classici rinforzi di lamierini in ferro.\nGrazie alle sue eccezionali prestazioni termiche, incontra i requisiti più stretti di efficienza energetica, stabiliti dalla norma RT2012, che posiziona iSlide#neo come lo scorrevole più performante del mercato." },
                },
                InternalColors = new[] {
                    new CollectionItem() { Id = "IC_ARG", Desc = "Argento" },
                    new CollectionItem() { Id = "IC_BRO", Desc = "Bronzo" },
                    new CollectionItem() { Id = "IC_RAM", Desc = "Ramato" },
                    new CollectionItem() { Id = "IC_RAF", Desc = "Raffaello/Altri colori Ral" },
                    new CollectionItem() { Id = "AC_EL", Desc = "Effetto legno" },
                },
                ExternalColors = new[] {
                    new CollectionItem() { Id = "EC_ARG", Desc = "Argento" },
                    new CollectionItem() { Id = "EC_BRO", Desc = "Bronzo" },
                    new CollectionItem() { Id = "EC_RAM", Desc = "Ramato" },
                    new CollectionItem() { Id = "EC_RAF", Desc = "Raffaello/Altri colori Ral" },
                    new CollectionItem() { Id = "AC_EL", Desc = "Effetto legno" },
                },
                AccessoryColors = new[] {
                    new CollectionItem() { Id = "AC_ARG", Desc = "Argento" },
                    new CollectionItem() { Id = "AC_BRO", Desc = "Bronzo" },
                    new CollectionItem() { Id = "AC_RAM", Desc = "Ramato" },
                    new CollectionItem() { Id = "AC_RAF", Desc = "Raffaello/Altri colori Ral" },
                    new CollectionItem() { Id = "AC_EL", Desc = "Effetto legno" },
                },
                WindowTypes = new[] {
                    new CollectionItem() { Id = "FIX", Desc = "Fisso con fermavetro" },
                    new CollectionItem() { Id = "F1A", Desc = "Finestra 1 Anta" },
                    new CollectionItem() { Id = "PF1A", Desc = "Portafinestra 1 Anta" },
                    new CollectionItem() { Id = "SIL", Desc = "Scorrevole in linea" },
                    new CollectionItem() { Id = "FIXA", Desc = "Fisso con anta fissa" },
                    new CollectionItem() { Id = "VAS", Desc = "Vasistas" },
                    new CollectionItem() { Id = "F2A", Desc = "Finestra 2 Ante" },
                    new CollectionItem() { Id = "PF2A", Desc = "Portafinestra 2 Ante" },
                    new CollectionItem() { Id = "SRAF", Desc = "Scorrevole Ribalta con anta fissa" },
                    new CollectionItem() { Id = "SRLA", Desc = "Scorrevole Ribalta con laterale apribile" },
                    new CollectionItem() { Id = "SCA", Desc = "Scorrevole alzante" },
                    new CollectionItem() { Id = "PRT1A", Desc = "Portoncino 1 anta" },
                    new CollectionItem() { Id = "PRT2A", Desc = "Portoncino 2 ante" },
                    new CollectionItem() { Id = "SLF", Desc = "Sopraluce fisso" },
                    new CollectionItem() { Id = "SLA", Desc = "Sopraluce apribile" },
                    new CollectionItem() { Id = "FLD", Desc = "Fisso laterale dx" },
                    new CollectionItem() { Id = "FLS", Desc = "Fisso laterale sx" },
                    new CollectionItem() { Id = "AD", Desc = "A Disegno allegato" },
                    new CollectionItem() { Id = "PIA", Desc = "Piatte" },
                    new CollectionItem() { Id = "LIS", Desc = "Listelli" },
                    new CollectionItem() { Id = "CAS", Desc = "Cassonetti" },
                    new CollectionItem() { Id = "CEL", Desc = "Celetti" },
                },
                OpeningTypes = new[] {
                    new CollectionItem() { Id = "OT_DX", Desc = "SX" },
                    new CollectionItem() { Id = "OT_SX", Desc = "DX" },
                },
                GlassTypes = new[] {
                    new CollectionItem() { Id = "GT_TRASPARENTE", Desc = "Trasparente" },
                    new CollectionItem() { Id = "GT_OPACO", Desc = "Opaco" },
                },
                Crosspieces = new[] {
                    new CollectionItem() { Id = "CR_A", Desc = "Alta" },
                    new CollectionItem() { Id = "CR_M", Desc = "Media" },
                    new CollectionItem() { Id = "CR_B", Desc = "Bassa" },
                },
            };
        }
    }
}
