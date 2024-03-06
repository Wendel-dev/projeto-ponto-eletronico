using Ponto.DataBase;
using Ponto.Interface;
using Ponto.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ponto.Tests
{
    internal class TestLogin
    {
        public TestLogin()
        {
            testarChecarSeSenhaEstaCorreta1();
            testarChecarSeSenhaEstaErrada2();
            testarSeLoginAbreAPaginaInicial3();
            testarSeLoginMostraMensagemDeErroComSenhaErrada4();
            testarSeLoginMostraMensagemDeErroComSenhaNula5();
            testarInicializacaoDoBancoDeDadosDeUsuarios6();
            testarAbrirTelaDeCadastro7();
            testarAbrirTelaDeRecuperarSenha8();
        }
        public void testarChecarSeSenhaEstaCorreta1()
        {
            if (LoginController.senhaCorreta("testlogin", "testlogin")) Console.WriteLine("teste ok");
            else DataStructureTests.MensagemDeErro("testarChecarSeSenhaEstaCorreta1", "Senha incorreta");
        }
        public void testarChecarSeSenhaEstaErrada2()
        {
            if (!LoginController.senhaCorreta("testlogin", "b")) Console.WriteLine("teste ok");
            else DataStructureTests.MensagemDeErro("testarChecarSeSenhaEstaErrada2", "Senha Correta");
        }
        public void testarSeLoginAbreAPaginaInicial3()
        {
            Program.login = new Login();
            var sut = Program.login;
            sut.Show();
            sut.testLogin("testlogin", "testlogin");
            if (Program.paginaInicial != null)
            {
                Program.paginaInicial.Close();
                Program.paginaInicial = null;
                Console.WriteLine("teste ok");
            }
            else DataStructureTests.MensagemDeErro("testarSeLoginAbreAPaginaInicial3", "null");
            Program.login = null;
            sut.Close();
        }
        public void testarSeLoginMostraMensagemDeErroComSenhaErrada4()
        {
            Program.login = new Login();
            var sut = Program.login;
            sut.Show();
            sut.testLogin("testlogin", "b");
            if (Program.paginaInicial == null) Console.WriteLine("teste ok");
            else
            {
                Program.paginaInicial.Close();
                Program.paginaInicial = null;
                DataStructureTests.MensagemDeErro("testarSeLoginMostraMensagemDeErroComSenhaErrada4", "Entrou na paginainicial");
            }
            Program.login = null;
            sut.Close();
        }
        public void testarSeLoginMostraMensagemDeErroComSenhaNula5()
        {
            Program.login = new Login();
            var sut = Program.login;
            sut.Show();

            sut.testLogin("", "");
            if (Program.paginaInicial == null) Console.WriteLine("teste ok");
            else
            {
                Program.paginaInicial.Close();
                Program.paginaInicial = null;
                DataStructureTests.MensagemDeErro("testarSeLoginMostraMensagemDeErroComSenhaNula5", "Entrou na paginainicial");
            }

            sut.testLogin(" ", " ");
            if (Program.paginaInicial == null) Console.WriteLine("teste ok");
            else
            {
                Program.paginaInicial.Close();
                Program.paginaInicial = null;
                DataStructureTests.MensagemDeErro("testarSeLoginMostraMensagemDeErroComSenhaNula5", "Entrou na paginainicial");
            }

            Program.login = null;
            sut.Close();
        }
        public void testarInicializacaoDoBancoDeDadosDeUsuarios6()
        {
            Program.login = new Login();
            LoginController.senhaCorreta("testlogin", "testlogin");
            LoginController.iniciarBancoDeUsuario();
            string controleConexao = $"Data source= {Application.StartupPath}\\testlogin.db";
            if (BancoDeDados.caminhoConexao.Equals(controleConexao))Console.WriteLine("teste ok");
            else DataStructureTests.MensagemDeErro("testarInicializacaoDoBancoDeDadosDeUsuarios6", BancoDeDados.caminhoConexao);
            Program.paginaInicial.Close();
            Program.paginaInicial = null;
            Program.login = null;
        }
        public void testarAbrirTelaDeCadastro7()
        {
            Program.login = new Login();
            var sut = Program.login;
            sut.testcadastrar();
            if (Program.cadastrarUsuario != null)
            {
                Program.cadastrarUsuario.Close();
                Program.cadastrarUsuario=null;
                Console.WriteLine("teste ok");
            }
            else DataStructureTests.MensagemDeErro("testarAbrirTelaDeCadastro7", "null");
            Program.login = null; 
            sut.Close();
        }
        public void testarAbrirTelaDeRecuperarSenha8()
        {
            Program.login = new Login();
            var sut = Program.login;
            sut.testRedefinirSenha();
            if (Program.redefinirSenha != null)
            {
                Program.redefinirSenha.Close();
                Console.WriteLine("teste ok");
            }
            else DataStructureTests.MensagemDeErro("testarAbrirTelaDeRecuperarSenha8", "null");
            Program.login = null;
            sut.Close();
        }
    }
}
