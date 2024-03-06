using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ponto.Interface
{
    public partial class CriarAtividade : Form
    {
        private string mensagemDeErro = "Digite somente numeros no tempo de atividade", usuario,senha;
        private bool veioDaPaginaInicial;
        public CriarAtividade(string senha, string usuario)
        {
            this.usuario = usuario;
            this.senha = senha;
            InitializeComponent();
            veioDaPaginaInicial = true;
        }
        public CriarAtividade()
        {
            InitializeComponent();
            veioDaPaginaInicial = false;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////
        public void testarBtn(string tempoAtividade, string nomeAtividade)
        {
            txtTempo.Text = tempoAtividade;
            txtNome.Text = nomeAtividade;
            button1_Click(null,null);
        }
        //////////////////////////////////////////////////////////////////////////////////////////////
        private void button1_Click(object sender, EventArgs e)
        {
            string tempoDaAtividade = txtTempo.Text;
            string nomeDaAtividade = txtNome.Text;
            if (!double.TryParse(tempoDaAtividade, out var tempo))
            {
                MessageBox.Show(mensagemDeErro);
                return;
            }
            if (confirmarContinuar())
            {
                Atividades atividade = new Atividades(nomeDaAtividade, tempo);
                RunApplication.atualiza_data(atividade, veioDaPaginaInicial, usuario, senha);
                this.Close();
            }
        }
        public bool confirmarContinuar()
        {
            DialogResult resultado = MessageBox.Show($"Deseja cadastrar a atividade {txtNome.Text}?","",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            return resultado == DialogResult.Yes;
        }
    }
}
