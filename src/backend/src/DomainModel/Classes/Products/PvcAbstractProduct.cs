using DomainModel.Classes.Colors;
using DomainModel.Classes.Materials.ConcreteMaterials;

namespace DomainModel.Classes.Products
{
    public abstract class PvcAbstractProduct(IColor color) : AbstractProduct(color, color)
    {
        public override decimal GetPrice_CAS(CAS m, long length_mm)
        {
            return GetFullSingleDimensionPrice(268M, length_mm);
        }
        public override decimal GetPrice_COP(COP m, long length_mm)
        {
            return GetFullSingleDimensionPrice(6M, length_mm);
        }
        public override sealed bool SingleColor => true;
    }
}
