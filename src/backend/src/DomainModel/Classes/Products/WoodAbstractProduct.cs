using DomainModel.Classes.Colors;
using DomainModel.Classes.Materials.ConcreteMaterials;

namespace DomainModel.Classes.Products
{
    public abstract class WoodAbstractProduct(IColor ic, IColor ec) : AbstractProduct(ic, ec)
    {
        public override decimal GetPrice_CAS(CAS m, long length_mm)
        {
            var price_m = ic.GetPrice_CAS_m(this);
            return GetFullSingleDimensionPrice(price_m, length_mm);
        }
        public override sealed decimal GetPrice_COP_Standard_m()
        {
            return 18.2M;
        }
        public override sealed decimal GetPrice_COP_OpenPore_m()
        {
            return 20M;
        }
        public override sealed decimal GetPrice_COP_BrushedDecapeToulipier_m()
        {
            return 23M;
        }
        public override sealed decimal GetPrice_FRO_Standard_m()
        {
            return 142M;
        }
        public override sealed decimal GetPrice_FRO_OpenPore_m()
        {
            return 167M;
        }
        public override sealed decimal GetPrice_FRO_BrushedDecapeToulipier_m()
        {
            return 202M;
        }
        public override sealed decimal GetPrice_CAS_Standard_m()
        {
            return 285M;
        }
        public override sealed decimal GetPrice_CAS_OpenPore_m()
        {
            return 361M;
        }
        public override sealed decimal GetPrice_CAS_BrushedDecapeToulipier_m()
        {
            return 443.5M;
        }
        public override sealed decimal GetPrice_CAS_PvcWhite_m()
        {
            throw new NotImplementedException();
        }
        public override sealed decimal GetPrice_CAS_PvcWoodEffect_m()
        {
            throw new NotImplementedException();
        }
        public override sealed decimal GetPrice_FRO_PvcWhite_m()
        {
            throw new NotImplementedException();
        }
        public override sealed decimal GetPrice_FRO_PvcWoodEffect_m()
        {
            throw new NotImplementedException();
        }
        public override sealed decimal GetPrice_COP_PvcWhite_m()
        {
            throw new NotImplementedException();
        }
        public override sealed decimal GetPrice_COP_PvcWoodEffect_m()
        {
            throw new NotImplementedException();
        }
        public override sealed bool SingleColor => false;
    }
}
