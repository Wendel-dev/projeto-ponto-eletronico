using Mysqlx.Crud;
using Ponto.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ponto.Interface;
using Ponto.DataBase;
using Ponto.Tests;

namespace Ponto.Tests
{
    internal class TDDRaiz
    {
        public TDDRaiz()
        {
            new DataStructureTests();
            MessageBox.Show(DataStructureTests.diasUteis.ToString());
            DialogResult resultado = MessageBox.Show("iniciar teste?","",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                DialogResult resultado0 = MessageBox.Show("iniciar TestDatas?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado0 == DialogResult.Yes) new TestDatas();
                DialogResult resultado1 = MessageBox.Show("iniciar TestLogin?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado1 == DialogResult.Yes) new TestLogin();
                DialogResult resultado2 = MessageBox.Show("iniciar TestCadastro?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado2 == DialogResult.Yes) new TestCadastro();
                DialogResult resultado3 = MessageBox.Show("iniciar TestCadastroAtividade?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado3 == DialogResult.Yes) new TestCadastroAtividade();
                DialogResult resultado4 = MessageBox.Show("iniciar TestTempoDeAtividade?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado4 == DialogResult.Yes) new TestTempoDeAtividade();
                DialogResult resultado5 = MessageBox.Show("iniciar TestRedefinirSenha?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado5 == DialogResult.Yes) new TestRedefinirSenha();
                DialogResult resultado6 = MessageBox.Show("iniciar TestPaginaInicial?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado6 == DialogResult.Yes) new TestPaginaInicial();
                /* o teste está com defeito pode ser passado para NUnit para melhor analise
                DialogResult resultado7 = MessageBox.Show("iniciar testeUnidadecadastrar?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado7 == DialogResult.Yes) testeUnidadecadastrar();*/
            }
            MessageBox.Show("Teste finalizado");
        }
        public void testeUnidadecadastrar()
        {
            testeAbrirTelaDeCadastro();
            testeDigitarUsuarioEclicarNoBotaoCadastrar();
            testeDigitarAtividadeEclicarNoBotaoCadastrarAtividade();
            testeEscolherDataInicialDaAtividadeEclicarNoBotaoFinalizar();
        }
        public void testeAbrirTelaDeCadastro()
        {
            var sut = new Login();
            sut.testcadastrar();
            if (Program.cadastrarUsuario != null)Console.WriteLine("teste ok");
            else MessageBox.Show("Falha no testarAbrirCadastro = null");
        }
        public void testeDigitarUsuarioEclicarNoBotaoCadastrar()
        {
            var sut = Program.cadastrarUsuario;
            sut.testarBtnCadastrar("testeUnidade", "testeUnidade", "testeUnidade2");
            if (Program.criarAtividade == null) Console.WriteLine("teste ok");
            else MessageBox.Show("Falha no teste 2 tastarBtnCadastrarUsuario2 = null");
            sut.testarBtnCadastrar("testeUnidade", "testeUnidade", "testeUnidade");
            if (Program.criarAtividade != null)Console.WriteLine("teste ok");
            else MessageBox.Show("Falha no teste 1 tastarBtnCadastrarUsuario1 = null");
        }
        public void testeDigitarAtividadeEclicarNoBotaoCadastrarAtividade()
        {
            var sut = Program.criarAtividade;
            sut.testarBtn("a", "teste");
            if (Program.cadastrarInicio == null) Console.WriteLine("teste ok");
            else MessageBox.Show("Falha no teste 2 testarBtnCadastro17 = null");
            sut.testarBtn("1", "teste");
            if (Program.cadastrarInicio != null)Console.WriteLine("teste ok");
            else MessageBox.Show("Falha no teste 1 testarBtnCadastro17 = null");
        }
        public void testeEscolherDataInicialDaAtividadeEclicarNoBotaoFinalizar()
        {
            var sut = Program.cadastrarInicio;
            sut.testarBtn(new DateTime(2024, 2, 1));
            var teste1 = BancoDeDados.lerBDdata(0);
            if (teste1.ToString("dd/MM/yyyy").Equals(DateTime.Today.ToString("dd/MM/yyyy"))) Console.WriteLine("teste ok");
            else MessageBox.Show($"Falha no teste 1 testarBtnAtualizarAtividade23 = {teste1}");
            var teste2 = BancoDeDados.lerBDdouble("teste1", DefaultTableData.getTables(1), null, null);
            if (teste2 == DataStructureTests.diasUteis - 20) Console.WriteLine("teste ok");
            else MessageBox.Show($"Falha no teste 2 testarBtnAtualizarAtividade23 = {teste2}");
            BancoDeDados.setConexao(BancoDeDados.bancoInicial);
            BancoDeDados.deletarLinhaDaTabela(RegistrationTable.getTables(0), new string[] { RegistrationTable.getColumnTableUserData(0) }, new string[] { "'teste1'" });
            BancoDeDados.deletarBancoDeDados("testeUnidade");
            Program.paginaInicial.Close();
        }
    }
}
