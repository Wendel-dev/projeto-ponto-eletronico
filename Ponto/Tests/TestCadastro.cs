using Ponto.DataBase;
using Ponto.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ponto.Tests
{
    internal class TestCadastro
    {
        public TestCadastro()
        {
            BancoDeDados.setConexao(BancoDeDados.bancoInicial);
            CriacaoDeBancoDeDados.inserirDados(RegistrationTable.getTables(0), "usuario", "'testCadastro'");
            testarCriarTabelaAtividade1();
            testarCriarTabelaBancoDeHoras2();
            testarCadastrarUsuarioNoBancoDeDados3();
            testarFinalizarInscricao4();
            testarCadatrarUsuarioProxPasso5();
            testarUsuarioCadastrado6();
            testarTentarCadastrarUsuario7();
            tastarBtnCadastrarUsuario8();
        }
        public void testarCriarTabelaAtividade1()
        {
            Program.cadastrarUsuario = new Cadastro();
            var sut = Program.cadastrarUsuario;
            BancoDeDados.setConexao("testCadastro");
            sut.criarTabelaAtividades();
            var teste2 = BancoDeDados.lerBDstring("atividade", "atividades", null, null);
            if (teste2 != null)
            {
                BancoDeDados.deletarTabela(DefaultTableData.getTables(0));
                Console.WriteLine("teste ok");
            }
            else DataStructureTests.MensagemDeErro("Falha no teste testarCriarTabela2 =", "true");
            BancoDeDados.setConexao(BancoDeDados.bancoInicial);
            sut.Close();
        }
        public void testarCriarTabelaBancoDeHoras2()
        {
            Program.cadastrarUsuario = new Cadastro();
            var sut = Program.cadastrarUsuario;
            BancoDeDados.setConexao("testCadastro");
            sut.criarTabelaBancoDeHoras();
            var teste1 = BancoDeDados.lerBDdata(0);
            if (teste1 != null)
            {
                BancoDeDados.deletarTabela(DefaultTableData.getTables(1));
                Console.WriteLine("teste ok");
            }
            else MessageBox.Show("Falha no teste testarCriarTabela1 = true");
            sut.Close();
            BancoDeDados.setConexao(BancoDeDados.bancoInicial);
        }
        public void testarCadastrarUsuarioNoBancoDeDados3()
        {
            Program.cadastrarUsuario = new Cadastro();
            var sut = Program.cadastrarUsuario;
            sut.inserirUsuarioNoBancoDeDadosGeral("testCadastro", "testCadastro");
            var teste = BancoDeDados.lerBDstring("usuario", RegistrationTable.getTables(0), "usuario", "'testCadastro'");
            if (teste != null)
            {
                BancoDeDados.deletarLinhaDaTabela(RegistrationTable.getTables(0), new string[] { "usuario" }, new string[] { "'testCadastro'" });
                Console.WriteLine("teste ok");
            }
            else MessageBox.Show($"Falha no teste testarCadastrarUsuarioNoBancoDeDados = {teste}");
            sut.Close();
        }
        public void testarFinalizarInscricao4()
        {
            Program.cadastrarUsuario = new Cadastro();
            var sut = Program.cadastrarUsuario;
            sut.finalizarInscricao("testCadastro2", "testCadastro2");
            try
            {
                BancoDeDados.deletarTabela(DefaultTableData.getTables(0));
                BancoDeDados.deletarTabela(DefaultTableData.getTables(1));
                BancoDeDados.deletarBancoDeDados("testCadastro2");
                BancoDeDados.setConexao(BancoDeDados.bancoInicial);
                BancoDeDados.deletarLinhaDaTabela("dadosusuarios", new string[] { "usuario" }, new string[] { "'testCadastro2'" });
                Console.WriteLine("teste ok");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " testarFinalizarInscricao");
            }
            sut.Close();
        }
        public void testarCadatrarUsuarioProxPasso5()
        {
            Program.cadastrarUsuario = new Cadastro();
            var sut = Program.cadastrarUsuario;
            sut.setUsuarioSenha("testCadastro", "testCadastro");
            sut.cadastrarUsuarioProxPasso();
            if (Program.criarAtividade != null)
            {
                Program.criarAtividade.Close();
                Program.criarAtividade = null;
                Console.WriteLine("teste ok");
            }
            else MessageBox.Show("Falha no teste testarCadatrarUsuario = false");
            sut.Close();

        }
        public void testarUsuarioCadastrado6()
        {
            BancoDeDados.setConexao(BancoDeDados.bancoInicial);
            CriacaoDeBancoDeDados.inserirDados(RegistrationTable.getTables(0), "usuario", "'testCadastro'");
            Program.cadastrarUsuario = new Cadastro();
            var sut = Program.cadastrarUsuario;
            sut.setUsuarioSenha("testCadastro", "testCadastro");
            if (sut.isCadastrado())
            {
                Console.WriteLine("teste ok");
            }
            else MessageBox.Show("Falha no teste 1 testarUsuarioCadastrado6 = false");
            sut.setUsuarioSenha("testCadastro2", "testCadastro2");
            if (!sut.isCadastrado())
            {
                Console.WriteLine("teste ok");
            }
            else MessageBox.Show("Falha no teste 2 testarUsuarioCadastrado6 = false");
            sut.Close();
        }
        public void testarTentarCadastrarUsuario7()
        {
            Program.cadastrarUsuario = new Cadastro();
            var sut = Program.cadastrarUsuario;
            sut.setUsuarioSenha("testCadastro2", "testCadastro2");
            sut.tentarCadastrarUsuario();
            if (Program.criarAtividade != null)
            {
                Program.criarAtividade.Close();
                Console.WriteLine("teste ok");
            }
            else MessageBox.Show("Falha no teste testarTentarCadastrarUsuario = false");
            sut.Close();
        }
        public void tastarBtnCadastrarUsuario8()
        {
            Program.cadastrarUsuario = new Cadastro();
            var sut = Program.cadastrarUsuario;
            sut.testarBtnCadastrar("testCadastro2", "testCadastro2", "testCadastro2");
            if (Program.criarAtividade != null)
            {
                Program.criarAtividade.Close();
                Program.criarAtividade = null;
                Console.WriteLine("teste ok");
            }
            else MessageBox.Show("Falha no teste 1 tastarBtnCadastrarUsuario1 = null");
            Program.cadastrarUsuario = new Cadastro();
            sut = Program.cadastrarUsuario;
            sut.testarBtnCadastrar("testCadastro", "testCadastro", "teste2");
            if (Program.criarAtividade == null) Console.WriteLine("teste ok");
            else MessageBox.Show("Falha no teste 2 tastarBtnCadastrarUsuario2 = null");
            sut.Close();
        }
    }
}
