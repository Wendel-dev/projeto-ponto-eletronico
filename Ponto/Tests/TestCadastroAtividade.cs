using Ponto.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ponto.Tests
{
    internal class TestCadastroAtividade
    {
        public TestCadastroAtividade()
        {
            testarBtnCadastro17();
        }
        public void testarBtnCadastro17()
        {
            Program.criarAtividade = new CriarAtividade("senha", "usuario");
            var sut = Program.criarAtividade;
            sut.testarBtn("1", "teste");
            if (Program.cadastrarInicio != null)
            {
                Program.cadastrarInicio.Close();
                Program.cadastrarInicio = null;
                Console.WriteLine("teste ok");
            }
            else MessageBox.Show("Falha no teste 1 testarBtnCadastro17 = null");
            sut.testarBtn("a", "teste");
            if (Program.cadastrarInicio == null) Console.WriteLine("teste ok");
            else MessageBox.Show("Falha no teste 2 testarBtnCadastro17 = null");
            sut.Close();
        }
    }
}
