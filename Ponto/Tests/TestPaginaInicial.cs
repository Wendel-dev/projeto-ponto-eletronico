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
    internal class TestPaginaInicial : DataStructureTests
    {
        public TestPaginaInicial()
        {
            testeSecalculaCorretamenteTempoDeTrabalho1();
            testeSeAtualizaHorasTrabalhadasNoMes2();
            testeSeAtualizaHorasATrabalharNoBancoDeHoras3();
            testeSeAtualizaHorasTrabalhadasTotal4();
            testeClickNoBotaoAtualizaLbl5();
            testeComboBox1_SelectedIndexChangedAlteraLbl6();
        }
        public void testeSecalculaCorretamenteTempoDeTrabalho1()
        {
            BancoDeDados.setConexao("testePaginaInicial");
            var sut = new PaginaInicial();
            if (sut.calcularTrabalhoTotal(2) == 2) Console.WriteLine("teste ok");
            else DataStructureTests.MensagemDeErro("1 testeSecalculaCorretamenteTempoDeTrabalho1", sut.calcularTrabalhoTotal(2).ToString());
            if (sut.calcularTrabalhoMensal(2) == 2) Console.WriteLine("teste ok");
            else DataStructureTests.MensagemDeErro("2 testeSecalculaCorretamenteTempoDeTrabalho1", sut.calcularTrabalhoMensal(2).ToString());
            if (sut.calcularTrabalhoEmAndamento(2) == diasUteis - 2) Console.WriteLine("teste ok");
            else DataStructureTests.MensagemDeErro("3 testeSecalculaCorretamenteTempoDeTrabalho1", sut.calcularTrabalhoEmAndamento(2).ToString());
            BancoDeDados.updateBD("testePaginaInicial", "março", "0", null, null);
            BancoDeDados.updateBD(banco, "testePaginaInicial", "0", null, null);
            BancoDeDados.updateBD("atividade1", "março", "0", null, null);
            BancoDeDados.updateBD(banco, "atividade1", "0", null, null);
            BancoDeDados.updateBD("atividade2", "março", "2", null, null);
            BancoDeDados.updateBD(banco, "atividade2", "0", this.bancoColuna1, $"'{this.bancoColuna1Valor1}'");
            BancoDeDados.updateBD(banco, "atividade2", "2", this.bancoColuna1, $"'{this.bancoColuna1Valor2}'");
            BancoDeDados.updateBD(banco, this.bancoColuna2, "'2024-01-01'", null, null);
            BancoDeDados.setConexao(BancoDeDados.bancoInicial);
        }
        public void testeSeAtualizaHorasTrabalhadasNoMes2()
        {
            BancoDeDados.setConexao("testePaginaInicial");
            var sut = new PaginaInicial();
            sut.atualizarBancoAtividade(2);
            if (sut.getTxtAtividade() == "2") Console.WriteLine("teste ok");
            else DataStructureTests.MensagemDeErro("1 testeSeAtualizaHorasTrabalhadasNoMes1", sut.getTxtAtividade());
            if (BancoDeDados.lerBDdouble("março", "testePaginaInicial", null, null) == 2) Console.WriteLine("teste ok");
            else DataStructureTests.MensagemDeErro("2 testeSeAtualizaHorasTrabalhadasNoMes1", BancoDeDados.lerBDdouble("março", "testePaginaInicial", null, null).ToString());
            BancoDeDados.updateBD("atividade1", "março", "0", null, null);
            BancoDeDados.updateBD(banco, "atividade1", "0", null, null);
            BancoDeDados.updateBD("testePaginaInicial", "março", "0", null, null);
            BancoDeDados.updateBD(banco, "testePaginaInicial", "0", null, null);
            BancoDeDados.updateBD("atividade2", "março", "2", null, null);
            BancoDeDados.updateBD(banco, "atividade2", "0", this.bancoColuna1, $"'{this.bancoColuna1Valor1}'");
            BancoDeDados.updateBD(banco, "atividade2", "2", this.bancoColuna1, $"'{this.bancoColuna1Valor2}'");
            BancoDeDados.updateBD(banco, this.bancoColuna2, "'2024-01-01'", null, null);
            BancoDeDados.setConexao(BancoDeDados.bancoInicial);
        }
        public void testeSeAtualizaHorasATrabalharNoBancoDeHoras3()
        {
            BancoDeDados.setConexao("testePaginaInicial");
            var sut = new PaginaInicial();
            sut.atualizarBancoDeHoras(2);
            if (sut.getTxtBanco() == "2") Console.WriteLine("teste ok");
            else DataStructureTests.MensagemDeErro("1 testeSeAtualizaHorasATrabalharNoBancoDeHoras2", sut.getTxtAtividade());
            if (BancoDeDados.lerBDdouble("testePaginaInicial", this.banco, this.bancoColuna1, $"'{this.bancoColuna1Valor1}'") == 2) Console.WriteLine("teste ok");
            else DataStructureTests.MensagemDeErro("2 testeSeAtualizaHorasATrabalharNoBancoDeHoras2", BancoDeDados.lerBDdouble("testePaginaInicial", this.banco, this.bancoColuna1, $"'{this.bancoColuna1Valor1}'").ToString());
            BancoDeDados.updateBD(banco, "atividade1", "0", null, null);
            BancoDeDados.updateBD("testePaginaInicial", "março", "0", null, null);
            BancoDeDados.updateBD(banco, "testePaginaInicial", "0", null, null);
            BancoDeDados.updateBD("atividade2", "março", "2", null, null);
            BancoDeDados.updateBD(banco, "atividade2", "0", this.bancoColuna1, $"'{this.bancoColuna1Valor1}'");
            BancoDeDados.updateBD(banco, "atividade2", "2", this.bancoColuna1, $"'{this.bancoColuna1Valor2}'");
            BancoDeDados.updateBD(banco, this.bancoColuna2, "'2024-01-01'", null, null);
            BancoDeDados.setConexao(BancoDeDados.bancoInicial);
        }
        public void testeSeAtualizaHorasTrabalhadasTotal4()
        {
            BancoDeDados.setConexao("testePaginaInicial");
            var sut = new PaginaInicial();
            sut.atualizarBancoDeHorasAtividades(2);
            if (sut.getTxtHoraTotal() == "2") Console.WriteLine("teste ok");
            else DataStructureTests.MensagemDeErro("1 testeSeAtualizaHorasTrabalhadasTotal3", sut.getTxtAtividade());
            if (BancoDeDados.lerBDdouble("testePaginaInicial", this.banco, this.bancoColuna1, $"'{this.bancoColuna1Valor2}'") == 2) Console.WriteLine("teste ok");
            else DataStructureTests.MensagemDeErro("2 testeSeAtualizaHorasTrabalhadasTotal3", BancoDeDados.lerBDdouble("testePaginaInicial", this.banco, null, null).ToString());
            BancoDeDados.updateBD(banco, "atividade1", "0", null, null);
            BancoDeDados.updateBD("testePaginaInicial", "março", "0", null, null);
            BancoDeDados.updateBD(banco, "testePaginaInicial", "0", null, null);
            BancoDeDados.updateBD("atividade2", "março", "2", null, null);
            BancoDeDados.updateBD(banco, "atividade2", "0", this.bancoColuna1, $"'{this.bancoColuna1Valor1}'");
            BancoDeDados.updateBD(banco, "atividade2", "2", this.bancoColuna1, $"'{this.bancoColuna1Valor2}'");
            BancoDeDados.updateBD(banco, this.bancoColuna2, "'2024-01-01'", null, null);
            BancoDeDados.setConexao(BancoDeDados.bancoInicial);
        }
        public void testeClickNoBotaoAtualizaLbl5()
        {
            BancoDeDados.setConexao("testePaginaInicial");
            var sut = new PaginaInicial();
            sut.testebtnAtualizacaoDeDadosAtividade("2");
            if (sut.getTxtAtividade() == "2") Console.WriteLine("teste ok");
            else DataStructureTests.MensagemDeErro("1 testeClickNoBotaoAtualizaLbl5", sut.getTxtAtividade());
            if (sut.getTxtBanco() == (diasUteis - 2).ToString()) Console.WriteLine("teste ok");
            else DataStructureTests.MensagemDeErro("2 testeClickNoBotaoAtualizaLbl5", sut.getTxtBanco());
            if (sut.getTxtHoraTotal() == "2") Console.WriteLine("teste ok");
            else DataStructureTests.MensagemDeErro("3 testeClickNoBotaoAtualizaLbl5", sut.getTxtHoraTotal());
            BancoDeDados.updateBD("atividade1", "março", "0", null, null);
            BancoDeDados.updateBD(banco, "atividade1", "0", null, null);
            BancoDeDados.updateBD("testePaginaInicial", "março", "0", null, null);
            BancoDeDados.updateBD(banco, "testePaginaInicial", "0", null, null);
            BancoDeDados.updateBD("atividade2", "março", "2", null, null);
            BancoDeDados.updateBD(banco, "atividade2", "0", this.bancoColuna1, $"'{this.bancoColuna1Valor1}'");
            BancoDeDados.updateBD(banco, "atividade2", "2", this.bancoColuna1, $"'{this.bancoColuna1Valor2}'");
            BancoDeDados.updateBD(banco, this.bancoColuna2, "'2024-01-01'", null, null);
            BancoDeDados.setConexao(BancoDeDados.bancoInicial);
        }
        public void testeComboBox1_SelectedIndexChangedAlteraLbl6()
        {
            BancoDeDados.setConexao("testePaginaInicial");
            var sut = new PaginaInicial();
            sut.testeComboBox1_SelectedIndexChanged();
            Console.WriteLine(sut.getComboBox1_SelectedItem());
            if (sut.getTxtAtividade() == "2") Console.WriteLine("teste ok");
            else DataStructureTests.MensagemDeErro("1 testeComboBox1_SelectedIndexChangedAlteraLbl6", sut.getTxtAtividade());
            if (sut.getTxtBanco() == (diasUteis*2).ToString()) Console.WriteLine("teste ok");
            else DataStructureTests.MensagemDeErro("2 testeComboBox1_SelectedIndexChangedAlteraLbl6", sut.getTxtBanco());
            if (sut.getTxtHoraTotal() == "2") Console.WriteLine("teste ok");
            else DataStructureTests.MensagemDeErro("3 testeComboBox1_SelectedIndexChangedAlteraLbl6", sut.getTxtHoraTotal());
            BancoDeDados.updateBD("atividade1", "março", "0", null, null);
            BancoDeDados.updateBD(banco, "atividade1", "0", null, null);
            BancoDeDados.updateBD("atividade2", "março", "2", null, null);
            BancoDeDados.updateBD(banco, "atividade2", "0", this.bancoColuna1, $"'{this.bancoColuna1Valor1}'");
            BancoDeDados.updateBD(banco, "atividade2", "2", this.bancoColuna1, $"'{this.bancoColuna1Valor2}'");
            BancoDeDados.updateBD("testePaginaInicial", "março", "0", null, null);
            BancoDeDados.updateBD(banco, "testePaginaInicial", "0", null, null);
            BancoDeDados.updateBD(banco, this.bancoColuna2, "'2024-01-01'", null, null);
            BancoDeDados.setConexao(BancoDeDados.bancoInicial);
        }
    }
}
