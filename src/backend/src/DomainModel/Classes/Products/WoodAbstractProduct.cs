using DomainModel.Classes.Materials.ConcreteMaterials;

namespace DomainModel.Classes.Products
{
    public abstract class WoodAbstractProduct : AbstractProduct
    {
        public override decimal GetPrice_CAS(CAS m, long length_mm)
        {
            return GetFullSingleDimensionPrice(285M, length_mm);
        }

        public override decimal GetPrice_COP(COP m, long length_mm)
        {
            return GetFullSingleDimensionPrice(18.2M, length_mm);
        }
    }
}
