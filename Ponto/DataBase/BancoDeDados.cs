using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ponto.DataBase
{

    public class BancoDeDados
    {
        public static string senha = "Senha";
        public static string usuario = "root";
        public static string bancoInicial = "dadoscadastrais";
        public static string bancoDeDados;
        public static string servidor = "localhost";
        public static string caminhoConexao { private set; get; }

        public static SQLiteConnection conexao;
        public BancoDeDados()
        {
            bancoDeDados = bancoInicial;
            string strConexao = $"Data source= {Application.StartupPath}\\dadoscadastrais.db";
            conexao = new SQLiteConnection(strConexao);
        }
        public static void setConexao(string bancoDeDados)
        {
            BancoDeDados.bancoDeDados = bancoDeDados;
            string strConexao = $"Data source= {Application.StartupPath}\\{bancoDeDados}.db";
            caminhoConexao = strConexao;
            conexao = new SQLiteConnection(strConexao);
        }
        public static SQLiteDataReader abrirConexao(string strComando)
        {
            SQLiteCommand Comando = new SQLiteCommand(strComando, conexao);
            Console.WriteLine(strComando);
            conexao.Open();
            return Comando.ExecuteReader();
        }
        public static double lerBDdouble(string coluna,string tabela,string colunaCondicao,string valorCondicao)
        {
            double retorno = 0;
            try
            {
                string strComando;
                if (colunaCondicao==null||valorCondicao==null)
                {
                    strComando = $"SELECT {coluna} FROM {tabela}";
                }
                else
                {
                    strComando = $"SELECT {coluna} FROM {tabela} WHERE {colunaCondicao}={valorCondicao}";
                }
                SQLiteDataReader reader = abrirConexao(strComando);
                while (reader.Read())
                {
                    retorno = reader.GetDouble(0);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                conexao.Close();
            }
            return retorno;
        }
        public static int[] lerBDintArray(string coluna, string tabela, string colunaCondicao, string valorCondicao)
        {
            List<int> retorno = new List<int>();
            try
            {
                string strComando;
                if (colunaCondicao == null || valorCondicao == null)
                {
                    strComando = $"SELECT {coluna} FROM {tabela}";
                }
                else
                {
                    strComando = $"SELECT {coluna} FROM {tabela} WHERE {colunaCondicao}={valorCondicao}";
                }
                SQLiteDataReader reader = abrirConexao(strComando);
                while (reader.Read())
                {
                    retorno.Add(reader.GetInt32(0));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                conexao.Close();
            }
            return retorno.ToArray();
        }
        public static string lerBDstring(String coluna, string tabela, string colunaCondicao, string valorCondicao)
        {
            string retorno = "";
            try
            {
                string strComando;
                if (colunaCondicao == null || valorCondicao == null)
                {
                    strComando = $"SELECT {coluna} FROM {tabela}";
                }
                else
                {
                    strComando = $"SELECT {coluna} FROM {tabela} WHERE {colunaCondicao}={valorCondicao}";
                }
                SQLiteDataReader reader = abrirConexao(strComando);
                while (reader.Read())
                {
                    retorno = reader.GetString(0);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                conexao.Close();
            }
            return retorno;
        }
        public static string[] lerBDstringArray(String coluna, string tabela, string colunaCondicao, string valorCondicao)
        {
            List<string> retorno =new List<string>();
            try
            {
                string strComando;
                if (colunaCondicao != null && valorCondicao != null)
                {
                    strComando = $"SELECT {coluna} FROM {tabela} WHERE {colunaCondicao}={valorCondicao}";
                }
                else
                {
                    strComando = $"SELECT {coluna} FROM {tabela}";
                }
                SQLiteDataReader reader = abrirConexao(strComando);
                while (reader.Read())
                {
                    retorno.Add(reader.GetString(0));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                conexao.Close();
            }
            return retorno.ToArray();
        }
        public static DateTime lerBDdata(int i)
        {
            DateTime retorno = new DateTime();
            try
            {
                String strComando = $"SELECT ultima_data FROM bancoDeHoras WHERE {DefaultTableData.getColumnTablesBancoDeHoras(0)}='{DefaultTableData.getColumnValueBancoDeHoras(i)}'";
                SQLiteDataReader reader = abrirConexao(strComando);
                while (reader.Read())
                {
                    retorno = reader.GetDateTime(0);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                MessageBox.Show(e.ToString());
            }
            finally
            {
                conexao.Close();
            }
            return retorno;
        }
        public static void updateBD(String tabela,string coluna,string valor,string colunaCondicao,string valorCondicao)
        {
            try
            {
                string strComando;
                if (colunaCondicao == null || valorCondicao == null)
                {
                    strComando = $"UPDATE {tabela} SET {coluna}={valor}";
                }
                else
                {
                    strComando = $"UPDATE {tabela} SET {coluna}={valor} WHERE {colunaCondicao}={valorCondicao}";
                }
                abrirConexao(strComando);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                conexao.Close();
            }
        }
        public static void deletarLinhaDaTabela(string tabela, string[] colunaCondicao, string[] valorCondicao)
        {
            string strComando;
            if (colunaCondicao == null || valorCondicao == null)
            {
                strComando = $"DELETE FROM {tabela}";
            }
            else
            {
                strComando = "";
                for (int i = 0; i < colunaCondicao.Length; i++)
                {
                    if (i == 0)
                    {
                        strComando = $"DELETE FROM {tabela} WHERE {colunaCondicao[i]}={valorCondicao[i]}";
                    }
                    else
                    {
                        strComando = $" AND {colunaCondicao[i]}={valorCondicao[i]}";
                    }
                }
            }
            abrirConexao(strComando);
            conexao.Close();
        }
        public static void deletarTabela(string tabela)
        {
            string strComando = $"DROP table {tabela}";
            abrirConexao(strComando);
            conexao.Close();
        }
        public static void deletarBancoDeDados(string bancoDeDados)
        {
            //código deletar banco de dados SQLite
            string databasePath = $"{Application.StartupPath}\\{bancoDeDados}.db";
            try
            {
                if (File.Exists(databasePath))
                {
                    File.Delete(databasePath);
                    Console.WriteLine("Banco de dados excluído com sucesso!");
                }
                else
                {
                    Console.WriteLine("Banco de dados não encontrado!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao excluir o banco de dados: " + ex.Message);
            }
            /* Código exclusão com banco de dados MySQL
            string strComando = $"DROP schema {bancoDeDados}";
            abrirConexao(strComando);
            conexao.Close();*/
        }
    }
}
