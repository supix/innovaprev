using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Classes.Materials;
using DomainModel.Classes.Materials.ConcreteMaterials;

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
                    new ProductCollectionItem() { Id = "AATT", Desc = "Armonia Alluminio/Legno", TrimSectionVisible = false, DescTitle = "ARMONIA", ExtDesc = "Alluminio/Legno a taglio termico: ISOLAMENTO TERMICO e perfetta tenuta alle intemperie sono i valori di pregio di questo nuovo sistema a cui vanno aggiunti elementi di design come lo stile MINIMAL e il particolare costruttivo degli angoli interni a 90°.\n Scocca esterna in alluminio estruso lega 60601 conformemente EN 5736-3 e EN 755-2.\n Materiale isolante in PVC-U ecologico - stabilizzato con Calcio e Zinco di colore BIANCO o NERO.\n Rivestimento interno in legno massello di frassino con umidità relativa di circa il 12% CON GIUNZIONI a 90°." },
                    new ProductCollectionItem() { Id = "AALAM", Desc = "Armonia Alluminio/Legno anta max", TrimSectionVisible = false, DescTitle = "ARMONIA", ExtDesc = "Alluminio/Legno a taglio termico: ISOLAMENTO TERMICO e perfetta tenuta alle intemperie sono i valori di pregio di questo nuovo sistema a cui vanno aggiunti elementi di design come lo stile MINIMAL e il particolare costruttivo degli angoli interni a 90°.\n Scocca esterna in alluminio estruso lega 60601 conformemente EN 5736-3 e EN 755-2.\n Materiale isolante in PVC-U ecologico - stabilizzato con Calcio e Zinco di colore BIANCO o NERO.\n Rivestimento interno in legno massello di frassino con umidità relativa di circa il 12% CON GIUNZIONI a 90°." },
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
                WindowTypes = [
                    this.GetMatCollItem("FIX"),
                    this.GetMatCollItem("F1A"),
                    this.GetMatCollItem("PF1A"),
                    this.GetMatCollItem("SIL"),
                    this.GetMatCollItem("FIXA"),
                    this.GetMatCollItem("VAS"),
                    this.GetMatCollItem("F2A"),
                    this.GetMatCollItem("PF2A"),
                    this.GetMatCollItem("SRAF"),
                    this.GetMatCollItem("SRLA"),
                    this.GetMatCollItem("SCA"),
                    this.GetMatCollItem("PRT1A"),
                    this.GetMatCollItem("PRT2A"),
                    this.GetMatCollItem("SLF"),
                    this.GetMatCollItem("SLA"),
                    this.GetMatCollItem("FLD"),
                    this.GetMatCollItem("FLS"),
                    this.GetMatCollItem("AD"),
                    this.GetMatCollItem("PIA"),
                    this.GetMatCollItem("LIS"),
                    this.GetMatCollItem("CAS"),
                    this.GetMatCollItem("CEL"),
                ],
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

        private MaterialCollectionItem GetMatCollItem(string materialCode)
        {
            var material = MaterialFactory.CreateByCode(materialCode);
            return new MaterialCollectionItem() { Id = materialCode, Desc = material.Description, NumOfDims = material.NumberOfDimensions };
        }
    }
}
