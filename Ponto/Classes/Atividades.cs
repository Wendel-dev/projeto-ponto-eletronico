using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ponto.DataBase;

namespace Ponto
{
    public class Atividades
    {
        public string nomeDaAtividade { get;private set; }
        public double tempoDiarioParaAtividade { get; private set; }

        public Atividades(string nome,double tempoAplicado)
        {
            nomeDaAtividade= nome;
            tempoDiarioParaAtividade=tempoAplicado;
        }
        public void concluirCriacaoDeAtividade()
        {
            cadastratAtividadeNaTabelaAtividades();
            CriacaoDeBancoDeDados.criarTabelaTrabalho(nomeDaAtividade);
            inicializarTabelaDeHorasExecutadas();
        }
        private void cadastratAtividadeNaTabelaAtividades()
        {
            string colunas = $"{DefaultTableData.getColumnTablesAtividade(0)},{DefaultTableData.getColumnTablesAtividade(1)}";
            string valores = $"'{nomeDaAtividade}',{tempoDiarioParaAtividade}";
            CriacaoDeBancoDeDados.inserirDados(DefaultTableData.getTables(0), colunas, valores);
        }
        private void inicializarTabelaDeHorasExecutadas()
        {
            string colunas = $"Janeiro,Fevereiro,Março,Abril,Maio,Junho,Julho,Agosto,Setembro,Outubro,Novembro,Dezembro";
            string valores = $"0,0,0,0,0,0,0,0,0,0,0,0";
            CriacaoDeBancoDeDados.inserirDados(nomeDaAtividade, colunas, valores);
            CriacaoDeBancoDeDados.incluirColuna(DefaultTableData.getTables(1), nomeDaAtividade, "double", "0");
        }
        public static int teste()
        {
            return 1;
        }
    }
}
