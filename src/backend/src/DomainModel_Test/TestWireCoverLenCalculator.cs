using DomainModel.Classes.Colors.ConcreteColors;
using DomainModel.Services.WireCoverLenCalculator;
namespace DomainModel_Test;

public class TestWireCoverLenCalculator
{
    [Test]
    public void Test_2WoodBars_LenIsCorrect()
    {
        var wclc = new WoodWireCoverLenCalculator();
        var len = wclc.GetLen(1500, 1000);
        Assert.That(len, Is.EqualTo(6000));
    }

    [Test]
    public void Test_1PvcBar_LenIsCorrect()
    {
        var wclc = new PvcWireCoverLenCalculator();
        var len = wclc.GetLen(1500, 1000);
        Assert.That(len, Is.EqualTo(6500));
    }

    [Test]
    public void Test_2PvcBar_LenIsCorrect()
    {
        var wclc = new PvcWireCoverLenCalculator();
        var len = wclc.GetLen(2500, 2000);
        Assert.That(len, Is.EqualTo(13000));
    }

    [Test]
    public void Test_Exactly2WoodBars_LenIsCorrect()
    {
        var wclc = new WoodWireCoverLenCalculator();
        var len = wclc.GetLen(900, 900);
        Assert.That(len, Is.EqualTo(3000));
    }

    [Test]
    public void Test_Exactly1PvcBar_LenIsCorrect()
    {
        var wclc = new PvcWireCoverLenCalculator();
        var len = wclc.GetLen(1900, 2400);
        Assert.That(len, Is.EqualTo(6500));
    }
}
