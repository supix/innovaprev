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
                    new CollectionItem() { Id = "P_F2A", Desc = "Finestra a due ante" },
                    new CollectionItem() { Id = "P_PF2A", Desc = "Porta finestra a due ante" },
                    new CollectionItem() { Id = "P_F1A", Desc = "Finestra a una anta" },
                    new CollectionItem() { Id = "P_PF1A", Desc = "Porta finestra a una anta" },
                },
                InternalColors = new[] {
                    new CollectionItem() { Id = "IC_G", Desc = "Giallo" },
                    new CollectionItem() { Id = "IC_V", Desc = "Verde" },
                    new CollectionItem() { Id = "IC_R", Desc = "Rosso" },
                },
                ExternalColors = new[] {
                    new CollectionItem() { Id = "EC_G", Desc = "Giallo" },
                    new CollectionItem() { Id = "EC_V", Desc = "Verde" },
                    new CollectionItem() { Id = "EC_R", Desc = "Rosso" },
                },
                AccessoryColors = new[] {
                    new CollectionItem() { Id = "AC_G", Desc = "Giallo" },
                    new CollectionItem() { Id = "AC_V", Desc = "Verde" },
                    new CollectionItem() { Id = "AC_R", Desc = "Rosso" },
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
