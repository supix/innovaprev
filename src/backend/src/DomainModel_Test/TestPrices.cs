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
        public void Test_ELACAS2500mm_PriceIs570()
        {
            var p = ProductFactory.CreateByCode("ELA");
            var m = MaterialFactory.CreateByCode("CAS", 2500L);
            var price = p.getMaterialPrice(m);
            Assert.That(price, Is.EqualTo(285*2.5));
        }

        [Test]
        public void Test_SPCAS2500mm_PriceIs570()
        {
            var p = ProductFactory.CreateByCode("SP");
            var m = MaterialFactory.CreateByCode("CAS", 2500L);
            var price = p.getMaterialPrice(m);
            Assert.That(price, Is.EqualTo(268*2.5));
        }
    }
}