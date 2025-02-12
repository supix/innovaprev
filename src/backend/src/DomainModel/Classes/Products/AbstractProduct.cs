using DomainModel.Classes.Materials;

namespace DomainModel.Classes.Products
{
    public abstract class AbstractProduct : IProduct
    {
        public string Code => GetType().Name;
        public abstract string Description { get; }
        public abstract bool TrimSectionVisible { get; }
        public abstract string ExtendedDescriptionTitle { get; }
        public abstract string ExtendedDescription { get; }
        public abstract decimal StandardPrice { get; }
        public abstract int Order { get; }
        public virtual decimal getMaterialPrice(IMaterial material)
        {
            if (material.Code == "AD")
                return 0;

            if (material.Code == "PIA")
                return 0;

            if (material.Code == "LIS")
                return 0;

            if (material.Code == "CEL")
                return 0;

            if (material.NumberOfDimensions == 2)
                return StandardPrice * material.GetArea_sqm;

            throw new InvalidOperationException($"Cannot compute material price. Code: {material.Code}");
        }
    }
}
