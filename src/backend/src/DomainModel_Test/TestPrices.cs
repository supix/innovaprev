using DomainModel.Classes.Colors.ConcreteColors;
using DomainModel.Classes.Materials.ConcreteMaterials;
using DomainModel.Classes.Products.ConcreteProducts;

namespace DomainModel_Test
{
    public class TestPrices
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_WoodWithCasAllowedLength_PriceIsCorrect()
        {
            var p = new ELA(new DarkColoredAsh(), new DarkColoredAsh());
            var cas = new CAS(2500L);
            var price = p.GetMaterialPrice(cas);
            Assert.That(Math.Abs(price - 285 * 2.5M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PvcWithCasAllowedLength_PriceIsCorrect()
        {
            var p = new SP(new OakWood());
            var cas = new CAS(2500L);
            var price = p.GetMaterialPrice(cas);
            Assert.That(Math.Abs(price - 352 * 2.5M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_WoodWithCasLowLength_PriceIsCorrect()
        {
            var p = new ELA(new DarkColoredAsh(), new DarkColoredAsh());
            var cas = new CAS(500L);
            var price = p.GetMaterialPrice(cas);
            Assert.That(Math.Abs(price - 285M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PvcWithCasLowLength_PriceIsCorrect()
        {
            var p = new IPC(new White9010());
            var cas = new CAS(500L);
            var price = p.GetMaterialPrice(cas);
            Assert.That(Math.Abs(price - 268M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_WoodWithDoubleAllowedLength_PriceIsCorrect()
        {
            var p = new ELA(new Ral1013(), new Ral1013());
            var f1a = new F1A(2000L, 3000L);
            var price = p.GetMaterialPrice(f1a);
            Assert.That(Math.Abs(price - (985 + 38) * 2M * 3M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_WoodWithColorSupplement_PriceIsCorrect()
        {
            var p = new ELA(new BrushedAsh9010(), new AluColor_2918());
            var f1a = new F1A(2000L, 3000L);
            var price = p.GetMaterialPrice(f1a);
            const int glassSupplement = 38;
            const int externalColorSupplement = 101;
            const int internalColorSupplement = 84;
            Assert.That(Math.Abs(price - (985 + internalColorSupplement + externalColorSupplement + glassSupplement) * 2M * 3M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_WoodWithColorSupplement_PriceIsCorrect_v2()
        {
            var p = new RALT(new OpenPoreAsh9010(), new SatinDarkGray());
            var f1a = new F1A(2000L, 3000L);
            var price = p.GetMaterialPrice(f1a);
            const int glassSupplement = 38;
            const int externalColorSupplement = 18;
            const int internalColorSupplement = 62;
            Assert.That(Math.Abs(price - (842 + internalColorSupplement + externalColorSupplement + glassSupplement) * 2M * 3M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PvcWithColorSupplement_PriceIsCorrect()
        {
            var p = new IPC(new DarkWood());
            var f1a = new F1A(2000L, 3000L);
            var price = p.GetMaterialPrice(f1a);
            const int glassSupplement = 38;
            const decimal colorSupplement = 89M;
            Assert.That(Math.Abs(price - (528 + colorSupplement + glassSupplement) * 2M * 3M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_WoodWithDoubleLowLengthF1A_PriceIsCorrect()
        {
            var p = new ELA(new Ral1013(), new Ral1013());
            var f1a = new F1A(1000L, 1000L);
            var price = p.GetMaterialPrice(f1a);
            Assert.That(Math.Abs(price - (985 + 38) * 1.5M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_WoodWithDoubleLowLengthF2A_PriceIsCorrect()
        {
            var p = new ELA(new Ral1013(), new Ral1013());
            var f2a = new F2A(1000L, 1000L);
            var price = p.GetMaterialPrice(f2a);
            Assert.That(Math.Abs(price - (985 + 38) * 1.8M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_RALTWithDoubleAllowedLength_PriceIsCorrect()
        {
            var p = new RALT(new Ral1013(), new Ral1013());
            var f1a = new F1A(5000L, 4000L);
            var price = p.GetMaterialPrice(f1a);
            Assert.That(Math.Abs(price - (842 + 38) * 5M * 4M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_AALAMWithDoubleAllowedLength_PriceIsCorrect()
        {
            var p = new AALAM(new Ral1013(), new Ral1013());
            var f1a = new F1A(5000L, 4000L);
            var price = p.GetMaterialPrice(f1a);
            Assert.That(Math.Abs(price - (876 + 38) * 5M * 4M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_AATTWithDoubleAllowedLength_PriceIsCorrect()
        {
            var p = new AATT(new Ral1013(), new Ral1013());
            var f1a = new F1A(5000L, 4000L);
            var price = p.GetMaterialPrice(f1a);
            Assert.That(Math.Abs(price - (694 + 38) * 5M * 4M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_IPCWithDoubleAllowedLength_PriceIsCorrect()
        {
            var p = new IPC(new Ral1013());
            var f1a = new F1A(5000L, 4000L);
            var price = p.GetMaterialPrice(f1a);
            Assert.That(Math.Abs(price - (528 + 38) * 5M * 4M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_IPNWithDoubleAllowedLength_PriceIsCorrect()
        {
            var p = new IPN(new Ral1013());
            var f1a = new F1A(5000L, 4000L);
            var price = p.GetMaterialPrice(f1a);
            Assert.That(Math.Abs(price - (528 + 38) * 5M * 4M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_SPWithFIXALowLength_PriceIsCorrect()
        {
            var p = new SP(new Ral1013());
            var f1a = new F1A(1500L, 1200L);
            var price = p.GetMaterialPrice(f1a);
            Assert.That(Math.Abs(price - (635 + 38) * 1.5M * 1.2M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_ELAWithFixed_PriceIsCorrect()
        {
            var p = new ELA(new Ral1013(), new Ral1013());
            var fix = new FIX(1800L, 1350L);
            var price = p.GetMaterialPrice(fix);
            Assert.That(Math.Abs(price - (568 + 38) * 1.8M * 1.35M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_SLFWithFixed_PriceIsCorrect()
        {
            var p = new RALT(new Ral1013(), new Ral1013());
            var slf = new SLF(1120L, 2400L);
            var price = p.GetMaterialPrice(slf);
            Assert.That(Math.Abs(price - (526 + 38) * 1.12M * 2.4M), Is.LessThan(1e-3M));
        }
        [Test]
        public void Test_AATTWithFixed_PriceIsCorrect()
        {
            var p = new AATT(new Ral1013(), new Ral1013());
            var fld = new FLD(1780L, 1500L);
            var price = p.GetMaterialPrice(fld);
            Assert.That(Math.Abs(price - (501 + 38) * 1.78M * 1.5M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_IPCWithFixed_PriceIsCorrect()
        {
            var p = new IPC(new Ral1013());
            var fls = new FLS(1300L, 1800L);
            var price = p.GetMaterialPrice(fls);
            Assert.That(Math.Abs(price - (385 + 38) * 1.3M * 1.8M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_COPWithPvc_PriceIsCorrect()
        {
            var p = new IPC(new LightWood());
            var cas = new CAS(2000L);
            var price = p.GetMaterialPrice(cas);
            Assert.That(Math.Abs(price - 352 * 2M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_CASWithPvcWhiteColorSupplement_PriceIsCorrect()
        {
            var p = new IPCAM(new White9010());
            var cop = new COP(3000L);
            var price = p.GetMaterialPrice(cop);
            Assert.That(Math.Abs(price - 5.5M * 3M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_CASWithPvcWoodEffectColorSupplement_PriceIsCorrect()
        {
            var p = new IPCAM(new WhiteWood());
            var cop = new COP(3000L);
            var price = p.GetMaterialPrice(cop);
            Assert.That(Math.Abs(price - 9M * 3M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_COPWithPvcLowLength_PriceIsCorrect()
        {
            var p = new IPC(new DarkWood());
            var cop = new COP(100L);
            var price = p.GetMaterialPrice(cop);
            Assert.That(Math.Abs(price - 9 * 0.1M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_COPWithWood_PriceIsCorrect()
        {
            var p = new AATT(new DarkColoredAsh(), new DarkColoredAsh());
            var cop = new COP(3000L);
            var price = p.GetMaterialPrice(cop);
            Assert.That(Math.Abs(price - 18.2M * 3M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_COPWithWoodOpenPoreColorSupplement_PriceIsCorrect()
        {
            var p = new AATT(new OpenPoreAsh1013(), new OpenPoreAsh1013());
            var cop = new COP(3000L);
            var price = p.GetMaterialPrice(cop);
            Assert.That(Math.Abs(price - 20M * 3M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_COPWithWoodDecapeColorSupplement_PriceIsCorrect()
        {
            var p = new AATT(new DecapeAsh9010(), new DecapeAsh9010());
            var cop = new COP(3000L);
            var price = p.GetMaterialPrice(cop);
            Assert.That(Math.Abs(price - 23M * 3M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_COPWithPvcWhiteColorSupplement_PriceIsCorrect()
        {
            var p = new IPC(new White9010());
            var cop = new COP(3000L);
            var price = p.GetMaterialPrice(cop);
            Assert.That(Math.Abs(price - 5.5M * 3M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_COPWithPvcWoodEffectColorSupplement_PriceIsCorrect()
        {
            var p = new IPC(new Pepper());
            var cop = new COP(2800L);
            var price = p.GetMaterialPrice(cop);
            Assert.That(Math.Abs(price - 9M * 2.8M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_FROWithWoodDecapeColorSupplement_PriceIsCorrect()
        {
            var p = new AATT(new DecapeAsh9010(), new DecapeAsh9010());
            var fro = new FRO(3000L);
            var price = p.GetMaterialPrice(fro);
            Assert.That(Math.Abs(price - 202M * 3M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_FROWithOpenPoreColorSupplement_PriceIsCorrect()
        {
            var p = new AATT(new OpenPoreAsh1013(), new OpenPoreAsh1013());
            var fro = new FRO(2500L);
            var price = p.GetMaterialPrice(fro);
            Assert.That(Math.Abs(price - 167M * 2.5M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_FROWithStandardColorSupplement_PriceIsCorrect()
        {
            var p = new AATT(new CherryColoredAsh(), new CherryColoredAsh());
            var fro = new FRO(2600L);
            var price = p.GetMaterialPrice(fro);
            Assert.That(Math.Abs(price - 142M * 2.6M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_COPWithWoodLowLength_PriceIsCorrect()
        {
            var p = new AATT(new DarkColoredAsh(), new DarkColoredAsh());
            var cop = new COP(100L);
            var price = p.GetMaterialPrice(cop);
            Assert.That(Math.Abs(price - 18.2M * 0.1M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_FROWithWood_PriceIsCorrect()
        {
            var p = new AATT(new DarkColoredAsh(), new DarkColoredAsh());
            var fro = new FRO(3000L);
            var price = p.GetMaterialPrice(fro);
            Assert.That(Math.Abs(price - 142M * 3M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_FROWithWoodLowLength_PriceIsCorrect()
        {
            var p = new AATT(new DarkColoredAsh(), new DarkColoredAsh());
            var fro = new FRO(100L);
            var price = p.GetMaterialPrice(fro);
            Assert.That(Math.Abs(price - 142M * 1M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_FROWithPvc_PriceIsCorrect()
        {
            var p = new IPC(new DarkWood());
            var fro = new FRO(3000L);
            var price = p.GetMaterialPrice(fro);
            Assert.That(Math.Abs(price - 201M * 3M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_FROWithPvcLowLength_PriceIsCorrect()
        {
            var p = new IPC(new White9010());
            var fro = new FRO(100L);
            var price = p.GetMaterialPrice(fro);
            Assert.That(Math.Abs(price - 142M * 1M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PRT1AWithWoodAntaMax_PriceIsCorrect()
        {
            var p = new AALAM(new Ral1013(), new Ral1013());
            var prt1a = new PRT1A(3000L, 4500L);
            var price = p.GetMaterialPrice(prt1a);
            var expected = (876M + 38M) * 3M * 4.5M + 616M;
            Assert.That(Math.Abs(price - expected), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PRT1AWithWoodAntaMaxLowArea_PriceIsCorrect()
        {
            var p = new AALAM(new Ral1013(), new Ral1013());
            var prt1a = new PRT1A(1000L, 1000L);
            var price = p.GetMaterialPrice(prt1a);
            var expected = (876M + 38M) * 1.5M + 616M;
            Assert.That(Math.Abs(price - expected), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PRT2AWithWoodAntaMax_PriceIsCorrect()
        {
            var p = new AALAM(new Ral1013(), new Ral1013());
            var prt2a = new PRT2A(3000L, 4500L);
            var price = p.GetMaterialPrice(prt2a);
            var expected = (876M + 38M) * 3M * 4.5M + 616M;
            Assert.That(Math.Abs(price - expected), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PRT2AWithWoodAntaMaxLowArea_PriceIsCorrect()
        {
            var p = new AALAM(new Ral1013(), new Ral1013());
            var prt2a = new PRT2A(1000L, 1000L);
            var price = p.GetMaterialPrice(prt2a);
            var expected = (876M + 38M) * 1.8M + 616M;
            Assert.That(Math.Abs(price - expected), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PRT1AWithPvcAntaMax_PriceIsCorrect()
        {
            var p = new IPCAM(new Ral1013());
            var prt1a = new PRT1A(3000L, 4500L);
            var price = p.GetMaterialPrice(prt1a);
            var expected = (635M + 38M) * 3M * 4.5M + 616M;
            Assert.That(Math.Abs(price - expected), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PRT1AWithPvcAntaMaxLowArea_PriceIsCorrect()
        {
            var p = new IPCAM(new Ral1013());
            var prt1a = new PRT1A(1000L, 1000L);
            var price = p.GetMaterialPrice(prt1a);
            var expected = (635M + 38M) * 1.5M + 616M;
            Assert.That(Math.Abs(price - expected), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PRT2AWithPvcAntaMax_PriceIsCorrect()
        {
            var p = new IPCAM(new Ral1013());
            var prt2a = new PRT2A(3000L, 4500L);
            var price = p.GetMaterialPrice(prt2a);
            var expected = (635M + 38M) * 3M * 4.5M + 616M;
            Assert.That(Math.Abs(price - expected), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PRT2AWithPvcAntaMaxLowArea_PriceIsCorrect()
        {
            var p = new IPCAM(new Ral1013());
            var prt2a = new PRT2A(1000L, 1000L);
            var price = p.GetMaterialPrice(prt2a);
            var expected = (635M + 38M) * 1.8M + 616M;
            Assert.That(Math.Abs(price - expected), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_COPWithOpenPoreColorSupplement_PriceIsCorrect()
        {
            var p = new AATT(new OpenPoreAsh1013(), new OpenPoreAsh1013());
            var cop = new COP(2500L);
            var price = p.GetMaterialPrice(cop);
            Assert.That(Math.Abs(price - 20M * 2.5M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_COPWithStandardColorSupplement_PriceIsCorrect()
        {
            var p = new AATT(new CherryColoredAsh(), new CherryColoredAsh());
            var cop = new COP(2600L);
            var price = p.GetMaterialPrice(cop);
            Assert.That(Math.Abs(price - 18.2M * 2.6M), Is.LessThan(1e-3M));
        }
    }
}