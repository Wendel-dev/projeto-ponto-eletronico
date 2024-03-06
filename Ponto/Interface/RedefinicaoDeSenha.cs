using Ponto.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ponto.Interface
{
    public partial class RedefinicaoDeSenha : Form
    {
        CriacaoDeBancoDeDados cb = new CriacaoDeBancoDeDados();
        private string usuario, senha, senha1;
        public RedefinicaoDeSenha()
        {
            InitializeComponent();
        }
        ////////////////////////////////////////////////////////////////////////////////////////
        public void testarAlterarSenha(string usuario, string senha)
        {
            this.usuario = usuario;
            this.senha = senha;
            alterarSenha();
        }
        public void testarConcluirAlteracao(string usuario, string senha)
        {
            this.usuario = usuario;
            this.senha = senha;
            concluirAlteracao();
        }
        public void testarTentarRedefinirSenha(string usuario, string senha)
        {
            this.usuario = usuario;
            this.senha = senha;
            tentarRedefinirSenha();
        }
        public void testarBtnAlterarSenha(string usuario, string senha, string senha1)
        {
            txtUsuario.Text = usuario;
            txtSenha.Text = senha;
            txtSenha1.Text = senha1;
            btnCadastro_Click(null,null);
        }
        ////////////////////////////////////////////////////////////////////////////////////////
        private void btnCadastro_Click(object sender, EventArgs e)
        {
            usuario = txtUsuario.Text;
            senha = txtSenha.Text;
            senha1 = txtSenha1.Text;
            if (TratarDados.usuarioNotNull(usuario))
            {
                if (TratarDados.senhaNotNull(senha))
                {
                    if (TratarDados.senhaEqualsConfirmSenha(senha, senha1))
                    {
                        tentarRedefinirSenha();
                    }
                    else MessageBox.Show(RegistrationTable.menssagemDeErro[2]);
                }
                else MessageBox.Show(RegistrationTable.menssagemDeErro[1]);
            }
            else MessageBox.Show(RegistrationTable.menssagemDeErro[0]);
        }
        public void tentarRedefinirSenha()
        {
            if (isCadastrado())
            {
                concluirAlteracao();
            }
            else
            {
                MessageBox.Show("Usuario não cadastrado");
            }
        }
        public bool isCadastrado()
        {
            string colunaUsuario = RegistrationTable.getColumnTableUserData(0);
            string condicao = BancoDeDados.lerBDstring(colunaUsuario, RegistrationTable.getTables(0), colunaUsuario, $"'{usuario}'");
            return condicao.Equals(usuario);
        }
        public void concluirAlteracao()
        {
            if (confirmarAlteracao())
            {
                alterarSenha();
                MessageBox.Show("Senha alterada com sucesso");
                this.Close();
            }
        }
        public bool confirmarAlteracao()
        {
            DialogResult resultado = MessageBox.Show($"Confirmar alteracao de senha?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return resultado == DialogResult.Yes;
        }
        public void alterarSenha()
        {
            string colunaUsuario = RegistrationTable.getColumnTableUserData(0);
            string colunaSenha = RegistrationTable.getColumnTableUserData(1);
            BancoDeDados.updateBD(RegistrationTable.getTables(0), colunaSenha, $"'{senha}'", colunaUsuario,$"'{usuario}'");
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
