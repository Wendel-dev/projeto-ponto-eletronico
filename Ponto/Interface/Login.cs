using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ponto.Controller;
using Ponto.DataBase;

namespace Ponto.Interface
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            BancoDeDados.setConexao(BancoDeDados.bancoInicial);
            if (!TratarDados.usuarioNotNull(txtUsuario.Text)) return;
            if (LoginController.senhaCorreta(txtUsuario.Text, txtSenha.Text))
            {
                this.Hide();
                LoginController.iniciarBancoDeUsuario();
            }
            else
            {
                MessageBox.Show("Usuario ou senha não encontrados\n\rDeseja recuperar senha?");
            }
        }
        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            RunApplication.cadastrarUsuario();
        }
        private void btnRecuperarSenha_Click(object sender, EventArgs e)
        {
            RunApplication.recuperarSenha();
        }
        public void testLogin(string usuario, string senha)
        {
            txtUsuario.Text = usuario;
            txtSenha.Text = senha;
            btnLogin_Click(null, null);
        }
        public void testcadastrar()
        {
            btnCadastrar_Click(null, null);
        }
        public void testRedefinirSenha()
        {
            btnRecuperarSenha_Click(null, null);
        }
    }
}