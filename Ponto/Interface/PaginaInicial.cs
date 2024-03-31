using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Ponto.Classes;
using Ponto.DataBase;

namespace Ponto.Interface
{
    public partial class PaginaInicial : Form
    {
        public static String[] mes= {"Janeiro","Fevereiro","Março","Abril","Maio","Junho","Julho",
        "Agosto","Setembro","Outubro","Novembro","Dezembro"};
        public String ano="2022";
        static Datas aData = new Datas();
        
        public PaginaInicial()
        {
            if (aData.diasUteis.ToArray().Length == 0) aData.inicializarDiasUteis(BancoDeDados.lerBDdata(0));
            InitializeComponent();
            DefaultTableData.refreshActivityList();
            inicializarToolStrioItens();
            inicializarComponentes();
        }
        public void inicializarComponentes()
        {
            lblMes.Text = mes[DateTime.Now.Month - 1];
            comboBox1.Items.Clear();
            DefaultTableData.refreshActivityList();
            comboBox1.Items.AddRange(DefaultTableData.getActivityList().ToArray());
            comboBox1.SelectedIndex = 0;
        }
        public void inicializarToolStrioItens()
        {
            periodo.DropDownItems.Clear();
            List<ToolStripItem> list = new List<ToolStripItem>();
            var ano = BancoDeDados.lerBDintArray("ano", DefaultTableData.getActivityList()[0], null, null).ToList();
            ano.Sort();
            for (int i = 0; i < ano.ToArray().Length; i++)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.DropDownItems.AddRange(new ToolStripItem[] { novoMes("janeiro",ano[i]), 
                    novoMes("fevereiro",ano[i]), novoMes("março",ano[i]), novoMes("abril",ano[i]), novoMes("maio",ano[i]), 
                    novoMes("junho",ano[i]), novoMes("julho",ano[i]), novoMes("agosto",ano[i]), novoMes("setembro",ano[i]), 
                    novoMes("outubro",ano[i]), novoMes("novembro",ano[i]), novoMes("dezembro",ano[i])}
                );
                item.Name = "ano"+ ano[i].ToString();
                item.Size = new Size(180, 22);
                item.Text = ano[i].ToString();
                list.Add(item);
                this.ano = ano[i].ToString();
            }
            periodo.DropDownItems.AddRange(list.ToArray());
        }
        public ToolStripMenuItem novoMes(string nome, int ano)
        {
            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Name = nome+ano.ToString();
            item.Size = new Size(180, 22);
            item.Text = nome;
            item.Click += evento;
            
            return item;
        }
        public void evento(object sender, EventArgs e)
        {
            bool atualizarTrablahoTotal = false;
            atualizarBancoDeHorasDate(atualizarTrablahoTotal);
            this.ano = TratarDados.RemoveCharacters((sender as ToolStripMenuItem).Name, (sender as ToolStripMenuItem).Text);
            atualizarInterface((sender as ToolStripMenuItem).Text);
        }
        private void btnIncluirTrabalho_Click(object sender, EventArgs e)
        {
            bool tentarConversao = double.TryParse(txtHorasAtividade.Text, out var valADD);
            if (!tentarConversao)
            {
                MessageBox.Show("Digite somente numeros");
                return;
            }

            bool atualizarTrablahoTotal = true;
            atualizarBancoDeHorasDate(atualizarTrablahoTotal);
            atualizarBancoAtividade(calcularTrabalhoMensal(valADD));
            atualizarBancoDeHoras(calcularTrabalhoEmAndamento(valADD));
            atualizarBancoDeHorasAtividades(calcularTrabalhoTotal(valADD));

            txtHorasAtividade.Text = "0";
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool atualizarTrablahoTotal = false;
            atualizarBancoDeHorasDate(atualizarTrablahoTotal);
            atualizarInterface(lblMes.Text);
        }
        public void atualizarBancoAtividade(double newValue)
        {
            string atividade = comboBox1.SelectedItem.ToString();

            string colunaCondicao = "ano";
            string mes = lblMes.Text;

            string valorCondicao = ano;

            
            string converterValor = TratarDados.doubleBD(newValue);
            BancoDeDados.updateBD(atividade, mes, TratarDados.RemoveCharacters(converterValor,","), colunaCondicao, valorCondicao);
            lblHoraAtividade.Text = newValue.ToString();
        }
        public void atualizarBancoDeHoras(double newBancoValue)
        {
            string tabelaBanco = DefaultTableData.getTables(1);

            string colunaCondicao = DefaultTableData.getColumnTablesBancoDeHoras(0);
            string atividade = comboBox1.SelectedItem.ToString();

            string valorCondicao = $"'{DefaultTableData.getColumnValueBancoDeHoras(0)}'";

            
            string converterBanco = TratarDados.doubleBD(newBancoValue);

            BancoDeDados.updateBD(tabelaBanco, atividade, TratarDados.RemoveCharacters(converterBanco, ","), colunaCondicao, valorCondicao);
            lblBancoDeHorasAtividades.Text = newBancoValue.ToString();
        }
        public void atualizarBancoDeHorasAtividades(double newBancoValue)
        {
            string tabelaBanco = DefaultTableData.getTables(1);

            string colunaCondicao = DefaultTableData.getColumnTablesBancoDeHoras(0);
            string atividade = comboBox1.SelectedItem.ToString();

            string valorCondicao = $"'{DefaultTableData.getColumnValueBancoDeHoras(1)}'";

            string converterBanco = TratarDados.doubleBD(newBancoValue);

            BancoDeDados.updateBD(tabelaBanco, atividade, TratarDados.RemoveCharacters(converterBanco, ","), colunaCondicao, valorCondicao);
            lblHoraTotal.Text = newBancoValue.ToString();
        }
        public double calcularTrabalhoMensal(double valADD)
        {
            double valAtual = double.Parse(this.lblHoraAtividade.Text);
            double newValue = valAtual + valADD;
            return newValue;
        }
        public double calcularTrabalhoTotal(double valADD)
        {
            double bancoValue = double.Parse(lblHoraTotal.Text);
            double newBancoValue = bancoValue + valADD;
            return newBancoValue;
        }
        public double calcularTrabalhoEmAndamento(double valADD)
        {
            double bancoValue = double.Parse(lblBancoDeHorasAtividades.Text);
            double newBancoValue = bancoValue - valADD;
            return newBancoValue;
        }
        public void atualizarInterface(string mes)
        {
            lblMes.Text = mes;
            string atividade = comboBox1.SelectedItem.ToString().ToLower();
            lblHoraAtividade.Text = atualizarHorariosInterface(mes, atividade, "ano", ano);
            lblBancoDeHorasAtividades.Text = atualizarHorariosInterface(atividade, DefaultTableData.getTables(1), DefaultTableData.getColumnTablesBancoDeHoras(0), $"'{DefaultTableData.getColumnValueBancoDeHoras(0)}'");
            lblHoraTotal.Text = atualizarHorariosInterface(atividade, DefaultTableData.getTables(1), DefaultTableData.getColumnTablesBancoDeHoras(0), $"'{DefaultTableData.getColumnValueBancoDeHoras(1)}'");
        }
        public void atualizarBancoDeHorasDate(bool atualizarHoraToal)
        {
            DateTime dateTimeToWork = BancoDeDados.lerBDdata(0);
            DateTime data2 = DateTime.Now;
            if ((data2 - dateTimeToWork).TotalDays >= 1)
            {
                foreach(string ativ in DefaultTableData.getActivityList())
                {
                    AtualizarValores.updateTimeToWork(aData.calculateDiasUteis(dateTimeToWork, data2), ativ);
                }
                AtualizarValores.updateDateDataBase(data2);
            }
            if (atualizarHoraToal) AtualizarValores.updateDateWork(data2);
        }
        private String atualizarHorariosInterface(String coluna, String tabela, string colunaCondicao, string valorCondicao)
        {
            double valAtual = BancoDeDados.lerBDdouble(coluna,tabela, colunaCondicao, valorCondicao);
            return valAtual.ToString();
        }
        private void criarAtividadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunApplication.criarAtividade();
        }

        private void editarAtividadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunApplication.editActivity();
        }
    }
}
