﻿using DomainModel.Classes.Materials;
using DomainModel.Classes.Products;

namespace DomainModel.Services.CollectionsProvider
{
    internal class CollectionProviderImpl : ICollectionProvider
    {
        public Collections Get()
        {
            return new Collections()
            {
                Product = GetProductCollItems(),
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
                WindowTypes = GetWindowTypesCollItems(),
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

        private ProductCollectionItem[] GetProductCollItems()
        {
            return ProductFactory.GetAll()
                .Select(p => new ProductCollectionItem() { Id = p.Code, Desc = p.Description, TrimSectionVisible = p.TrimSectionVisible, DescTitle = p.ExtendedDescriptionTitle, ExtDesc = p.ExtendedDescription })
                .ToArray();
        }

        private MaterialCollectionItem[] GetWindowTypesCollItems()
        {
            return MaterialFactory.GetAll()
                .Select(m => new MaterialCollectionItem() { Id = m.Code, Desc = m.Description, NumOfDims = m.NumberOfDimensions })
                .ToArray();
        }
    }
}
