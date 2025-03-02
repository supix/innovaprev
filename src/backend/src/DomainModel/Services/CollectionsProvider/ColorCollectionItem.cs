namespace DomainModel.Services.CollectionsProvider
{
    public class ColorCollectionItem : CollectionItem
    {
        public required string[] InternalColorForProduct { get; set; }
        public required string[] ExternalColorForProduct { get; set; }
    }
}