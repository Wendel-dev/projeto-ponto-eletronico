using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ponto.DataBase
{
    internal class CriacaoDeBancoDeDados
    {
        public static void criarBancoDeDados(string nome)
        {
            try
            {
                string strComando = $"create schema {nome} default char set utf8mb4";
                BancoDeDados.abrirConexao(strComando);
                BancoDeDados.conexao.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                BancoDeDados.conexao.Close();
            }
        }
        public static void criarTabelaTrabalho(string nome)
        {
            try
            {
                string strComando = $"create table {nome}(ano int default 0,Janeiro double default 0,";
                strComando += $"Fevereiro double default 0,Março double default 0,Abril double default 0,";
                strComando += $"Maio double default 0,Junho double default 0,Julho double default 0,";
                strComando += $"Agosto double default 0,Setembro double default 0,Outubro double default 0,";
                strComando += $"Novembro double default 0,Dezembro double default 0)";
                BancoDeDados.abrirConexao(strComando);
                BancoDeDados.conexao.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                BancoDeDados.conexao.Close();
            }
        }
        public static void criarTabela(string nome,string[] coluna,string[] tipo,string[] valor)
        {
            try
            {
                string strComando = $"create table {nome}(";
                for (int i=0;i<coluna.Length;i++)
                {
                    if(i>0) strComando+=",";
                    strComando += $"{coluna[i]} {tipo[i]} default {valor[i]}";
                }
                strComando += $")";
                BancoDeDados.abrirConexao(strComando);
                BancoDeDados.conexao.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                BancoDeDados.conexao.Close();
            }
        }
        public static void incluirColuna(string tabela, string coluna, string tipo, string valor)
        {
            try
            {
                string strComando2 = $"ALTER TABLE {tabela} ADD COLUMN {coluna} {tipo} default {valor}";
                BancoDeDados.abrirConexao(strComando2);
                BancoDeDados.conexao.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                BancoDeDados.conexao.Close();
            }
        }
        public static void inserirDados(String tabela, String colunas, String valores)
        {
            try
            {
                String strComando = $"insert into {tabela}({colunas}) values({valores})";
                BancoDeDados.abrirConexao(strComando);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                BancoDeDados.conexao.Close();
            }
        }
        public static void inserirColuna(string tabela, string coluna,string tipo)
        {
            try
            {
                String strComando = $"alter table {tabela} add column {coluna} {tipo} null first";
                BancoDeDados.abrirConexao(strComando);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                BancoDeDados.conexao.Close();
            }
        }
    }
}
