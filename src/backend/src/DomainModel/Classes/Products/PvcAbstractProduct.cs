using DomainModel.Classes.Materials;

namespace DomainModel.Classes.Products
{
    public abstract class PvcAbstractProduct : AbstractProduct
    {
        private const decimal CAS_PRICE_PER_METER = 268M;
        public override decimal getMaterialPrice(IMaterial material)
        {
            if (material.Code == "CAS")
                return material.GetLength_m * CAS_PRICE_PER_METER;

            return base.getMaterialPrice(material);
        }
    }
}
