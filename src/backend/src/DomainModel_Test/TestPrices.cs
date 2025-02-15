using DomainModel.Classes.Materials;
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
            var m = MaterialFactory.CreateByCode("CAS", 2500L, 0L);
            var price = p.GetMaterialPrice(m);
            Assert.That(Math.Abs(price - 285 * 2.5M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PvcWithCasAllowedLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("SP");
            var m = MaterialFactory.CreateByCode("CAS", 2500L, 0L);
            var price = p.GetMaterialPrice(m);
            Assert.That(Math.Abs(price - 268 * 2.5M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_WoodWithCasLowLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("ELA");
            var m = MaterialFactory.CreateByCode("CAS", 500L, 0L);
            var price = p.GetMaterialPrice(m);
            Assert.That(Math.Abs(price - 285M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_PvcWithCasLowLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("IPC");
            var m = MaterialFactory.CreateByCode("CAS", 500L, 0L);
            var price = p.GetMaterialPrice(m);
            Assert.That(Math.Abs(price - 268M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_WoodWithDoubleAllowedLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("ELA");
            var m = MaterialFactory.CreateByCode("F1A", 2000L, 3000L);
            var price = p.GetMaterialPrice(m);
            Assert.That(Math.Abs(price - (985 + 38) * 2M * 3M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_WoodWithDoubleLowLengthF1A_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("ELA");
            var m = MaterialFactory.CreateByCode("F1A", 1000L, 1000L);
            var price = p.GetMaterialPrice(m);
            Assert.That(Math.Abs(price - (985 + 38) * 1.5M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_WoodWithDoubleLowLengthF2A_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("ELA");
            var m = MaterialFactory.CreateByCode("F2A", 1000L, 1000L);
            var price = p.GetMaterialPrice(m);
            Assert.That(Math.Abs(price - (985 + 38) * 1.8M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_RALTWithDoubleAllowedLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("RALT");
            var m = MaterialFactory.CreateByCode("F1A", 5000L, 4000L);
            var price = p.GetMaterialPrice(m);
            Assert.That(Math.Abs(price - (842 + 38) * 5M * 4M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_AALAMWithDoubleAllowedLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("AALAM");
            var m = MaterialFactory.CreateByCode("F1A", 5000L, 4000L);
            var price = p.GetMaterialPrice(m);
            Assert.That(Math.Abs(price - (876 + 38) * 5M * 4M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_AATTWithDoubleAllowedLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("AATT");
            var m = MaterialFactory.CreateByCode("F1A", 5000L, 4000L);
            var price = p.GetMaterialPrice(m);
            Assert.That(Math.Abs(price - (694 + 38) * 5M * 4M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_IPCWithDoubleAllowedLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("IPC");
            var m = MaterialFactory.CreateByCode("F1A", 5000L, 4000L);
            var price = p.GetMaterialPrice(m);
            Assert.That(Math.Abs(price - (528 + 38) * 5M * 4M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_IPNWithDoubleAllowedLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("IPN");
            var m = MaterialFactory.CreateByCode("F1A", 5000L, 4000L);
            var price = p.GetMaterialPrice(m);
            Assert.That(Math.Abs(price - (528 + 38) * 5M * 4M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_SPWithFIXALowLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("SP");
            var m = MaterialFactory.CreateByCode("F1A", 1500L, 1200L);
            var price = p.GetMaterialPrice(m);
            Assert.That(Math.Abs(price - (635 + 38) * 1.5M * 1.2M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_ELAWithFixed_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("ELA");
            var m = MaterialFactory.CreateByCode("FIX", 1800L, 1350L);
            var price = p.GetMaterialPrice(m);
            Assert.That(Math.Abs(price - (568 + 38) * 1.8M * 1.35M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_RALTWithFixed_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("RALT");
            var m = MaterialFactory.CreateByCode("FIXA", 1120L, 2400L);
            var price = p.GetMaterialPrice(m);
            Assert.That(Math.Abs(price - (526 + 38) * 1.12M * 2.4M), Is.LessThan(1e-3M));
        }
        [Test]
        public void Test_AATTWithFixed_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("AATT");
            var m = MaterialFactory.CreateByCode("FLD", 1780L, 1500L);
            var price = p.GetMaterialPrice(m);
            Assert.That(Math.Abs(price - (501 + 38) * 1.78M * 1.5M), Is.LessThan(1e-3M));
        }

        [Test]
        public void Test_IPCWithFixed_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("IPC");
            var m = MaterialFactory.CreateByCode("FLS", 1300L, 1800L);
            var price = p.GetMaterialPrice(m);
            Assert.That(Math.Abs(price - (385 + 38) * 1.3M * 1.8M), Is.LessThan(1e-3M));
        }
    }
}