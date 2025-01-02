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
                    new CollectionItem() { Id = "ELA", Desc = "Emblema Legno/Alluminio" },
                    new CollectionItem() { Id = "RALT", Desc = "Review Alluminio/Legno Termico" },
                    new CollectionItem() { Id = "AAL", Desc = "Armonia Alluminio/Legno" },
                    new CollectionItem() { Id = "AATT", Desc = "Armonia Alluminio TT" },
                    new CollectionItem() { Id = "AALAM", Desc = "Armonia Alluminio/Legno anta max" },
                    new CollectionItem() { Id = "SALTT", Desc = "Scorrevole Alluminio/Legno TT" },
                    new CollectionItem() { Id = "IPA", Desc = "Innova PVC/A" },
                    new CollectionItem() { Id = "IPAAM", Desc = "Innova PVC/A Anta Max" },
                    new CollectionItem() { Id = "SP", Desc = "Scorrevole PVC" },
                },
                InternalColors = new[] {
                    new CollectionItem() { Id = "IC_ARG", Desc = "Argento" },
                    new CollectionItem() { Id = "IC_BRO", Desc = "Bronzo" },
                    new CollectionItem() { Id = "IC_RAM", Desc = "Ramato" },
                    new CollectionItem() { Id = "IC_RAF", Desc = "Raffaello/Altricolori Ral" },
                },
                ExternalColors = new[] {
                    new CollectionItem() { Id = "EC_ARG", Desc = "Argento" },
                    new CollectionItem() { Id = "EC_BRO", Desc = "Bronzo" },
                    new CollectionItem() { Id = "EC_RAM", Desc = "Ramato" },
                    new CollectionItem() { Id = "EC_RAF", Desc = "Raffaello/Altricolori Ral" },
                },
                AccessoryColors = new[] {
                    new CollectionItem() { Id = "AC_ARG", Desc = "Argento" },
                    new CollectionItem() { Id = "AC_BRO", Desc = "Bronzo" },
                    new CollectionItem() { Id = "AC_RAM", Desc = "Ramato" },
                    new CollectionItem() { Id = "AC_RAF", Desc = "Raffaello/Altricolori Ral" },
                },
                ClimateZones = new[] {
                    new CollectionItem() { Id = "CZ_F", Desc = "Fredda" },
                    new CollectionItem() { Id = "CZ_M", Desc = "Mite" },
                    new CollectionItem() { Id = "CZ_C", Desc = "Calda" },
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
                    new CollectionItem() { Id = "GT_AZZURRATO", Desc = "Azzurrato" },
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
