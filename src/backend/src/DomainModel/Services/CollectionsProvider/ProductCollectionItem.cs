namespace DomainModel.Services.CollectionsProvider
{
    public class ProductCollectionItem : CollectionItem
    {
        public required bool TrimSectionVisible { get; set; }
        public required bool SingleColor { get; set; }
        public required string DescTitle { get; set; }
        public required string ExtDesc { get; set; }
    }
}