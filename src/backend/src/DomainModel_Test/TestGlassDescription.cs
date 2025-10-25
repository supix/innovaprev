using DomainModel.Classes.Colors.ConcreteColors;
using DomainModel.Classes.Frames.ConcreteFrames;
using DomainModel.Classes.Materials.ConcreteMaterials;
using DomainModel.Classes.Products.ConcreteProducts;

namespace DomainModel_Test;

public class TestGlassDescription
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void NormalMaterialWithAATT_GlassDescriptionIsCorrect()
    {
        var mat = new PF1A(2000, 2000, "SX", false, false, new L4EgdesTwitch());
        var prd = new AATT(new Ral1013(), new Ral1013());
        Assert.That(mat.GetGlassDescription(prd), Is.EqualTo("33.1 selettivo/15argon/33.1 be"));
    }

    [Test]
    public void NormalMaterialWithAALAM_GlassDescriptionIsCorrect()
    {
        var mat = new PF1A(2000, 2000, "SX", false, false, new L4EgdesTwitch());
        var prd = new AALAM(new Ral1013(), new Ral1013());
        Assert.That(mat.GetGlassDescription(prd), Is.EqualTo("33.1 selettivo/15argon/33.1 be"));
    }

    [Test]
    public void NormalMaterialWithAATTAndOpaque_GlassDescriptionIsCorrect()
    {
        var mat = new PF1A(2000, 2000, "SX", true, false, new L4EgdesTwitch());
        var prd = new AATT(new Ral1013(), new Ral1013());
        Assert.That(mat.GetGlassDescription(prd), Is.EqualTo("33.1 selettivo/15argon/33.1 be vetro OPACO"));
    }

    [Test]
    public void NormalMaterialWithAALAMAndOpaque_GlassDescriptionIsCorrect()
    {
        var mat = new PF1A(2000, 2000, "SX", true, false, new L4EgdesTwitch());
        var prd = new AALAM(new Ral1013(), new Ral1013());
        Assert.That(mat.GetGlassDescription(prd), Is.EqualTo("33.1 selettivo/15argon/33.1 be vetro OPACO"));
    }

    [Test]
    public void FixMaterialWithAATT_GlassDescriptionIsCorrect()
    {
        var mat = new FIX(2000, 2000, "SX", false, false, new L4EgdesTwitch());
        var prd = new AATT(new Ral1013(), new Ral1013());
        Assert.That(mat.GetGlassDescription(prd), Is.EqualTo("33.1 selettivo/20argon/33.1 be"));
    }
    [Test]
    public void AntaMaxMaterialWithAATT_GlassDescriptionIsCorrect()
    {
        var mat = new PRT1A(2000, 2000, "SX", false, false, new L4EgdesTwitch());
        var prd = new AATT(new Ral1013(), new Ral1013());
        Assert.That(mat.GetGlassDescription(prd), Is.EqualTo("33.1 selettivo/20argon/33.1 be"));
    }

    [Test]
    public void FixMaterialWithRALT_GlassDescriptionIsCorrect()
    {
        var mat = new FIX(2000, 2000, "SX", false, false, new L4EgdesTwitch());
        var prd = new RALT(new Ral1013(), new Ral1013());
        Assert.That(mat.GetGlassDescription(prd), Is.EqualTo("33.1 selettivo/15argon/33.1 be"));
    }
    [Test]
    public void AntaMaxMaterialWithRALT_GlassDescriptionIsCorrect()
    {
        var mat = new PRT1A(2000, 2000, "SX", false, false, new L4EgdesTwitch());
        var prd = new RALT(new Ral1013(), new Ral1013());
        Assert.That(mat.GetGlassDescription(prd), Is.EqualTo("33.1 selettivo/15argon/33.1 be"));
    }

    [Test]
    public void FixMaterialWithAALAM_GlassDescriptionIsCorrect()
    {
        var mat = new FIX(2000, 2000, "SX", false, false, new L4EgdesTwitch());
        var prd = new AALAM(new Ral1013(), new Ral1013());
        Assert.That(mat.GetGlassDescription(prd), Is.EqualTo("33.1 selettivo/20argon/33.1 be"));
    }

    [Test]
    public void AntaMaxMaterialWithAALAM_GlassDescriptionIsCorrect()
    {
        var mat = new PRT2A(2000, 2000, "SX", false, false, new L4EgdesTwitch());
        var prd = new AALAM(new Ral1013(), new Ral1013());
        Assert.That(mat.GetGlassDescription(prd), Is.EqualTo("33.1 selettivo/20argon/33.1 be"));
    }

    [Test]
    public void FixMaterialWithAATTAndOpaque_GlassDescriptionIsCorrect()
    {
        var mat = new FIX(2000, 2000, "SX", true, false, new L4EgdesTwitch());
        var prd = new AATT(new Ral1013(), new Ral1013());
        Assert.That(mat.GetGlassDescription(prd), Is.EqualTo("33.1 selettivo/20argon/33.1 be vetro OPACO"));
    }

    [Test]
    public void AntaMaxMaterialWithAATTAndOpaque_GlassDescriptionIsCorrect()
    {
        var mat = new PRT1A(2000, 2000, "SX", true, false, new L4EgdesTwitch());
        var prd = new AATT(new Ral1013(), new Ral1013());
        Assert.That(mat.GetGlassDescription(prd), Is.EqualTo("33.1 selettivo/20argon/33.1 be vetro OPACO"));
    }

    [Test]
    public void FixMaterialWithAALAMAndOpaque_GlassDescriptionIsCorrect()
    {
        var mat = new FIX(2000, 2000, "SX", true, false, new L4EgdesTwitch());
        var prd = new AALAM(new Ral1013(), new Ral1013());
        Assert.That(mat.GetGlassDescription(prd), Is.EqualTo("33.1 selettivo/20argon/33.1 be vetro OPACO"));
    }

    [Test]
    public void AntaMaxMaterialWithAALAMAndOpaque_GlassDescriptionIsCorrect()
    {
        var mat = new PRT2A(2000, 2000, "SX", true, false, new L4EgdesTwitch());
        var prd = new AALAM(new Ral1013(), new Ral1013());
        Assert.That(mat.GetGlassDescription(prd), Is.EqualTo("33.1 selettivo/20argon/33.1 be vetro OPACO"));
    }

    [Test]
    public void CASWithAATT_EmptyDesc()
    {
        var mat = new CAS(2500, 600);
        var prd = new AALAM(new Ral1013(), new Ral1013());
        Assert.That(prd.GetGlassDescription(mat), Is.Empty);
    }

    [Test]
    public void SingleDimMaterialWithAATT_Throws()
    {
        var mat = new FRO(2500);
        var prd = new AALAM(new Ral1013(), new Ral1013());
        Assert.That(prd.GetGlassDescription(mat), Is.Empty);
    }
}
