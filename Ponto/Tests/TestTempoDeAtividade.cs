using Ponto.DataBase;
using Ponto.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ponto.Tests
{
    internal class TestTempoDeAtividade
    {
        public TestTempoDeAtividade()
        {
            testarCalcularTempoDeAtividade18();
            testarDiasUteisTrabalho19();
            testarGetTempoDiarioDeAtividade21();
            testarAtualizarTempoDeAtividades22();
            testarBtnAtualizarAtividade23();
        }
        public void testarCalcularTempoDeAtividade18()
        {
            Program.cadastrarInicio = new atualizar_data(new Atividades("", 1), true, "", "");
            var sut = Program.cadastrarInicio;
            BancoDeDados.setConexao("testtempodeatividade");
            DefaultTableData.getActivityList().Add("atividade1");
            DefaultTableData.getActivityList().Add("atividade2");
            sut.testecalculartempoatividade();
            double teste1 = sut.calculartempoDeAtividade("atividade1");
            if (teste1 == 100) Console.WriteLine("teste ok");
            else MessageBox.Show($"Falha no teste 1 testarCalcularTempoDeAtividade18 = {teste1}");
            double teste2 = sut.calculartempoDeAtividade("atividade1", 2023);
            if (teste2 == 100) Console.WriteLine("teste ok");
            else MessageBox.Show($"Falha no teste 2 testarCalcularTempoDeAtividade18 = {teste2}");
            double teste3 = sut.calculartempoDeAtividade("atividade1", 2024);
            if (teste3 == 30) Console.WriteLine("teste ok");
            else MessageBox.Show($"Falha no teste 3 testarCalcularTempoDeAtividade18 = {teste3}");
            BancoDeDados.setConexao(BancoDeDados.bancoInicial);
            sut.Close();
        }
        public void testarDiasUteisTrabalho19()
        {
            var sut = new atualizar_data(new Atividades("testtempodeatividade", 1), true, "", "");
            DateTime data1 = new DateTime(2024, 1, 1);
            DateTime data2 = new DateTime(2024, 1, 3);
            sut.testediasuteistrabalho(data1, data2);
            BancoDeDados.setConexao("testtempodeatividade");
            var testeDiasUteis = sut.diasUteisTrabalho("testtempodeatividade");
            if (testeDiasUteis == DataStructureTests.diasUteis - 1) Console.WriteLine("teste ok");
            else MessageBox.Show($"Falha no teste 1 testarDiasUteisTrabalho19 = {testeDiasUteis}");
            sut = new atualizar_data(new Atividades("testtempodeatividade", 1), true, "", "");
            sut.testediasuteistrabalho(data1, data2);
            var testeDiasUteis2 = sut.diasUteisTrabalho("atividade2");
            if (testeDiasUteis2 == DataStructureTests.diasUteis) Console.WriteLine("teste ok");
            else MessageBox.Show($"Falha no teste 2 testarDiasUteisTrabalho19 = {testeDiasUteis2}");
            BancoDeDados.setConexao(BancoDeDados.bancoInicial);
            sut.Close();
        }
        public void testarGetTempoDiarioDeAtividade21()
        {
            Program.cadastrarInicio = new atualizar_data(new Atividades("", 1), true, "", "");
            var sut = Program.cadastrarInicio;
            BancoDeDados.setConexao("testtempodeatividade");
            var teste1 = sut.getTempoDiarioDeAtividade("atividade1");
            if (teste1 == 7) Console.WriteLine("teste ok");
            else MessageBox.Show($"Falha no teste 1 testarGetTempoDiarioDeAtividade21 = {teste1}");
            var teste2 = sut.getTempoDiarioDeAtividade("atividade2");
            if (teste2 == 3) Console.WriteLine("teste ok");
            else MessageBox.Show($"Falha no teste 2 testarGetTempoDiarioDeAtividade21 = {teste2}");
        }
        public void testarAtualizarTempoDeAtividades22()
        {
            //MessageBox.Show(BancoDeDados.bancoInicial);
            DefaultTableData.testeLimparAtividade();
            var sut = new atualizar_data(new Atividades("testtempodeatividade", 1), true, "w", "w");
            sut.setatualizarTempoDeAtividades(new DateTime(2024, 2, 1));
            BancoDeDados.setConexao("testtempodeatividade");
            BancoDeDados.updateBD("testtempodeatividade", PaginaInicial.mes[1], "0", null, null);
            BancoDeDados.updateBD(DefaultTableData.getTables(1), "testtempodeatividade", "0", null, null);
            BancoDeDados.updateBD(DefaultTableData.getTables(1), DefaultTableData.getColumnTablesBancoDeHoras(1), "'2024-01-01'", null, null);
            sut.atualizarTempoDeAtividades();
            var teste1 = BancoDeDados.lerBDdata(0);
            if (teste1.ToString("dd/MM/yyyy").Equals(DateTime.Today.ToString("dd/MM/yyyy"))) Console.WriteLine("teste ok");
            else MessageBox.Show($"Falha no teste 1 testarAtualizarTempoDeAtividades22 = {teste1}");
            var teste2 = BancoDeDados.lerBDdouble("testtempodeatividade", DefaultTableData.getTables(1), DefaultTableData.getColumnTablesBancoDeHoras(0), $"'{DefaultTableData.getColumnValueBancoDeHoras(0)}'");
            if (teste2 == DataStructureTests.diasUteis - 20) Console.WriteLine("teste ok");
            else MessageBox.Show($"Falha no teste 2 testarAtualizarTempoDeAtividades22 = {teste2}");
            BancoDeDados.updateBD("testtempodeatividade", PaginaInicial.mes[1], "0", null, null);
            BancoDeDados.updateBD(DefaultTableData.getTables(1), "atividade1", "100", null, null);
            BancoDeDados.updateBD(DefaultTableData.getTables(1), "atividade2", "0", null, null);
            BancoDeDados.updateBD(DefaultTableData.getTables(1), "testtempodeatividade", "0", null, null);
            BancoDeDados.updateBD(DefaultTableData.getTables(1), DefaultTableData.getColumnTablesBancoDeHoras(1), "'2024-01-01'", null, null);
            BancoDeDados.setConexao(BancoDeDados.bancoInicial);
            sut.Close();
        }
        public void testarBtnAtualizarAtividade23()
        {
            DefaultTableData.testeLimparAtividade();
            Program.login = new Login();
            Program.cadastrarInicio = new atualizar_data(new Atividades("testtempodeatividade", 1), true, "teste1", "teste1");
            var sut = Program.cadastrarInicio;
            sut.testarBtn(new DateTime(2024, 2, 1));
            var teste1 = BancoDeDados.lerBDdata(0);
            if (teste1.ToString("dd/MM/yyyy").Equals(DateTime.Today.ToString("dd/MM/yyyy")))
            { 
                Program.paginaInicial.Close();
                Program.paginaInicial=null;
                Console.WriteLine("teste ok");
            }
            else MessageBox.Show($"Falha no teste 1 testarBtnAtualizarAtividade23 = {teste1}");
            var teste2 = BancoDeDados.lerBDdouble("testtempodeatividade", DefaultTableData.getTables(1), DefaultTableData.getColumnTablesBancoDeHoras(0), $"'{DefaultTableData.getColumnValueBancoDeHoras(0)}'");
            if (teste2 == DataStructureTests.diasUteis - 20)
            {
                Console.WriteLine("teste ok");
            }
            else MessageBox.Show($"Falha no teste 2 testarBtnAtualizarAtividade23 = {teste2}");
            BancoDeDados.deletarTabela("testtempodeatividade");
            BancoDeDados.deletarTabela("atividades");
            BancoDeDados.deletarTabela("bancodehoras");
            BancoDeDados.setConexao(BancoDeDados.bancoInicial);
            BancoDeDados.deletarLinhaDaTabela(RegistrationTable.getTables(0), new string[] { RegistrationTable.getColumnTableUserData(0) }, new string[] { "'teste1'" });
            Program.login = null;
            Program.cadastrarInicio = null;
            sut.Close();
        }
    }
}
