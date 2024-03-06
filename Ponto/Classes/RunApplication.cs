using Org.BouncyCastle.Asn1.X500;
using Ponto.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ponto
{
    public class RunApplication
    {
        public static void login(bool primeiroLogin)
        {
            if(primeiroLogin)
            {
                Program.login = new Login();
                Program.login.ShowDialog();
            }
            else Program.login.Show();
        }
        public static void recuperarSenha()
        {
            Program.redefinirSenha = new RedefinicaoDeSenha();
            Program.redefinirSenha.Show();
        }
        public static void cadastrarUsuario()
        {
            Program.cadastrarUsuario = new Cadastro();
            Program.cadastrarUsuario.Show();
        }
        public static void criarAtividade(string senha, string usuario)
        {
            Program.criarAtividade = new CriarAtividade(senha, usuario);
            Program.criarAtividade.Show();
        }
        public static void criarAtividade()
        {
            Program.criarAtividade = new CriarAtividade();
            Program.criarAtividade.Show();
        }
        public static void atualiza_data(Atividades atividade, bool paginaInicial, string usuario, string senha)
        {
            Program.cadastrarInicio = new atualizar_data(atividade, paginaInicial, usuario,senha);
            Program.cadastrarInicio.Show();
        }
        public static void editActivity()
        {
            Program.editActivity = new EditActivity();
            Program.editActivity.Show();
        }
        public static void iniciarPaginaInicial()
        {
            Program.paginaInicial = new PaginaInicial();
            Program.paginaInicial.ShowDialog();
        }
        public static void fechar()
        {
            Program.login.Close();
        }
    }
}
