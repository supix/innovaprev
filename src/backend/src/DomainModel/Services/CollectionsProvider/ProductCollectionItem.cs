namespace DomainModel.Services.CollectionsProvider
{
    public class ProductCollectionItem: CollectionItem
    {
        public required bool TrimSectionVisible { get; set; }
        public required string ImageFile { get; set; }
        public required string ThumbImageFile { get; set; }
        public required string ExtDesc { get; set; }
    }
}