using DomainModel.Classes.Materials;
using DomainModel.Classes.Materials.ConcreteMaterials;

namespace DomainModel.Classes.Products
{
    public abstract class PvcAbstractProduct : AbstractProduct
    {
        public override decimal GetPrice_CAS(CAS m, long length_mm)
        {
            return GetFullSingleDimensionPrice(268M, length_mm);
        }
    }
}
