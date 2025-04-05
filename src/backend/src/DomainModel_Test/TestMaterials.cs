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
        var fix = new FIX(1000, 1000, false);
        Assert.That(fix.MaterialForProduct, Is.EquivalentTo(correctProducts));

    }

    [Test]
    public void PF1A_ProductAreCorrect()
    {
        var correctProducts = new[] { "ELA", "RALT", "AATT", "IPC", "IPN" };
        var fix = new PF1A(1000, 1000, false);
        Assert.That(fix.MaterialForProduct, Is.EquivalentTo(correctProducts));

    }

    [Test]
    public void PRT1A_ProductAreCorrect()
    {
        var correctProducts = new[] { "AALAM", "IPCAM" };
        var fix = new PRT1A(1000, 1000, false);
        Assert.That(fix.MaterialForProduct, Is.EquivalentTo(correctProducts));

    }

    [Test]
    public void COP_ProductAreCorrect()
    {
        var correctProducts = new[] { "AALAM", "AATT", "ELA", "IPC", "IPCAM", "IPN", "RALT", "SP" };
        var fix = new COP(1000);
        Assert.That(fix.MaterialForProduct, Is.EquivalentTo(correctProducts));

    }
}
