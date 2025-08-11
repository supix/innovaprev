using DomainModel.Classes.Colors.ConcreteColors;
using DomainModel.Classes.Products.ConcreteProducts;

namespace DomainModel_Test;

public class TestColors
{
    [Test]
    public void Test_AluminumMarbleGray1203ColorAsInternalColor_GivesNoProducts()
    {
        var c = new MarbleGray1203();
        var products = c.InternalColorForProducts;
        Assert.That(products, Is.Empty);
    }

    [Test]
    public void Test_AluminumMarbleGray1203ColorAsExternalColor_GivesWoodAluProducts()
    {
        var c = new MarbleGray1203();
        var products = c.ExternalColorForProducts;
        Assert.That(products, Is.EquivalentTo(new[] { typeof(ELA).Name, typeof(RALT).Name, typeof(AATT).Name, typeof(AALAM).Name }));
    }

    [Test]
    public void Test_AluminumBrown8017ColorAsExternalColor_GivesWoodAluProducts()
    {
        var c = new Brown8017();
        var products = c.ExternalColorForProducts;
        Assert.That(products, Is.EquivalentTo(new[] { typeof(ELA).Name, typeof(RALT).Name, typeof(AATT).Name, typeof(AALAM).Name }));
    }

    [Test]
    public void Test_PvcWhite9010ColorAsExternalColor_GivesWoodAluProducts()
    {
        var c = new White9010();
        var products = c.InternalColorForProducts;
        Assert.That(products, Is.EquivalentTo(new[] { typeof(IPC).Name, typeof(IPN).Name, typeof(IPCAM).Name, typeof(SP).Name }));
    }

    [Test]
    public void Test_AluminumEmbossedIvory1804ColorAsExternalColor_GivesWoodAluProducts()
    {
        var c = new EmbossedIvory1804();
        var products = c.ExternalColorForProducts;
        Assert.That(products, Is.EquivalentTo(new[] { typeof(ELA).Name, typeof(RALT).Name, typeof(AATT).Name, typeof(AALAM).Name }));
    }

    [Test]
    public void Test_AluminumEmbossedIvory1804ColorAsInternalColor_IsEmpty()
    {
        var c = new EmbossedIvory1804(); //AluColor_2912
        var products = c.InternalColorForProducts;
        Assert.That(products, Is.Empty);
    }

    [Test]
    public void Test_AluminumAluColor_2912ColorAsInternalColor_IsEmpty()
    {
        var c = new AluColor_2912();
        var products = c.InternalColorForProducts;
        Assert.That(products, Is.Empty);
    }

    [Test]
    public void Test_AluminumAluColor_2912ColorAsExternalColor_GivesWoodAluProducts()
    {
        var c = new AluColor_2912();
        var products = c.ExternalColorForProducts;
        Assert.That(products, Is.EquivalentTo(new[] { typeof(ELA).Name, typeof(RALT).Name, typeof(AATT).Name, typeof(AALAM).Name }));
    }
}
