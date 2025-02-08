using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Classes.Products.ConcreteProducts
{
    public class AALAM : AbstractProduct
    {
        public override string Description => "Armonia Alluminio/Legno anta max";

        public override bool TrimSectionVisible => false;

        public override string ExtendedDescriptionTitle => "Armonia";

        public override string ExtendedDescription => "Alluminio/Legno a taglio termico: ISOLAMENTO TERMICO e perfetta tenuta alle intemperie sono i valori di pregio di questo nuovo sistema a cui vanno aggiunti elementi di design come lo stile MINIMAL e il particolare costruttivo degli angoli interni a 90°.\n Scocca esterna in alluminio estruso lega 60601 conformemente EN 5736-3 e EN 755-2.\n Materiale isolante in PVC-U ecologico - stabilizzato con Calcio e Zinco di colore BIANCO o NERO.\n Rivestimento interno in legno massello di frassino con umidità relativa di circa il 12% CON GIUNZIONI a 90°.";

        public override decimal StandardPrice => 876M;

        public override int Order => 40;
    }
}
