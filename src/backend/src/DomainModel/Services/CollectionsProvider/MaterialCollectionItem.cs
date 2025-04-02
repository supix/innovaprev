namespace DomainModel.Services.CollectionsProvider
{
    public class MaterialCollectionItem : CollectionItem
    {
        public required int NumOfDims { get; set; }
        public required string[] MaterialForProduct { get; set; }
    }
}