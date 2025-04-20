using DomainModel.Classes.Materials;
using DomainModel.Classes.Materials.ConcreteMaterials;

namespace DomainModel_Test;

public class TestMaterialFactory
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CAS_CanBeCreated()
    {
        var cas = MaterialFactory.CreateByCode("CAS", 1234, 4321, false, false);
        Assert.That(cas.GetType() == typeof(CAS));
        Assert.That((cas as CAS)!.Height_mm, Is.EqualTo(1234));
        Assert.That((cas as CAS)!.Width_mm, Is.EqualTo(4321));
    }

    [Test]
    public void AllMaterialsCanBeCreated()
    {
        var mats = MaterialFactory.GetAll();
        Assert.That(mats.Single(m => m.GetType() == typeof(CAS)), Is.InstanceOf<CAS>());
        Assert.That(mats.Single(m => m.GetType() == typeof(F1A)), Is.InstanceOf<F1A>());
        Assert.That(mats.Single(m => m.GetType() == typeof(FIX)), Is.InstanceOf<FIX>());
        Assert.That(mats.Single(m => m.GetType() == typeof(PRT1A)), Is.InstanceOf<PRT1A>());
        Assert.That(mats.Single(m => m.GetType() == typeof(FRO)), Is.InstanceOf<FRO>());
        Assert.That(mats.Single(m => m.GetType() == typeof(VAS)), Is.InstanceOf<VAS>());
    }
}
