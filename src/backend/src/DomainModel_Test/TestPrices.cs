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
            var m = MaterialFactory.CreateByCode("CAS", 2500L);
            var price = p.getMaterialPrice(m);
            Assert.That(price, Is.EqualTo(285*2.5));
        }

        [Test]
        public void Test_PvcWithCasAllowedLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("SP");
            var m = MaterialFactory.CreateByCode("CAS", 2500L);
            var price = p.getMaterialPrice(m);
            Assert.That(price, Is.EqualTo(268*2.5));
        }

        [Test]
        public void Test_WoodWithCasLowLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("ELA");
            var m = MaterialFactory.CreateByCode("CAS", 500L);
            var price = p.getMaterialPrice(m);
            Assert.That(price, Is.EqualTo(285));
        }

        [Test]
        public void Test_PvcWithCasLowLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("IPC");
            var m = MaterialFactory.CreateByCode("CAS", 500L);
            var price = p.getMaterialPrice(m);
            Assert.That(price, Is.EqualTo(268));
        }

        [Test]
        public void Test_WoodWithDoubleAllowedLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("ELA");
            var m = MaterialFactory.CreateByCode("F1A", 2000L, 3000L);
            var price = p.getMaterialPrice(m);
            Assert.That(price, Is.EqualTo((985+38)*2*3));
        }

        [Test]
        public void Test_WoodWithDoubleLowLengthF1A_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("ELA");
            var m = MaterialFactory.CreateByCode("F1A", 1000L, 1000L);
            var price = p.getMaterialPrice(m);
            Assert.That(price, Is.EqualTo((985 + 38) * 1.5));
        }

        [Test]
        public void Test_WoodWithDoubleLowLengthF2A_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("ELA");
            var m = MaterialFactory.CreateByCode("F2A", 1000L, 1000L);
            var price = p.getMaterialPrice(m);
            Assert.That(price, Is.EqualTo((985 + 38) * 1.8));
        }

        [Test]
        public void Test_RALTWithDoubleAllowedLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("RALT");
            var m = MaterialFactory.CreateByCode("F1A", 5000L, 4000L);
            var price = p.getMaterialPrice(m);
            Assert.That(price, Is.EqualTo((842 + 38) * 5 * 4));
        }

        [Test]
        public void Test_AALAMWithDoubleAllowedLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("AALAM");
            var m = MaterialFactory.CreateByCode("F1A", 5000L, 4000L);
            var price = p.getMaterialPrice(m);
            Assert.That(price, Is.EqualTo((876 + 38) * 5 * 4));
        }

        [Test]
        public void Test_AATTWithDoubleAllowedLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("AATT");
            var m = MaterialFactory.CreateByCode("F1A", 5000L, 4000L);
            var price = p.getMaterialPrice(m);
            Assert.That(price, Is.EqualTo((694 + 38) * 5 * 4));
        }

        [Test]
        public void Test_IPCWithDoubleAllowedLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("IPC");
            var m = MaterialFactory.CreateByCode("F1A", 5000L, 4000L);
            var price = p.getMaterialPrice(m);
            Assert.That(price, Is.EqualTo((528 + 38) * 5 * 4));
        }

        [Test]
        public void Test_IPNWithDoubleAllowedLength_PriceIsCorrect()
        {
            var p = ProductFactory.CreateByCode("IPN");
            var m = MaterialFactory.CreateByCode("F1A", 5000L, 4000L);
            var price = p.getMaterialPrice(m);
            Assert.That(price, Is.EqualTo((528 + 38) * 5 * 4));
        }
    }
}