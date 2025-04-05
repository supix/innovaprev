using DomainModel.Classes.Materials.ConcreteMaterials;

namespace DomainModel_Test;

public class TestMaterials
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void FIX_ProductAreCorrect()
    {
        var correctProducts = new[] { "ELA", "RALT", "AATT", "IPC", "IPN" };
        var mat = new FIX(1000, 1000, false);
        Assert.That(mat.MaterialForProduct, Is.EquivalentTo(correctProducts));
    }

    [Test]
    public void PF1A_ProductAreCorrect()
    {
        var correctProducts = new[] { "ELA", "RALT", "AATT", "IPC", "IPN" };
        var mat = new PF1A(1000, 1000, false);
        Assert.That(mat.MaterialForProduct, Is.EquivalentTo(correctProducts));
    }

    [Test]
    public void PRT1A_ProductAreCorrect()
    {
        var correctProducts = new[] { "AALAM", "IPCAM" };
        var mat = new PRT1A(1000, 1000, false);
        Assert.That(mat.MaterialForProduct, Is.EquivalentTo(correctProducts));
    }

    [Test]
    public void COP_ProductAreCorrect()
    {
        var correctProducts = new[] { "AALAM", "AATT", "ELA", "IPC", "IPCAM", "IPN", "RALT", "SP" };
        var mat = new COP(1000);
        Assert.That(mat.MaterialForProduct, Is.EquivalentTo(correctProducts));

    }

    [Test]
    public void COP_DoesntShowGlass()
    {
        var mat = new COP(1000);
        Assert.That(mat.glassTypeVisible, Is.False);

    }

    [Test]
    public void FRO_DoesntShowGlass()
    {
        var mat = new FRO(1000);
        Assert.That(mat.glassTypeVisible, Is.False);

    }

    [Test]
    public void CAS_DoesntShowGlass()
    {
        var mat = new CAS(1000);
        Assert.That(mat.glassTypeVisible, Is.False);

    }

    [Test]
    public void F1A_ShowsGlass()
    {
        var mat = new F1A(1000, 1500, false);
        Assert.That(mat.glassTypeVisible, Is.True);
    }

    [Test]
    public void FIX_ShowsGlass()
    {
        var mat = new FIX(1000, 1550, false);
        Assert.That(mat.glassTypeVisible, Is.True);
    }

    [Test]
    public void PRT1A_ShowsGlass()
    {
        var mat = new PRT1A(1000, 1550, false);
        Assert.That(mat.glassTypeVisible, Is.True);
    }
}
