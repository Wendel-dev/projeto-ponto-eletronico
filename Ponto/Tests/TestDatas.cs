using Ponto.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ponto.Tests
{
    internal class TestDatas
    {
        public TestDatas()
        {
            testarDiaDePascoa1();
            testarCalculoDeFeriadosReligiosos2();
            testarInicializarDiasUteis3();
            testarCalculoDeDiasDeTrabalhoAnterior4();
            testarSeCalculaDiasUteisCorretamente5();
        }
        public void testarDiaDePascoa1()
        {
            var sut = new Datas();
            int ano = 2024;
            var diasDePascoa = sut.diaDePascoa(ano);
            if (diasDePascoa == new DateTime(ano, 3, 31)) Console.WriteLine("teste ok");
            else DataStructureTests.MensagemDeErro("1 testarDiaDePascoa", diasDePascoa.ToString());
            ano = 2023;
            diasDePascoa = sut.diaDePascoa(ano);
            if (diasDePascoa == new DateTime(ano, 4, 9)) Console.WriteLine("teste ok");
            else DataStructureTests.MensagemDeErro("2 testarDiaDePascoa", diasDePascoa.ToString());
            ano = 2025;
            diasDePascoa = sut.diaDePascoa(ano);
            if (diasDePascoa == new DateTime(ano, 4, 20)) Console.WriteLine("teste ok");
            else DataStructureTests.MensagemDeErro("3 testarDiaDePascoa", diasDePascoa.ToString());
        }
        public void testarCalculoDeFeriadosReligiosos2()
        {
            var sut = new Datas();
            DateTime data1 = new DateTime(2024, 1, 1);
            int ano = 2024;
            int index = 0;
            var diasDePascoa = sut.feriadosReligiosos(index, ano);
            if (diasDePascoa == new DateTime(ano, 2, 12)) Console.WriteLine("teste ok");
            else DataStructureTests.MensagemDeErro("1 testarCalculoDeFeriadosReligiosos3", diasDePascoa.ToString());
            index = 1;
            diasDePascoa = sut.feriadosReligiosos(index, ano);
            if (diasDePascoa == new DateTime(ano, 2, 13)) Console.WriteLine("teste ok");
            else DataStructureTests.MensagemDeErro("2 testarCalculoDeFeriadosReligiosos3", diasDePascoa.ToString());
            index = 2;
            diasDePascoa = sut.feriadosReligiosos(index, ano);
            if (diasDePascoa == new DateTime(ano, 3, 29)) Console.WriteLine("teste ok");
            else DataStructureTests.MensagemDeErro("3 testarCalculoDeFeriadosReligiosos3", diasDePascoa.ToString());
            index = 3;
            diasDePascoa = sut.feriadosReligiosos(index, ano);
            if (diasDePascoa == new DateTime(ano, 5, 30)) Console.WriteLine("teste ok");
            else DataStructureTests.MensagemDeErro("4 testarCalculoDeFeriadosReligiosos3", diasDePascoa.ToString());
        }
        public void testarInicializarDiasUteis3()
        {
            var sut = new Datas();
            DateTime data1 = new DateTime(2023, 1, 1);
            sut.inicializarDiasUteis(data1);
            var testeDiasUteis = sut.diasUteis.ToArray().Length;
            if (testeDiasUteis == 493) Console.WriteLine("teste ok");
            else DataStructureTests.MensagemDeErro("1 testarInicializarDiasUteis4", testeDiasUteis.ToString());
        }
        public void testarCalculoDeDiasDeTrabalhoAnterior4()
        {
            var sut = new Datas();
            DateTime data1 = new DateTime(2023, 12, 1);
            var testeDiasUteis = sut.diasDeTrabalhoAnteriores(data1);
            if (testeDiasUteis == DataStructureTests.diasUteis + 20) Console.WriteLine("teste ok");
            else DataStructureTests.MensagemDeErro("1 testarCalculoDeDiasDeTrabalhoAnterior5", testeDiasUteis.ToString());
        }
        public void testarSeCalculaDiasUteisCorretamente5()
        {
            var sut = new Datas();
            DateTime data1 = new DateTime(2023, 12, 1);
            DateTime data2 = new DateTime(2024, 2, 1);
            sut.inicializarDiasUteis(data1);
            var testeDiasUteis = sut.calculateDiasUteis(data1, data2);
            if (testeDiasUteis == 40) Console.WriteLine("teste ok");
            else DataStructureTests.MensagemDeErro("1 testarSeCalculaDiasUteisCorretamente1", testeDiasUteis.ToString());
        }
    }
}
