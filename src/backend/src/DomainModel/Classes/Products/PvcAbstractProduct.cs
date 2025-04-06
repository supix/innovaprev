using DomainModel.Classes.Colors;
using DomainModel.Classes.Materials.ConcreteMaterials;
using DomainModel.Services.WireCoverLenCalculator;

namespace DomainModel.Classes.Products
{
    public abstract class PvcAbstractProduct(IColor color) : AbstractProduct(color, color)
    {
        public override sealed decimal GetPrice_CAS(CAS m, long length_mm)
        {
            var price_m = ic.GetPrice_CAS_m(this);
            return GetFullSingleDimensionPrice(price_m, length_mm);
        }
        public override sealed bool SingleColor => true;
        public override decimal GetPrice_COP_Standard_m()
        {
            throw new NotImplementedException();
        }
        public override sealed decimal GetPrice_COP_OpenPore_m()
        {
            throw new NotImplementedException();
        }
        public override sealed decimal GetPrice_COP_BrushedDecapeToulipier_m()
        {
            throw new NotImplementedException();
        }
        public override sealed decimal GetPrice_FRO_Standard_m()
        {
            throw new NotImplementedException();
        }
        public override sealed decimal GetPrice_FRO_OpenPore_m()
        {
            throw new NotImplementedException();
        }
        public override sealed decimal GetPrice_FRO_BrushedDecapeToulipier_m()
        {
            throw new NotImplementedException();
        }
        public override sealed decimal GetPrice_CAS_Standard_m()
        {
            throw new NotImplementedException();
        }
        public override sealed decimal GetPrice_CAS_OpenPore_m()
        {
            throw new NotImplementedException();
        }
        public override sealed decimal GetPrice_CAS_BrushedDecapeToulipier_m()
        {
            throw new NotImplementedException();
        }
        public override sealed decimal GetPrice_CAS_PvcWhite_m()
        {
            return 268M;
        }
        public override sealed decimal GetPrice_CAS_PvcWoodEffect_m()
        {
            return 352M;
        }
        public override sealed decimal GetPrice_FRO_PvcWhite_m()
        {
            return 142M;
        }
        public override sealed decimal GetPrice_FRO_PvcWoodEffect_m()
        {
            return 201M;
        }
        public override sealed decimal GetPrice_COP_PvcWhite_m()
        {
            return 5.5M;
        }
        public override sealed decimal GetPrice_COP_PvcWoodEffect_m()
        {
            return 9M;
        }
        protected override sealed IWireCoverLenCalculator WireCoverLenCalculator => new PvcWireCoverLenCalculator();

    }
}
