using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ponto.DataBase;

namespace Ponto.Interface
{
    public partial class Cadastro : Form
    {
        private string usuario, senha, senha1;

        public Cadastro()
        {
            InitializeComponent();
        }
        ////////////////////////////////////////////////////////////
        public void testarBtnCadastrar(string usuario, string senha, string senha1)
        {
            txtUsuario.Text = usuario;
            txtSenha.Text = senha;
            txtSenha1.Text = senha1;
            button1_Click(null,null);
        }
        public void setUsuarioSenha(string usuario, string senha)
        {
            this.usuario = usuario;
            this.senha = senha;
        }
        public void setSenha1(string senha1)
        {
            this.senha1 = senha1;
        }
        ////////////////////////////////////////////////////////////
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            usuario = txtUsuario.Text;
            senha = txtSenha.Text;
            senha1 = txtSenha1.Text;
            if(TratarDados.usuarioNotNull(usuario))
            {
                if(TratarDados.senhaNotNull(senha))
                {
                    if (TratarDados.senhaEqualsConfirmSenha(senha, senha1))
                    {
                        tentarCadastrarUsuario();
                    }
                    else TratarDados.MensagemDeErro(RegistrationTable.menssagemDeErro[2]);
                }
                else TratarDados.MensagemDeErro(RegistrationTable.menssagemDeErro[1]);
            }
            else TratarDados.MensagemDeErro(RegistrationTable.menssagemDeErro[0]);
        }
        public void tentarCadastrarUsuario()
        {
            if (!isCadastrado()) cadastrarUsuarioProxPasso();
            else MessageBox.Show("Usuario já cadastrado");
        }
        public bool isCadastrado()
        {
            string colunaUsuario = RegistrationTable.getColumnTableUserData(0);
            string condicao = BancoDeDados.lerBDstring(colunaUsuario, RegistrationTable.getTables(0), colunaUsuario, $"'{usuario}'");
            return condicao.Equals(usuario);
        }
        public void cadastrarUsuarioProxPasso()
        {
            //não finalizar a inscrição ainda para não cadastrar usuario sem atividade inicial
            if(continuarCadastro())
            {
                RunApplication.criarAtividade(senha,usuario);
                this.Close();
            }
        }
        public bool continuarCadastro()
        {
            DialogResult resultado = MessageBox.Show($"Confirmar cadastro com usuario {usuario.Trim(' ')}", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return resultado == DialogResult.Yes;
        }
        public void finalizarInscricao(string senha, string usuario)
        {
            inserirUsuarioNoBancoDeDadosGeral(senha.Trim(' '), usuario.Trim(' '));
            //CriacaoDeBancoDeDados.criarBancoDeDados(usuario.Trim(' ')); comando de criação de banco de dados com mysql 
            BancoDeDados.setConexao(usuario.Trim(' '));
            criarTabelaAtividades();
            criarTabelaBancoDeHoras();

        }
        public void inserirUsuarioNoBancoDeDadosGeral(string senha,string usuario)
        {
            string coluna1 = RegistrationTable.getColumnTableUserData(0);
            string coluna2 = RegistrationTable.getColumnTableUserData(1);
            CriacaoDeBancoDeDados.inserirDados(RegistrationTable.getTables(0), $"{coluna1},{coluna2}", $"'{usuario}','{senha}'");
        }
        public void criarTabelaAtividades()
        {
            string[] atividade = { DefaultTableData.getColumnTablesAtividade(0), DefaultTableData.getColumnTablesAtividade(1) };
            string[] tipo = { "varchar(255)", "double" };
            string[] valor = { "null", "null" };
            CriacaoDeBancoDeDados.criarTabela(DefaultTableData.getTables(0), atividade, tipo, valor);
        }
        public void criarTabelaBancoDeHoras()
        {
            string[] atividade1 = { DefaultTableData.getColumnTablesBancoDeHoras(0), DefaultTableData.getColumnTablesBancoDeHoras(1) };
            string[] tipo1 = { "varchar(255)","datetime" };
            string[] valor1 = { "null","null" };
            CriacaoDeBancoDeDados.criarTabela(DefaultTableData.getTables(1), atividade1, tipo1, valor1);
            string descricaoPrimeiraLinha = $"'{DefaultTableData.getColumnValueBancoDeHoras(0)}'" ;
            string descricaoSegundaLinha = $"'{DefaultTableData.getColumnValueBancoDeHoras(1)}'";
            CriacaoDeBancoDeDados.inserirDados(DefaultTableData.getTables(1), $"{DefaultTableData.getColumnTablesBancoDeHoras(0)},{DefaultTableData.getColumnTablesBancoDeHoras(1)}", $"{descricaoPrimeiraLinha},'2000-01-01'");
            CriacaoDeBancoDeDados.inserirDados(DefaultTableData.getTables(1), $"{DefaultTableData.getColumnTablesBancoDeHoras(0)},{DefaultTableData.getColumnTablesBancoDeHoras(1)}", $"{descricaoSegundaLinha},'2000-01-01'");
        }
    }
}
