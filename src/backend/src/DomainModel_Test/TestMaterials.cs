using DomainModel.Classes.Materials.ConcreteMaterials;

namespace DomainModel_Test;

public class TestMaterials
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void FIX_ProductsAreCorrect()
    {
        var correctProducts = new[] { "ELA", "RALT", "AATT", "IPC", "IPN" };
        var mat = new FIX(1000, 1000, false, false);
        Assert.That(mat.MaterialForProduct, Is.EquivalentTo(correctProducts));
    }

    [Test]
    public void PF1A_ProductsAreCorrect()
    {
        var correctProducts = new[] { "ELA", "RALT", "AATT", "IPC", "IPN" };
        var mat = new PF1A(1000, 1000, false, false);
        Assert.That(mat.MaterialForProduct, Is.EquivalentTo(correctProducts));
    }

    [Test]
    public void PRT1A_ProductsAreCorrect()
    {
        var correctProducts = new[] { "AALAM", "IPCAM" };
        var mat = new PRT1A(1000, 1000, false, false);
        Assert.That(mat.MaterialForProduct, Is.EquivalentTo(correctProducts));
    }

    [Test]
    public void FRO_DoesntShowGlass()
    {
        var mat = new FRO(1000);
        Assert.That(mat.glassTypeVisible, Is.False);

    }

    [Test]
    public void CAS_HeightLessThan500_Throws()
    {
        Assert.Catch(typeof(InvalidOperationException), () => new CAS(400, 1000));
    }

    [Test]
    public void CAS_DoesntShowGlass()
    {
        var mat = new CAS(1000, 600);
        Assert.That(mat.glassTypeVisible, Is.False);

    }

    [Test]
    public void CAS_DoesntShowWireCover()
    {
        var mat = new CAS(1000, 600);
        Assert.That(mat.wireCoverVisible, Is.False);

    }

    [Test]
    public void F1A_ShowsGlass()
    {
        var mat = new F1A(1000, 1500, false, false);
        Assert.That(mat.glassTypeVisible, Is.True);
    }

    [Test]
    public void F1A_ShowsWireCover()
    {
        var mat = new F1A(1000, 1500, false, false);
        Assert.That(mat.wireCoverVisible, Is.True);
    }

    [Test]
    public void FIX_ShowsGlass()
    {
        var mat = new FIX(1000, 1550, false, false);
        Assert.That(mat.glassTypeVisible, Is.True);
    }

    [Test]
    public void FIX_ShowsWireCover()
    {
        var mat = new FIX(1000, 1550, false, false);
        Assert.That(mat.wireCoverVisible, Is.True);
    }

    [Test]
    public void PRT1A_ShowsGlass()
    {
        var mat = new PRT1A(1000, 1550, false, false);
        Assert.That(mat.glassTypeVisible, Is.True);
    }
}
