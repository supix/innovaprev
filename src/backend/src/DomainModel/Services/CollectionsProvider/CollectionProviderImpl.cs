using DomainModel.Classes.Colors;
using DomainModel.Classes.Materials;
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
                Colors = GetColorCollItems(),
                AccessoryColors = new[] {
                    new CollectionItem() { Id = "AC_ORO", Desc = "Oro" },
                    new CollectionItem() { Id = "AC_ARG", Desc = "Argento" },
                    new CollectionItem() { Id = "AC_BIA", Desc = "Bianco" },
                    new CollectionItem() { Id = "AC_BRO", Desc = "Bronzo" },
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
                .Select(m => new MaterialCollectionItem() { Id = m.Code, Desc = m.Description, NumOfDims = m.NumberOfDimensions, MaterialForProduct = m.MaterialForProduct })
                .ToArray();
        }
        private ColorCollectionItem[] GetColorCollItems()
        {
            return ColorFactory.GetAll()
                .Select(c => new ColorCollectionItem() { Id = c.Code, Desc = c.Description, InternalColorForProduct = c.InternalColorForProducts, ExternalColorForProduct = c.ExternalColorForProducts })
                .ToArray();
        }
    }
}
