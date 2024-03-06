using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponto.DataBase
{
    internal class AtualizarValores
    {
        public static void updateDateDataBase(DateTime updateDate, bool trabalhoTotal)
        {
            string tabelaBanco = DefaultTableData.getTables(1);

            string colunaTabelaBanco = DefaultTableData.getColumnTablesBancoDeHoras(0);
            string colunaTabelaBancoData = DefaultTableData.getColumnTablesBancoDeHoras(1);

            string dateUpdate = TratarDados.dataToBD(updateDate);

            BancoDeDados.updateBD(tabelaBanco, colunaTabelaBancoData, $"'{dateUpdate}'", colunaTabelaBanco, $"'{DefaultTableData.getColumnValueBancoDeHoras(0)}'");
            if(trabalhoTotal)BancoDeDados.updateBD(tabelaBanco, colunaTabelaBancoData, $"'{dateUpdate}'", colunaTabelaBanco, $"'{DefaultTableData.getColumnValueBancoDeHoras(1)}'");

        }
        public static void updateTimeToWork(int dias, string atividade)
        {
            string tabelaAtividade = DefaultTableData.getTables(0);
            string tabelaBanco = DefaultTableData.getTables(1);

            string colunaTabelaAtividadeativ = DefaultTableData.getColumnTablesAtividade(0);
            string colunaTabelaAtividadetemp = DefaultTableData.getColumnTablesAtividade(1);
            string colunaTabelaBanco = DefaultTableData.getColumnTablesBancoDeHoras(0);

            string descricaoPrimeiraLinhaBanco = DefaultTableData.getColumnValueBancoDeHoras(0);

            double tempoDeAtividade = BancoDeDados.lerBDdouble(colunaTabelaAtividadetemp, tabelaAtividade, colunaTabelaAtividadeativ, $"'{atividade}'");
            double bancoDeHorasAtual = BancoDeDados.lerBDdouble(atividade, tabelaBanco, colunaTabelaBanco, $"'{descricaoPrimeiraLinhaBanco}'");
            double horasAtualizadas = bancoDeHorasAtual + dias * tempoDeAtividade;
            string horasConvertidas = TratarDados.doubleBD(horasAtualizadas);

            BancoDeDados.updateBD(tabelaBanco, atividade, TratarDados.RemoveCharacters(horasConvertidas, ","), colunaTabelaBanco, $"'{descricaoPrimeiraLinhaBanco}'");
        }
        public static void updateTotalTimeWorked(double tempo, string atividade)
        {
            string tabelaBanco = DefaultTableData.getTables(1);

            string colunaTabelaBanco = DefaultTableData.getColumnTablesBancoDeHoras(0);
            string descricaoSegundaLinhaBanco = DefaultTableData.getColumnValueBancoDeHoras(1);
            string timeConverted = TratarDados.doubleBD(tempo);

            BancoDeDados.updateBD(tabelaBanco, atividade, TratarDados.RemoveCharacters(timeConverted, ","), colunaTabelaBanco, $"'{descricaoSegundaLinhaBanco}'");
        }
    }
}
