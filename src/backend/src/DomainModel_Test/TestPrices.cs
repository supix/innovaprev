using DomainModel.Classes.Colors.ConcreteColors;
using DomainModel.Classes.Frames.ConcreteFrames;
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
            var cas = new CAS(600L, 2500L);
            var price = p.GetMaterialPrice(cas);
            Assert.That(Math.Abs(price - 285 * 2.5M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PvcWithCasAllowedLength_PriceIsCorrect()
        {
            var p = new SP(new OakWood());
            var cas = new CAS(600L, 2500L);
            var price = p.GetMaterialPrice(cas);
            Assert.That(Math.Abs(price - 352 * 2.5M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_WoodWithCasLowLength_PriceIsCorrect()
        {
            var p = new ELA(new DarkColoredAsh(), new DarkColoredAsh());
            var cas = new CAS(600L, 500L);
            var price = p.GetMaterialPrice(cas);
            Assert.That(Math.Abs(price - 285M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PvcWithCasLowLength_PriceIsCorrect()
        {
            var p = new IPC(new White9010());
            var cas = new CAS(600L, 500L);
            var price = p.GetMaterialPrice(cas);
            Assert.That(Math.Abs(price - 268M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_WoodWithDoubleAllowedLength_PriceIsCorrect()
        {
            var p = new ELA(new Ral1013(), new Ral1013());
            var f1a = new F1A(2000L, 3000L, "SX", false, false, new L4Egdes());
            var price = p.GetMaterialPrice(f1a);
            Assert.That(Math.Abs(price - (985 + 38) * 2M * 3M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_WoodWithColorSupplement_PriceIsCorrect()
        {
            var p = new ELA(new BrushedAsh9010(), new AluColor_2918());
            var f1a = new F1A(2000L, 3000L, "SX", false, false, new L4Egdes());
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
            var f1a = new F1A(2000L, 3000L, "SX", false, false, new L4Egdes());
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
            var f1a = new F1A(2000L, 3000L, "SX", false, false, new Z4Egdes());
            var price = p.GetMaterialPrice(f1a);
            const int glassSupplement = 38;
            const decimal colorSupplement = 89M;
            Assert.That(Math.Abs(price - (528 + colorSupplement + glassSupplement) * 2M * 3M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_WoodWithDoubleLowLengthF1A_PriceIsCorrect()
        {
            var p = new ELA(new Ral1013(), new Ral1013());
            var f1a = new F1A(1000L, 1000L, "SX", false, false, new L4Egdes());
            var price = p.GetMaterialPrice(f1a);
            Assert.That(Math.Abs(price - (985 + 38) * 1.5M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_WoodWithDoubleLowLengthF2A_PriceIsCorrect()
        {
            var p = new ELA(new Ral1013(), new Ral1013());
            var f2a = new F2A(1000L, 1000L, "SX", false, false, new L4Egdes());
            var price = p.GetMaterialPrice(f2a);
            Assert.That(Math.Abs(price - (985 + 38) * 1.8M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_RALTWithDoubleAllowedLength_PriceIsCorrect()
        {
            var p = new RALT(new Ral1013(), new Ral1013());
            var f1a = new F1A(5000L, 4000L, "SX", false, false, new L4Egdes());
            var price = p.GetMaterialPrice(f1a);
            Assert.That(Math.Abs(price - (842 + 38) * 5M * 4M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_AALAMWithDoubleAllowedLength_PriceIsCorrect()
        {
            var p = new AALAM(new Ral1013(), new Ral1013());
            var f1a = new F1A(5000L, 4000L, "SX", false, false, new L4Egdes());
            var price = p.GetMaterialPrice(f1a);
            Assert.That(Math.Abs(price - (876 + 38) * 5M * 4M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_AATTWithDoubleAllowedLength_PriceIsCorrect()
        {
            var p = new AATT(new Ral1013(), new Ral1013());
            var f1a = new F1A(5000L, 4000L, "SX", false, false, new L4Egdes());
            var price = p.GetMaterialPrice(f1a);
            Assert.That(Math.Abs(price - (793 + 38) * 5M * 4M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_IPCWithDoubleAllowedLength_PriceIsCorrect()
        {
            var p = new IPC(new Ral1013());
            var f1a = new F1A(5000L, 4000L, "SX", false, false, new Z4Egdes());
            var price = p.GetMaterialPrice(f1a);
            Assert.That(Math.Abs(price - (528 + 38) * 5M * 4M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_IPNWithDoubleAllowedLength_PriceIsCorrect()
        {
            var p = new IPN(new Ral1013());
            var f1a = new F1A(5000L, 4000L, "SX", false, false, new Z4Egdes());
            var price = p.GetMaterialPrice(f1a);
            Assert.That(Math.Abs(price - (528 + 38) * 5M * 4M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_SPWithFIXALowLength_PriceIsCorrect()
        {
            var p = new SP(new Ral1013());
            var f1a = new F1A(1500L, 1200L, "SX", false, false, new NullFrame());
            var price = p.GetMaterialPrice(f1a);
            Assert.That(Math.Abs(price - (635 + 38) * 1.5M * 1.2M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_NullFrameForInnovaProduct_RaisesException()
        {
            var p = new IPC(new Ral1013());
            var f1a = new F1A(1500L, 1200L, "SX", false, false, new NullFrame());
            Assert.Throws<InvalidOperationException>(() =>
            {
                var price = p.GetMaterialPrice(f1a);
            });
        }

        [Test]
        public void Test_NullFrameForWoodProduct_RaisesException()
        {
            var p = new ELA(new Ral1013(), new Ral1013());
            var f1a = new F1A(1500L, 1200L, "SX", false, false, new NullFrame());
            Assert.Throws<InvalidOperationException>(() =>
            {
                var price = p.GetMaterialPrice(f1a);
            });
        }

        [Test]
        public void Test_WoodFrameForInnovaProduct_RaisesException()
        {
            var p = new IPC(new Ral1013());
            var f1a = new F1A(1500L, 1200L, "SX", false, false, new Z3EgdesLThreshold());
            Assert.Throws<InvalidOperationException>(() =>
            {
                var price = p.GetMaterialPrice(f1a);
            });
        }

        [Test]
        public void Test_InnovaFrameForWoodProduct_RaisesException()
        {
            var p = new ELA(new Ral1013(), new Ral1013());
            var f1a = new F1A(1500L, 1200L, "SX", false, false, new L4EgdesTwitch());
            Assert.Throws<InvalidOperationException>(() =>
            {
                var price = p.GetMaterialPrice(f1a);
            });
        }

        [Test]
        public void Test_ELAWithFixed_PriceIsCorrect()
        {
            var p = new ELA(new Ral1013(), new Ral1013());
            var fix = new FIX(1800L, 1350L, "SX", false, false, new L4Egdes());
            var price = p.GetMaterialPrice(fix);
            Assert.That(Math.Abs(price - (568 + 38) * 1.8M * 1.35M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_ELAWithFixedAndZFrame_PriceIsCorrect()
        {
            var p = new ELA(new Ral1013(), new Ral1013());
            var fix = new FIX(1800L, 1350L, "SX", false, false, new Z3EgdesLThreshold());
            var price = p.GetMaterialPrice(fix);
            Assert.That(Math.Abs(price - (568 + 38 + 8) * 1.8M * 1.35M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_SLFWithFixed_PriceIsCorrect()
        {
            var p = new RALT(new Ral1013(), new Ral1013());
            var slf = new SLF(1120L, 2400L, "SX", false, false, new L4Egdes());
            var price = p.GetMaterialPrice(slf);
            Assert.That(Math.Abs(price - (526 + 38) * 1.12M * 2.4M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_AATTWithFixed_PriceIsCorrect()
        {
            var p = new AATT(new Ral1013(), new Ral1013());
            var fld = new FLD(1780L, 1500L, "SX", false, false, new L4Egdes());
            var price = p.GetMaterialPrice(fld);
            Assert.That(Math.Abs(price - (501 + 38) * 1.78M * 1.5M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_AATTWithFixedAndZFrame_PriceIsCorrect()
        {
            var p = new AATT(new Ral1013(), new Ral1013());
            var fld = new FLD(1780L, 1500L, "SX", false, false, new Z3EgdesLThreshold());
            var price = p.GetMaterialPrice(fld);
            Assert.That(Math.Abs(price - (501 + 38 + 8) * 1.78M * 1.5M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_IPCWithFixed_PriceIsCorrect()
        {
            var p = new IPC(new Ral1013());
            var fls = new FLS(1300L, 1800L, "SX", false, false, new Z4Egdes());
            var price = p.GetMaterialPrice(fls);
            Assert.That(Math.Abs(price - (385 + 38) * 1.3M * 1.8M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_COPWithPvc_PriceIsCorrect()
        {
            var p = new IPC(new LightWood());
            var cas = new CAS(600L, 2000L);
            var price = p.GetMaterialPrice(cas);
            Assert.That(Math.Abs(price - 352 * 2M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_FROWithPvcWoodEffectColorSupplement_PriceIsCorrect()
        {
            var p = new IPCAM(new WhiteWood());
            var fro = new FRO(3000L);
            var price = p.GetMaterialPrice(fro);
            Assert.That(Math.Abs(price - 201M * 3M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_FROWithPvcWhiteColorSupplement_PriceIsCorrect()
        {
            var p = new IPCAM(new White9010());
            var fro = new FRO(3000L);
            var price = p.GetMaterialPrice(fro);
            Assert.That(Math.Abs(price - 142M * 3M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_FROWithWoodStandardColorSupplement_PriceIsCorrect()
        {
            var p = new RALT(new OakColoredAsh(), new Brown8017());
            var fro = new FRO(3000L);
            var price = p.GetMaterialPrice(fro);
            Assert.That(Math.Abs(price - 142M * 3M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_FROWithWoodOpenPoreColorSupplement_PriceIsCorrect()
        {
            var p = new RALT(new OpenPoreAsh1013(), new SatinDarkGray());
            var fro = new FRO(3000L);
            var price = p.GetMaterialPrice(fro);
            Assert.That(Math.Abs(price - 167M * 3M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_FROWithWoodBrushedDecapeToulipierColorSupplement_PriceIsCorrect()
        {
            var p = new RALT(new BrushedAsh1013(), new AluColor_M220());
            var fro = new FRO(3000L);
            var price = p.GetMaterialPrice(fro);
            Assert.That(Math.Abs(price - 202M * 3M), Is.LessThan(1e-3M));
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
            var prt1a = new PRT1A(3000L, 4500L, "SX", false, false, new L4Egdes());
            var price = p.GetMaterialPrice(prt1a);
            var expected = (876M + 38M) * 3M * 4.5M + 616M;
            Assert.That(Math.Abs(price - expected), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PRT1AWithWoodAntaMaxOpaqueGlass_PriceIsCorrect()
        {
            var p = new AALAM(new Ral1013(), new Ral1013());
            var prt1a = new PRT1A(3000L, 4500L, "SX", true, false, new L4Egdes());
            var price = p.GetMaterialPrice(prt1a);
            var expected = (876M + 48M) * 3M * 4.5M + 616M;
            Assert.That(Math.Abs(price - expected), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PRT1AWithWoodAntaMaxLowArea_PriceIsCorrect()
        {
            var p = new AALAM(new Ral1013(), new Ral1013());
            var prt1a = new PRT1A(1000L, 1000L, "SX", false, false, new L4Egdes());
            var price = p.GetMaterialPrice(prt1a);
            var expected = (876M + 38M) * 1.5M + 616M;
            Assert.That(Math.Abs(price - expected), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PRT2AWithWoodAntaMax_PriceIsCorrect()
        {
            var p = new AALAM(new Ral1013(), new Ral1013());
            var prt2a = new PRT2A(3000L, 4500L, "SX", false, false, new L4Egdes());
            var price = p.GetMaterialPrice(prt2a);
            var expected = (876M + 38M) * 3M * 4.5M + 616M;
            Assert.That(Math.Abs(price - expected), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PRT2AWithWood_PriceIsCorrect()
        {
            var p = new AATT(new Ral1013(), new Ral1013());
            var prt2a = new PRT2A(3000L, 4500L, "SX", false, false, new L4Egdes());
            var price = p.GetMaterialPrice(prt2a);
            var expected = (876M + 38M) * 3M * 4.5M + 616M;
            Assert.That(Math.Abs(price - expected), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PRT2AWithWoodAntaMaxLowArea_PriceIsCorrect()
        {
            var p = new AALAM(new Ral1013(), new Ral1013());
            var prt2a = new PRT2A(1000L, 1000L, "SX", false, false, new L4Egdes());
            var price = p.GetMaterialPrice(prt2a);
            var expected = (876M + 38M) * 1.8M + 616M;
            Assert.That(Math.Abs(price - expected), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PRT1AWithPvcAntaMax_PriceIsCorrect()
        {
            var p = new IPCAM(new Ral1013());
            var prt1a = new PRT1A(3000L, 4500L, "SX", false, false, new Z4Egdes());
            var price = p.GetMaterialPrice(prt1a);
            var expected = (635M + 38M) * 3M * 4.5M + 616M;
            Assert.That(Math.Abs(price - expected), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PRT1AWithPvcAntaMaxLowArea_PriceIsCorrect()
        {
            var p = new IPCAM(new Ral1013());
            var prt1a = new PRT1A(1000L, 1000L, "SX", false, false, new Z4Egdes());
            var price = p.GetMaterialPrice(prt1a);
            var expected = (635M + 38M) * 1.5M + 616M;
            Assert.That(Math.Abs(price - expected), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PRT2AWithPvcAntaMax_PriceIsCorrect()
        {
            var p = new IPCAM(new Ral1013());
            var prt2a = new PRT2A(3000L, 4500L, "SX", false, false, new Z4Egdes());
            var price = p.GetMaterialPrice(prt2a);
            var expected = (635M + 38M) * 3M * 4.5M + 616M;
            Assert.That(Math.Abs(price - expected), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PRT2AWithPvcAntaClassic_PriceIsCorrect()
        {
            var p = new IPC(new Ral1013());
            var mat = new PRT2A(2500L, 1200L, "SX", false, false, new Z3EgdesTrimmedThreshold());
            var price = p.GetMaterialPrice(mat);
            var expected = (635M + 38M) * 2.5M * 1.2M + 616M;
            Assert.That(Math.Abs(price - expected), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_SILWithPvcAntaClassic_PriceIsCorrect()
        {
            var p = new IPC(new Ral1013());
            var mat = new SIL(2500L, 1100L, "SX", false, false, new Z3EgdesTrimmedThreshold());
            var price = p.GetMaterialPrice(mat);
            var expected = (635M + 38M) * 2.5M * 1.1M;
            Assert.That(Math.Abs(price - expected), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PRT2AWithPvcAntaMaxLowArea_PriceIsCorrect()
        {
            var p = new IPCAM(new Ral1013());
            var prt2a = new PRT2A(1000L, 1000L, "SX", false, false, new Z4Egdes());
            var price = p.GetMaterialPrice(prt2a);
            var expected = (635M + 38M) * 1.8M + 616M;
            Assert.That(Math.Abs(price - expected), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_AATTPlain_PriceIsCorrect()
        {
            var p = new AATT(new DarkColoredAsh(), new Ral1013());
            var mat = new F1A(1500L, 1000L, "SX", false, false, new L4Egdes());
            var price = p.GetMaterialPrice(mat);
            Assert.That(Math.Abs(price - 831M * 1.5M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_AATTWithOpaqueGlass_PriceIsCorrect()
        {
            var p = new AATT(new DarkColoredAsh(), new Ral1013());
            var mat = new F1A(1500L, 1000L, "SX", true, false, new L4Egdes());
            var price = p.GetMaterialPrice(mat);
            Assert.That(Math.Abs(price - 841M * 1.5M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_WoodWithWireCover_PriceIsCorrect()
        {
            var p = new AATT(new DarkColoredAsh(), new Ral1013());
            var mat = new F1A(1500L, 1000L, "SX", false, true, new L4Egdes());
            var price = p.GetMaterialPrice(mat);
            const decimal windowPrice = 831M * 1.5M;
            const decimal wireCoverPrice = 3 * 2 * 18.2M;
            Assert.That(Math.Abs(price - (windowPrice + wireCoverPrice)), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_WoodWithWireCoverAndZFrame_PriceIsCorrect()
        {
            var p = new AATT(new DarkColoredAsh(), new Ral1013());
            var mat = new F1A(1500L, 1000L, "SX", false, true, new Z3EgdesLThreshold());
            var price = p.GetMaterialPrice(mat);
            const decimal windowPrice = 831M * 1.5M;
            const decimal wireCoverPrice = 3 * 2 * 18.2M;
            const decimal framePrice = 8M * 1.5M;
            Assert.That(Math.Abs(price - (windowPrice + wireCoverPrice + framePrice)), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PvcWithWireCover_PriceIsCorrect()
        {
            var p = new IPC(new White9010());
            var mat = new F1A(2000L, 2000L, "SX", false, true, new Z4Egdes());
            var price = p.GetMaterialPrice(mat);
            const decimal windowPrice = (528M + 38M) * 4M;
            const decimal wireCoverPrice = 6.5M * 5.5M;
            Assert.That(Math.Abs(price - (windowPrice + wireCoverPrice)), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_WoodWithVasc_PriceIsCorrect()
        {
            var p = new ELA(new Ral1013(), new Ral1013());
            var vas = new VASC(2500L, 2400L, string.Empty, false, false, new L4Egdes());
            var price = p.GetMaterialPrice(vas);
            Assert.That(Math.Abs(price - (985 + 38) * 2.5M * 2.4M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_WoodWithVasm_PriceIsCorrect()
        {
            var p = new ELA(new Ral1013(), new Ral1013());
            var vasm = new VASM(2500L, 2400L, string.Empty, false, false, new L4Egdes());
            var price = p.GetMaterialPrice(vasm);
            Assert.That(Math.Abs(price - ((985 + 38) * 2.5M * 2.4M + 180)), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_WoodWithSrafLessOrEqualThan240_PriceIsCorrect()
        {
            var p = new ELA(new Ral1013(), new Ral1013());
            var sraf = new SRAF(2500L, 2400L, string.Empty, false, false, new L4Egdes());
            var price = p.GetMaterialPrice(sraf);
            Assert.That(Math.Abs(price - ((985 + 38) * 2.5M * 2.4M + 1300)), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_WoodWithSrafGreaterThan240_PriceIsCorrect()
        {
            var p = new ELA(new Ral1013(), new Ral1013());
            var sraf = new SRAF(2500L, 2401L, string.Empty, false, false, new L4Egdes());
            var price = p.GetMaterialPrice(sraf);
            Assert.That(Math.Abs(price - ((985 + 38) * 2.5M * 2.401M + 3000)), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_WoodWithSrlaLessOrEqualThan240_PriceIsCorrect()
        {
            var p = new ELA(new Ral1013(), new Ral1013());
            var srla = new SRLA(2500L, 2400L, string.Empty, false, false, new L4Egdes());
            var price = p.GetMaterialPrice(srla);
            Assert.That(Math.Abs(price - ((985 + 38) * 2.5M * 2.4M + 1300)), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_WoodWithSrlaGreaterThan240_PriceIsCorrect()
        {
            var p = new ELA(new Ral1013(), new Ral1013());
            var srla = new SRLA(2500L, 2401L, string.Empty, false, false, new L4Egdes());
            var price = p.GetMaterialPrice(srla);
            Assert.That(Math.Abs(price - ((985 + 38) * 2.5M * 2.401M + 3000)), Is.LessThan(1e-3M));
        }
    }
}