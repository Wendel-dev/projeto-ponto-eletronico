using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ponto.DataBase
{
    internal class DefaultTableData
    {
        private static string[] tables = { "atividades", "bancoDeHoras" };
        private static string[] tablesAtividades = { "atividade", "tempoDeAtividade" };
        private static string[] tablesBancoDeHoras = { "descricaoDeLinha", "ultima_data" };
        private static string[] bancoDeHorasColumnValue = { "Linha de registro de horas a executar", "Linha de resgistro de todas as horas realizadas em cada tarefa" };
        private static List<string> activityList = new List<string>();
        /////////////////////////////////////////////////////////////////////////
        public static void testeLimparAtividade()
        {
            activityList.Clear();
        }
        /////////////////////////////////////////////////////////////////////////
        public static string getTables(int i)
        {
            if (i >= tables.Length)
            {
                MessageBox.Show("A tabela inserida não está cadastrada","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return "";
            }
            return tables[i];
        }
        public static string[] getTablesArray()
        {
            return tables;
        }
        public static string getColumnValueBancoDeHoras(int i)
        {
            if (i >= bancoDeHorasColumnValue.Length)
            {
                MessageBox.Show("O valor selecionado não está cadastrado", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
            return bancoDeHorasColumnValue[i];
        }
        public static string getColumnTablesAtividade(int i)
        {
            if (i >= tablesAtividades.Length)
            { 
                MessageBox.Show("A cluna selecionada não está cadastrada", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
            return tablesAtividades[i];
        }
        public static string getColumnTablesBancoDeHoras(int i)
        {
            if (i >= tablesBancoDeHoras.Length)
            { 
                MessageBox.Show("A cluna inserida não está cadastrada", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
            return tablesBancoDeHoras[i];
        }
        public static void refreshActivityList()
        {
            activityList.Clear();
            activityList.AddRange(activityRegistred());
        }
        public static List<string> getActivityList()
        {
            return activityList;
        }
        public static string[] getactivityArray()
        {
            if (activityList.ToArray().Length<1)
            {
                return new string[1];
            }
            return activityList.ToArray();
        }
        private static string[] activityRegistred()
        {
            return BancoDeDados.lerBDstringArray(DefaultTableData.getColumnTablesAtividade(0), DefaultTableData.getTables(0), null, null);
        }
    }
}
