using Ponto.DataBase;
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
    public partial class EditActivity : Form
    {
        public EditActivity()
        {
            InitializeComponent();
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(DefaultTableData.getactivityArray());
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox1.Text, out var tempo)) 
            { 
                editarTempo();
                Program.paginaInicial.atualizarBancoDeHorasDate(false);
                MessageBox.Show("tempo diario de atividade alterado com sucesso");
                editarNovamente();
            }
            else MessageBox.Show("Digite um valor numérico");
        }
        public void editarNovamente()
        {
            DialogResult resultado = MessageBox.Show("Deseja editar outra atividade?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.No) this.Close();
        }
        public void editarTempo()
        {
            BancoDeDados.updateBD("atividades", "tempodeatividade",textBox1.Text, "atividade", $"'{checkActivity()}'");
        }
        public string checkActivity()
        {
            return comboBox1.SelectedItem.ToString();
        }
    }
}
