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
    internal class TestRedefinirSenha
    {
        public TestRedefinirSenha()
        {
            testarAlterarSenha24();
            testarConcluirAlteracao25();
            testarTentarRedefinirSenha26();
            testarBtnAlterarSenha27();
        }
        public void testarAlterarSenha24()
        {
            Program.redefinirSenha = new RedefinicaoDeSenha();
            var sut = Program.redefinirSenha;
            sut.testarAlterarSenha("testeSenha", "senha");
            string senha = BancoDeDados.lerBDstring("senha", RegistrationTable.getTables(0), "usuario", "'testeSenha'");
            if (senha.Equals("senha"))
            {
                Console.WriteLine("teste ok");
                BancoDeDados.updateBD(RegistrationTable.getTables(0), "senha", "'testeSenha'", "usuario", "'testeSenha'");
            }
            else MessageBox.Show($"Falha no teste testarAlterarSenha24 = {senha}");
        }
        public void testarConcluirAlteracao25()
        {
            Program.redefinirSenha = new RedefinicaoDeSenha();
            var sut = Program.redefinirSenha;
            sut.testarConcluirAlteracao("testeSenha", "senha");
            string senha = BancoDeDados.lerBDstring("senha", RegistrationTable.getTables(0), "usuario", "'testeSenha'");
            if (senha.Equals("senha"))
            {
                Console.WriteLine("teste ok");
                BancoDeDados.updateBD(RegistrationTable.getTables(0), "senha", "'testeSenha'", "usuario", "'testeSenha'");
            }
            else MessageBox.Show($"Falha no teste testarConcluirAlteracao25 = {senha}");
        }
        public void testarTentarRedefinirSenha26()
        {
            Program.redefinirSenha = new RedefinicaoDeSenha();
            var sut = Program.redefinirSenha;
            sut.testarTentarRedefinirSenha("testeSenha", "senha");
            string senha = BancoDeDados.lerBDstring("senha", RegistrationTable.getTables(0), "usuario", "'testeSenha'");
            if (senha.Equals("senha"))
            {
                Console.WriteLine("teste ok");
                BancoDeDados.updateBD(RegistrationTable.getTables(0), "senha", "'testeSenha'", "usuario", "'testeSenha'");
            }
            else MessageBox.Show($"Falha no teste 1 testarTentarRedefinirSenha26 = {senha}");
        }
        public void testarBtnAlterarSenha27()
        {
            Program.redefinirSenha = new RedefinicaoDeSenha();
            var sut = Program.redefinirSenha;
            sut.testarBtnAlterarSenha("testeSenha", "senha", "senha");
            string senha = BancoDeDados.lerBDstring("senha", RegistrationTable.getTables(0), "usuario", "'testeSenha'");
            if (senha.Equals("senha"))
            {
                Console.WriteLine("teste ok");
                BancoDeDados.updateBD(RegistrationTable.getTables(0), "senha", "'testeSenha'", "usuario", "'testeSenha'");
            }
            else MessageBox.Show($"Falha no teste 1 testarBtnAlterarSenha27 = {senha}");
            sut.testarBtnAlterarSenha("testeSenha", "senha", "senha1");
            senha = BancoDeDados.lerBDstring("senha", RegistrationTable.getTables(0), "usuario", "'testeSenha'");
            if (senha.Equals("testeSenha"))
            {
                Console.WriteLine("teste ok");
            }
            else MessageBox.Show($"Falha no teste 2 testarBtnAlterarSenha27 = {senha}");
        }
    }
}
