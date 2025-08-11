using DomainModel.Classes.Frames.ConcreteFrames;

namespace DomainModel_Test;

public class TestFrames
{
    [Test]
    public void Test_L3EgdesTwitchTrimmedThreshold_HasCorrectProducts()
    {
        var frame = new L3EgdesTwitchTrimmedThreshold();
        var products = frame.FrameForProduct;
        Assert.That(products, Is.EquivalentTo(new[] { "IPC", "IPCAM", "IPN" }));
    }

    [Test]
    public void Test_L4Egdes_HasCorrectProducts()
    {
        var frame = new L4Egdes();
        var products = frame.FrameForProduct;
        Assert.That(products, Is.EquivalentTo(new[] { "AALAM", "AATT", "ELA", "RALT" }));
    }

    [Test]
    public void Test_L4EgdesTwitch_HasCorrectProducts()
    {
        var frame = new L4EgdesTwitch();
        var products = frame.FrameForProduct;
        Assert.That(products, Is.EquivalentTo(new[] { "IPC", "IPCAM", "IPN" }));
    }

    [Test]
    public void Test_Z3EgdesLThreshold_HasCorrectProducts()
    {
        var frame = new Z3EgdesLThreshold();
        var products = frame.FrameForProduct;
        Assert.That(products, Is.EquivalentTo(new[] { "AALAM", "AATT", "ELA", "RALT" }));
    }

    [Test]
    public void Test_Z3EgdesTrimmedThreshold_HasCorrectProducts()
    {
        var frame = new Z3EgdesTrimmedThreshold();
        var products = frame.FrameForProduct;
        Assert.That(products, Is.EquivalentTo(new[] { "IPC", "IPCAM", "IPN" }));
    }

    [Test]
    public void Test_Z4Egdes_HasCorrectProducts()
    {
        var frame = new Z4Egdes();
        var products = frame.FrameForProduct;
        Assert.That(products, Is.EquivalentTo(new[] { "IPC", "IPCAM", "IPN" }));
    }
}
