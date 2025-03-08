using DomainModel.Classes.Materials.ConcreteMaterials;
using DomainModel.Classes.Products;

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
            var p = ProductFactory.CreateByCode("ELA");
            var cas = new CAS(2500L);
            var price = p.GetMaterialPrice(cas);
            Assert.That(Math.Abs(price - 285 * 2.5M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PvcWithCasAllowedLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("SP");
            var cas = new CAS(2500L);
            var price = p.GetMaterialPrice(cas);
            Assert.That(Math.Abs(price - 268 * 2.5M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_WoodWithCasLowLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("ELA");
            var cas = new CAS(500L);
            var price = p.GetMaterialPrice(cas);
            Assert.That(Math.Abs(price - 285M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PvcWithCasLowLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("IPC");
            var cas = new CAS(500L);
            var price = p.GetMaterialPrice(cas);
            Assert.That(Math.Abs(price - 268M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_WoodWithDoubleAllowedLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("ELA");
            var f1a = new F1A(2000L, 3000L);
            var price = p.GetMaterialPrice(f1a);
            Assert.That(Math.Abs(price - (985 + 38) * 2M * 3M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_WoodWithDoubleLowLengthF1A_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("ELA");
            var f1a = new F1A(1000L, 1000L);
            var price = p.GetMaterialPrice(f1a);
            Assert.That(Math.Abs(price - (985 + 38) * 1.5M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_WoodWithDoubleLowLengthF2A_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("ELA");
            var f2a = new F2A(1000L, 1000L);
            var price = p.GetMaterialPrice(f2a);
            Assert.That(Math.Abs(price - (985 + 38) * 1.8M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_RALTWithDoubleAllowedLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("RALT");
            var f1a = new F1A(5000L, 4000L);
            var price = p.GetMaterialPrice(f1a);
            Assert.That(Math.Abs(price - (842 + 38) * 5M * 4M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_AALAMWithDoubleAllowedLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("AALAM");
            var f1a = new F1A(5000L, 4000L);
            var price = p.GetMaterialPrice(f1a);
            Assert.That(Math.Abs(price - (876 + 38) * 5M * 4M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_AATTWithDoubleAllowedLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("AATT");
            var f1a = new F1A(5000L, 4000L);
            var price = p.GetMaterialPrice(f1a);
            Assert.That(Math.Abs(price - (694 + 38) * 5M * 4M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_IPCWithDoubleAllowedLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("IPC");
            var f1a = new F1A(5000L, 4000L);
            var price = p.GetMaterialPrice(f1a);
            Assert.That(Math.Abs(price - (528 + 38) * 5M * 4M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_IPNWithDoubleAllowedLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("IPN");
            var f1a = new F1A(5000L, 4000L);
            var price = p.GetMaterialPrice(f1a);
            Assert.That(Math.Abs(price - (528 + 38) * 5M * 4M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_SPWithFIXALowLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("SP");
            var f1a = new F1A(1500L, 1200L);
            var price = p.GetMaterialPrice(f1a);
            Assert.That(Math.Abs(price - (635 + 38) * 1.5M * 1.2M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_ELAWithFixed_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("ELA");
            var fix = new FIX(1800L, 1350L);
            var price = p.GetMaterialPrice(fix);
            Assert.That(Math.Abs(price - (568 + 38) * 1.8M * 1.35M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_SLFWithFixed_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("RALT");
            var slf = new SLF(1120L, 2400L);
            var price = p.GetMaterialPrice(slf);
            Assert.That(Math.Abs(price - (526 + 38) * 1.12M * 2.4M), Is.LessThan(1e-3M));
        }
        [Test]
        public void Test_AATTWithFixed_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("AATT");
            var fld = new FLD(1780L, 1500L);
            var price = p.GetMaterialPrice(fld);
            Assert.That(Math.Abs(price - (501 + 38) * 1.78M * 1.5M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_IPCWithFixed_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("IPC");
            var fls = new FLS(1300L, 1800L);
            var price = p.GetMaterialPrice(fls);
            Assert.That(Math.Abs(price - (385 + 38) * 1.3M * 1.8M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_COPWithPvc_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("IPC");
            var cop = new COP(3000L);
            var price = p.GetMaterialPrice(cop);
            Assert.That(Math.Abs(price - 6 * 3M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_COPWithPvcLowLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("IPC");
            var cop = new COP(100L);
            var price = p.GetMaterialPrice(cop);
            Assert.That(Math.Abs(price - 6 * 0.1M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_COPWithWood_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("AATT");
            var cop = new COP(3000L);
            var price = p.GetMaterialPrice(cop);
            Assert.That(Math.Abs(price - 18.2M * 3M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_COPWithWoodLowLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("AATT");
            var cop = new COP(100L);
            var price = p.GetMaterialPrice(cop);
            Assert.That(Math.Abs(price - 18.2M * 0.1M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_FROWithWood_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("AATT");
            var fro = new FRO(3000L);
            var price = p.GetMaterialPrice(fro);
            Assert.That(Math.Abs(price - 142M * 3M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_FROWithWoodLowLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("AATT");
            var fro = new FRO(100L);
            var price = p.GetMaterialPrice(fro);
            Assert.That(Math.Abs(price - 142M * 1M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_FROWithPvc_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("IPC");
            var fro = new FRO(3000L);
            var price = p.GetMaterialPrice(fro);
            Assert.That(Math.Abs(price - 142M * 3M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_FROWithPvcLowLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("IPC");
            var fro = new FRO(100L);
            var price = p.GetMaterialPrice(fro);
            Assert.That(Math.Abs(price - 142M * 1M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PRT1AWithWoodAntaMax_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("AALAM");
            var prt1a = new PRT1A(3000L, 4500L);
            var price = p.GetMaterialPrice(prt1a);
            var expected = (876M + 38M) * 3M * 4.5M + 616M;
            Assert.That(Math.Abs(price - expected), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PRT1AWithWoodAntaMaxLowArea_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("AALAM");
            var prt1a = new PRT1A(1000L, 1000L);
            var price = p.GetMaterialPrice(prt1a);
            var expected = (876M + 38M) * 1.5M + 616M;
            Assert.That(Math.Abs(price - expected), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PRT2AWithWoodAntaMax_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("AALAM");
            var prt2a = new PRT2A(3000L, 4500L);
            var price = p.GetMaterialPrice(prt2a);
            var expected = (876M + 38M) * 3M * 4.5M + 616M;
            Assert.That(Math.Abs(price - expected), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PRT2AWithWoodAntaMaxLowArea_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("AALAM");
            var prt2a = new PRT2A(1000L, 1000L);
            var price = p.GetMaterialPrice(prt2a);
            var expected = (876M + 38M) * 1.8M + 616M;
            Assert.That(Math.Abs(price - expected), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PRT1AWithPvcAntaMax_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("IPCAM");
            var prt1a = new PRT1A(3000L, 4500L);
            var price = p.GetMaterialPrice(prt1a);
            var expected = (635M + 38M) * 3M * 4.5M + 616M;
            Assert.That(Math.Abs(price - expected), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PRT1AWithPvcAntaMaxLowArea_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("IPCAM");
            var prt1a = new PRT1A(1000L, 1000L);
            var price = p.GetMaterialPrice(prt1a);
            var expected = (635M + 38M) * 1.5M + 616M;
            Assert.That(Math.Abs(price - expected), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PRT2AWithPvcAntaMax_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("IPCAM");
            var prt2a = new PRT2A(3000L, 4500L);
            var price = p.GetMaterialPrice(prt2a);
            var expected = (635M + 38M) * 3M * 4.5M + 616M;
            Assert.That(Math.Abs(price - expected), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PRT2AWithPvcAntaMaxLowArea_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("IPCAM");
            var prt2a = new PRT2A(1000L, 1000L);
            var price = p.GetMaterialPrice(prt2a);
            var expected = (635M + 38M) * 1.8M + 616M;
            Assert.That(Math.Abs(price - expected), Is.LessThan(1e-3M));
        }
    }
}