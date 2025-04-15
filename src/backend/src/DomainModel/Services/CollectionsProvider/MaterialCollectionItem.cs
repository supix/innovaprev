namespace DomainModel.Services.CollectionsProvider
{
    public class MaterialCollectionItem : CollectionItem
    {
        public required int NumOfDims { get; set; }
        public required string[] MaterialForProduct { get; set; }
        public required bool openingTypeVisible { get; set; }
        public required bool glassTypeVisible { get; set; }
        public required bool wireCoverVisible { get; set; }
        public required long? MinAllowedWidth_mm { get; set; }
        public required long? MinAllowedHeight_mm { get; set; }
        public required long? MinAllowedLength_mm { get; set; }
    }
}