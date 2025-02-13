using DomainModel.Classes.Materials;

namespace DomainModel.Classes.Products
{
    public abstract class  WoodAbstractProduct : AbstractProduct
    {
        private const decimal CAS_PRICE_PER_METER = 285M;
        public override decimal getMaterialPrice(IMaterial material)
        {
            if (material.Code == "CAS")
                return material.GetAllowedLength_m * CAS_PRICE_PER_METER;

            return base.getMaterialPrice(material);
        }
    }
}
