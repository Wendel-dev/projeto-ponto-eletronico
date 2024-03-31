using Ponto.Classes;
using Ponto.Interface;

namespace TestProject1
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            PaginaInicial p = new PaginaInicial();
            Datas data = new Datas();
            DateTime inicio = new DateTime(2024, 03, 15);
            DateTime final = new DateTime(2024, 03, 17, 01, 00, 00);
            var teste = data.calculateDiasUteis(inicio, final);
            Assert.That(teste, Is.EqualTo(0));
        }
    }
}