using Ponto.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ponto.Classes;

namespace Ponto.Tests
{
    internal class DataStructureTests
    {
        private static Datas data;
        public static int diasUteis ;
        public string atividades = DefaultTableData.getTables(0);
        public string banco = DefaultTableData.getTables(1);
        public string bancoColuna1 = DefaultTableData.getColumnTablesBancoDeHoras(0);
        public string bancoColuna1Valor1 = DefaultTableData.getColumnValueBancoDeHoras(0);
        public string bancoColuna1Valor2 = DefaultTableData.getColumnValueBancoDeHoras(1);
        public string bancoColuna2 = DefaultTableData.getColumnTablesBancoDeHoras(1);
        public DataStructureTests()
        {
            data = new Datas();
            DateTime data1 = new DateTime(2024, 1, 1);
            diasUteis = data.diasDeTrabalhoAnteriores(data1);
        }
        public static void MensagemDeErro(string teste, string resultado)
        {
            MessageBox.Show($"Falha no teste {teste} = {resultado}","",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }
    }
}
