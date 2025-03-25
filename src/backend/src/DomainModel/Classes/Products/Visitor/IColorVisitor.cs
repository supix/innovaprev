using DomainModel.Classes.Materials;
using DomainModel.Classes.Materials.ConcreteMaterials;

namespace DomainModel.Classes.Products.Visitor
{
    public interface IColorVisitor
    {
        decimal GetPrice_COP_Standard_m();
        decimal GetPrice_COP_OpenPore_m();
        decimal GetPrice_COP_BrushedDecapeToulipier_m();
        decimal GetPrice_FRO_Standard_m();
        decimal GetPrice_FRO_OpenPore_m();
        decimal GetPrice_FRO_BrushedDecapeToulipier_m();
        decimal GetPrice_CAS_Standard_m();
        decimal GetPrice_CAS_OpenPore_m();
        decimal GetPrice_CAS_BrushedDecapeToulipier_m();
        decimal GetPrice_CAS_PvcWhite_m();
        decimal GetPrice_CAS_PvcWoodEffect_m();
        decimal GetPrice_FRO_PvcWhite_m();
        decimal GetPrice_FRO_PvcWoodEffect_m();
        decimal GetPrice_COP_PvcWhite_m();
        decimal GetPrice_COP_PvcWoodEffect_m();

    }
}
