namespace DomainModel.Services.CollectionsProvider
{
    public class Collections
    {
        public required ProductCollectionItem[] Product { get; set; }
        public required ColorCollectionItem[] Colors { get; set; }
        public required CollectionItem[] AccessoryColors { get; set; }
        public required MaterialCollectionItem[] WindowTypes { get; set; }
        public required CollectionItem[] OpeningTypes { get; set; }
        public required CollectionItem[] GlassTypes { get; set; }
        public required FrameCollectionItem[] FrameTypes { get; set; }
    }
}
