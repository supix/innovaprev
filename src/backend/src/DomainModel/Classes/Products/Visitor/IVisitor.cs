using DomainModel.Classes.Materials;
using DomainModel.Classes.Materials.ConcreteMaterials;

namespace DomainModel.Classes.Products.Visitor
{
    public interface IVisitor
    {
        decimal GetPrice_DoubleDim(DoubleDimMaterial m, long area_sqmm);
        decimal GetPrice_DoubleDimFixed(DoubleDimFixedMaterial m, long area_sqmm);
        decimal GetPrice_CAS(CAS m, long length_mm);
        decimal GetPrice_COP(COP m, long length_mm);
        decimal GetPrice_FRO(FRO m, long length_mm);
    }
}
