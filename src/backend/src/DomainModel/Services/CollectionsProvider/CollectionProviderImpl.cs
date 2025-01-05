﻿using System;
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
                    new ProductCollectionItem() { Id = "ELA", Desc = "Emblema Legno/Alluminio", TrimSectionVisible = false, ImageFile = "RH36Y.jpg", ThumbImageFile = "thumb/RH36Y.jpg", ExtDesc = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum." },
                    new ProductCollectionItem() { Id = "RALT", Desc = "Review Alluminio/Legno Termico", TrimSectionVisible = false, ImageFile = "no-image.jpg", ThumbImageFile = "thumb/no-image.jpg", ExtDesc = string.Empty  },
                    new ProductCollectionItem() { Id = "AAL", Desc = "Armonia Alluminio/Legno", TrimSectionVisible = false, ImageFile = "T2S3W.jpg", ThumbImageFile = "thumb/T2S3W.jpg", ExtDesc = string.Empty  },
                    new ProductCollectionItem() { Id = "AATT", Desc = "Armonia Alluminio TT", TrimSectionVisible = false, ImageFile = "YHX21.jpg", ThumbImageFile = "thumb/YHX21.jpg", ExtDesc = string.Empty  },
                    new ProductCollectionItem() { Id = "AALAM", Desc = "Armonia Alluminio/Legno anta max", TrimSectionVisible = false, ImageFile = "T2S3W.jpg", ThumbImageFile = "thumb/T2S3W.jpg", ExtDesc = string.Empty  },
                    new ProductCollectionItem() { Id = "SALTT", Desc = "Scorrevole Alluminio/Legno TT", TrimSectionVisible = false, ImageFile = "no-image.jpg", ThumbImageFile = "thumb/no-image.jpg", ExtDesc = string.Empty  },
                    new ProductCollectionItem() { Id = "IPC", Desc = "Innova PVC/A Classic", TrimSectionVisible = true, ImageFile = "U651R.jpg", ThumbImageFile = "thumb/U651R.jpg", ExtDesc = string.Empty  },
                    new ProductCollectionItem() { Id = "IPN", Desc = "Innova PVC/A New", TrimSectionVisible = true, ImageFile = "H2G71.jpg", ThumbImageFile = "thumb/H2G71.jpg", ExtDesc = string.Empty  },
                    new ProductCollectionItem() { Id = "SP", Desc = "Scorrevole PVC", TrimSectionVisible = false, ImageFile = "no-image.jpg", ThumbImageFile = "thumb/no-image.jpg", ExtDesc = string.Empty  },
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
