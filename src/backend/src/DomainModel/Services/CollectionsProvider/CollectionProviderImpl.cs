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
            };
        }

        private ProductCollectionItem[] GetProductCollItems()
        {
            return ProductFactory.GetAll()
                .Select(p => new ProductCollectionItem() { Id = p.Code, Desc = p.Description, TrimSectionVisible = p.TrimSectionVisible, SingleColor = p.SingleColor, DescTitle = p.ExtendedDescriptionTitle, ExtDesc = p.ExtendedDescription })
                .ToArray();
        }

        private MaterialCollectionItem[] GetWindowTypesCollItems()
        {
            return MaterialFactory.GetAll()
                .Select(m => new MaterialCollectionItem()
                {
                    Id = m.Code,
                    Desc = m.Description,
                    NumOfDims = m.NumberOfDimensions,
                    MaterialForProduct = m.MaterialForProduct,
                    glassTypeVisible = m.glassTypeVisible,
                    openingTypeVisible = m.openingTypeVisible,
                    wireCoverVisible = m.wireCoverVisible,
                    MinAllowedHeight_mm = m.MinAllowedHeight_mm,
                    MinAllowedLength_mm = m.MinAllowedLength_mm,
                    MinAllowedWidth_mm = m.MinAllowedWidth_mm,
                    MaxAllowedHeight_mm = m.MaxAllowedHeight_mm,
                    MaxAllowedLength_mm = m.MaxAllowedLength_mm,
                    MaxAllowedWidth_mm = m.MaxAllowedWidth_mm
                })
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
