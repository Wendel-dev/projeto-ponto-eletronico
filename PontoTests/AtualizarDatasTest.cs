using Ponto;
using Ponto.Classes;

namespace PontoTests
{
    [TestFixture]
    public class AtualizarDatasTest : Datas
    {
        [Test]
        public void testarSeRetornaAquantidadeDeDiasUteisCorreta()
        {
            DateTime data1 = new DateTime(2024/01/01);
            var testeDiasUteis = diasDeTrabalhoAnteriores(data1);
            Assert.That(testeDiasUteis, Is.EqualTo(41));
        }
        [Test]
        public void testarTeste()
        {
            var teste = 1;
            Assert.That(teste, Is.EqualTo(1));
        }
    }
}