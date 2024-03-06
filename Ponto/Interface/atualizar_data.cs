using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ponto.Classes;
using Ponto.DataBase;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Ponto.Interface
{
    public partial class atualizar_data : Form
    {
        private string usuario, senha,selectedAtividade;
        private bool paginaInicial;
        private Datas classeData = new Datas();
        private DateTime hoje,ultimaDataBancoDeDados, dataEscolhidaParaNovaAtividade;
        private Atividades objAtividade;
        public atualizar_data(Atividades atividade, bool paginaInicial, string usuario, string senha)
        {
            this.selectedAtividade = atividade.nomeDaAtividade;
            this.objAtividade = atividade;
            this.senha = senha;
            this.paginaInicial = paginaInicial;
            this.usuario = usuario;
            InitializeComponent();
        }
        //////////////////////////////////////////////////////////////////////////////////////
        public void testarBtn(DateTime data)
        {
            this.dateTimePicker1.Value = data;
            button1_Click(null,null);
        }
        public void testecalculartempoatividade()
        {
            hoje = DateTime.Now;
        }
        public void testediasuteistrabalho(DateTime dataBancoDeDados, DateTime dataNovaAtividade)
        {
            hoje = DateTime.Now;
            ultimaDataBancoDeDados = dataBancoDeDados;
            dataEscolhidaParaNovaAtividade = dataNovaAtividade;
        }
        public void setatualizarTempoDeAtividades(DateTime data)
        {
            this.dateTimePicker1.Value = data;
        }
        //////////////////////////////////////////////////////////////////////////////////////

        private void button1_Click(object sender, EventArgs e)
        {
            if (!confirmarCadastro()) return;
            if (paginaInicial)
            {
                new Cadastro().finalizarInscricao(senha, usuario);
                atualizarComponentes();
                RunApplication.iniciarPaginaInicial();
            }
            else
            {
                atualizarComponentes();
                Program.paginaInicial.inicializarComponentes();
                Program.login.Close();
                Program.cadastrarInicio = null;
            }
        }
        private void atualizarComponentes()
        {
            objAtividade.concluirCriacaoDeAtividade();
            atualizarTempoDeAtividades();
            this.Dispose();
        }
        public bool confirmarCadastro()
        {
            DialogResult resultado = MessageBox.Show($"Confirmar cadastro com a data selecionada?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return resultado == DialogResult.Yes;
        }
        public void atualizarTempoDeAtividades()
        {
            hoje = DateTime.Now;
            string strdata = TratarDados.dataToBD(hoje);
            dataEscolhidaParaNovaAtividade = dateTimePicker1.Value;
            ultimaDataBancoDeDados = BancoDeDados.lerBDdata(0);
            double trabalhoBdARealizar;
            DefaultTableData.refreshActivityList();
            foreach (string ativ in DefaultTableData.getActivityList())
            {
                trabalhoBdARealizar = calculartempoDeAtividade(ativ);
                double horasATrabalhar = trabalhoBdARealizar + diasUteisTrabalho(ativ) * getTempoDiarioDeAtividade(ativ);
                string horasDeTrabalhoTratadasParaString = TratarDados.doubleBD(horasATrabalhar);
                BancoDeDados.updateBD(DefaultTableData.getTables(1), ativ,TratarDados.RemoveCharacters(horasDeTrabalhoTratadasParaString,","), DefaultTableData.getColumnTablesBancoDeHoras(0), $"'{DefaultTableData.getColumnValueBancoDeHoras(0)}'" );
            }
            BancoDeDados.updateBD(DefaultTableData.getTables(1), DefaultTableData.getColumnTablesBancoDeHoras(1), $"'{strdata}'", null, null);
        }
        public double getTempoDiarioDeAtividade(string ativ)
        {
            return BancoDeDados.lerBDdouble(DefaultTableData.getColumnTablesAtividade(1), DefaultTableData.getTables(0), DefaultTableData.getColumnTablesAtividade(0), $"'{ativ}'");
        }
        public int diasUteisTrabalho(string ativ)
        {
            if (ativ.Equals(selectedAtividade))
            {
                atualizarAnoTrabalho(ativ);
               return classeData.diasDeTrabalhoAnteriores(dataEscolhidaParaNovaAtividade);
            }
            atualizarAnoTrabalho(ativ);
            return classeData.diasDeTrabalhoAnteriores(ultimaDataBancoDeDados);
        }
        private void atualizarAnoTrabalho(string ativ)
        {
            List<int> ano = BancoDeDados.lerBDintArray("ano", ativ, null, null).ToList();
            ano.Sort();
            if (ano[0] == 0)
            {
                BancoDeDados.deletarLinhaDaTabela(ativ, new string[] { "ano" }, new string[] { "0" });
            }
            int anoIteracao = ano[ano.ToArray().Length-1];
            if (anoIteracao > dataEscolhidaParaNovaAtividade.Year ||anoIteracao<1000) anoIteracao = dataEscolhidaParaNovaAtividade.Year;
            for (int i = anoIteracao; i <= DateTime.Now.Year; i++)
            {
                if(ano.Contains(i)) continue;
                CriacaoDeBancoDeDados.inserirDados(ativ, "ano", $"'{i}'");
            }
        }
        public double calculartempoDeAtividade(string atividade, int ano)
        {
            double tempoDeAtividade = 0;
            for (int j =ano; j <= hoje.Year; j++)
            {
                int mes=12;
                if (j == hoje.Year) mes = hoje.Month;
                for (int i = 0; i < mes; i++)
                {
                    tempoDeAtividade += BancoDeDados.lerBDdouble(PaginaInicial.mes[i], $"{atividade}", "ano", j.ToString());
                }
            }
            return tempoDeAtividade;
        }
        public double calculartempoDeAtividade(string atividade)
        {
            double tempoDeAtividade = BancoDeDados.lerBDdouble(atividade, DefaultTableData.getTables(1), DefaultTableData.getColumnTablesBancoDeHoras(0), $"'{DefaultTableData.getColumnValueBancoDeHoras(0)}'");
            return tempoDeAtividade;
        }
    }
}
