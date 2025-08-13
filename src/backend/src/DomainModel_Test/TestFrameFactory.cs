using DomainModel.Classes.Frames;
using DomainModel.Classes.Frames.ConcreteFrames;

namespace DomainModel_Test;

public class TestFrameFactory
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void L4Egdes_CanBeCreated()
    {
        var frame = FrameFactory.CreateByCode("L4Egdes");
        Assert.That(frame.GetType() == typeof(L4Egdes));
    }

    [Test]
    public void AllMaterialsCanBeCreated()
    {
        var mats = FrameFactory.GetAll();
        Assert.That(mats.Single(m => m.GetType() == typeof(L3EgdesTwitchTrimmedThreshold)), Is.InstanceOf<L3EgdesTwitchTrimmedThreshold>());
        Assert.That(mats.Single(m => m.GetType() == typeof(L4Egdes)), Is.InstanceOf<L4Egdes>());
        Assert.That(mats.Single(m => m.GetType() == typeof(L4EgdesTwitch)), Is.InstanceOf<L4EgdesTwitch>());
        Assert.That(mats.Single(m => m.GetType() == typeof(Z3EgdesLThreshold)), Is.InstanceOf<Z3EgdesLThreshold>());
        Assert.That(mats.Single(m => m.GetType() == typeof(Z3EgdesTrimmedThreshold)), Is.InstanceOf<Z3EgdesTrimmedThreshold>());
        Assert.That(mats.Single(m => m.GetType() == typeof(Z4Egdes)), Is.InstanceOf<Z4Egdes>());
    }

    [Test]
    public void NullFrameIsNotReturned()
    {
        var mats = FrameFactory.GetAll();
        Assert.Throws<InvalidOperationException>(() =>
        {
            var nullFrame = mats.Single(m => m.GetType() == typeof(NullFrame));
        });
    }
}
