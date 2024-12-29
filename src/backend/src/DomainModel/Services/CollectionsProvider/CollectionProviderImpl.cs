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
                    new CollectionItem() { Id = "PRO_GIALLO", Desc = "Giallo" },
                    new CollectionItem() { Id = "PRO_VERDE", Desc = "Verde" },
                    new CollectionItem() { Id = "PRO_ROSSO", Desc = "Rosso" },
                },
                InternalColors = new[] {
                    new CollectionItem() { Id = "IC_GIALLO", Desc = "Giallo" },
                    new CollectionItem() { Id = "IC_VERDE", Desc = "Verde" },
                    new CollectionItem() { Id = "IC_ROSSO", Desc = "Rosso" },
                },
                ExternalColors = new[] {
                    new CollectionItem() { Id = "EC_GIALLO", Desc = "Giallo" },
                    new CollectionItem() { Id = "EC_VERDE", Desc = "Verde" },
                    new CollectionItem() { Id = "EC_ROSSO", Desc = "Rosso" },
                },
                AccessoryColors = new[] {
                    new CollectionItem() { Id = "AC_GIALLO", Desc = "Giallo" },
                    new CollectionItem() { Id = "AC_VERDE", Desc = "Verde" },
                    new CollectionItem() { Id = "AC_ROSSO", Desc = "Rosso" },
                },
                ClimateZones = new[] {
                    new CollectionItem() { Id = "CZ_GIALLO", Desc = "Giallo" },
                    new CollectionItem() { Id = "CZ_VERDE", Desc = "Verde" },
                    new CollectionItem() { Id = "CZ_ROSSO", Desc = "Rosso" },
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
                    new CollectionItem() { Id = "CRO_ALTA", Desc = "Alta" },
                    new CollectionItem() { Id = "CRO_MEDIA", Desc = "Media" },
                    new CollectionItem() { Id = "CRO_BASSA", Desc = "Bassa" },
                },
            };
        }
    }
}
