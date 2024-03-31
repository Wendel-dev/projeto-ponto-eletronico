using System;
using System.Collections.Generic;

namespace Ponto.Classes
{
    public class Datas
    {
        DateTime[] feriados;
        int diasAAdicionar = 0;
        DateTime dataInicial;
        static List<DateTime> checar = new List<DateTime>();
        public List<DateTime> diasUteis = new List<DateTime>();
        public int calculateDiasUteis(DateTime dataInicial, DateTime dataFinal)
        {
            int indexDiasUteis = 0;
            while (diasUteis[indexDiasUteis] < dataInicial)
            {
                indexDiasUteis++;
            }
            int dias = 0;
            while (diasUteis[indexDiasUteis] < dataFinal)
            {
                dias++;
                indexDiasUteis++;
            }
            if (dias > 0)
            {
                while (diasUteis[indexDiasUteis] > dataFinal)
                {
                    dias--;
                    indexDiasUteis--;
                }
            }
            return dias;
        }
        public int diasDeTrabalhoAnteriores(DateTime dataDeInicioDaAtividade)
        {
            int diaTrabalho = 0;
            inicializarDiasUteis(dataDeInicioDaAtividade);
            feriados = setarFeriado(dataDeInicioDaAtividade.Year).ToArray();
            while (diasUteis[diaTrabalho] < DateTime.Now)
            {
                diaTrabalho++;

            }
            return diaTrabalho;
        }
        public void inicializarDiasUteis(DateTime dataI)
        {
            List<DateTime> list = new List<DateTime>();
            dataInicial = dataI;
            int ano = dataInicial.Year;
            while (ano <= DateTime.Now.Year)
            {
                checar.AddRange(setarFeriado(ano));
                ano++;
            }
            diasAAdicionar =0;
            while (dataInicial.AddDays(diasAAdicionar).Year <= DateTime.Now.Year)
            {
                checarFeriado();
                list.Add(dataInicial.AddDays(diasAAdicionar));
                if (dataInicial.AddDays(diasAAdicionar).DayOfWeek == DayOfWeek.Friday)
                {
                    diasAAdicionar += 2;
                }
                diasAAdicionar++;
            }
            diasUteis = list;
        }
        private void pularFeriado(int i, DateTime check)
        {
            if (checar[i].DayOfWeek == DayOfWeek.Thursday)
            {
                diasAAdicionar += 3;
            }
            else if (checar[i].DayOfWeek == DayOfWeek.Friday)
            {
                diasAAdicionar += 2;
            }
            else if (check.ToString("dd/MM") == "12/02")
            {
                diasAAdicionar++;
            }
            diasAAdicionar++;
            checar.Remove(checar[i]);
        }
        private void checarFeriado()
        {
            DateTime check = dataInicial.AddDays(diasAAdicionar);
            for (int i = 0; i < checar.ToArray().Length; i++)
            {
                string dataString = TratarDados.dataToBD(checar[i]);
                if (checar[i] > check.AddDays(1))
                {
                    break;
                }
                if (dataString == TratarDados.dataToBD(check))
                {
                    pularFeriado(i, check);
                    break;
                }
                if (dataString == TratarDados.dataToBD(check.AddDays(1)))
                {
                    if (checar[i].DayOfWeek == DayOfWeek.Monday)
                    {
                        pularFeriado(i);
                        break;
                    }
                }
            }
        }
        private void pularFeriado(int i)
        {
            diasAAdicionar += 2;
            checar.Remove(checar[i]);
        }
        public List<DateTime> setarFeriado(int ano)
        {
            List<DateTime> retorno = new List<DateTime>();
            retorno.Add(new DateTime(ano, 1, 1));
            retorno.Add(new DateTime(ano, 1, 25));
            retorno.Add(feriadosReligiosos(0, ano));
            retorno.Add(feriadosReligiosos(1, ano));
            retorno.Add(feriadosReligiosos(2, ano));
            retorno.Add(new DateTime(ano, 4, 21));
            retorno.Add(new DateTime(ano, 5, 1));
            retorno.Add(feriadosReligiosos(3, ano));
            retorno.Add(new DateTime(ano, 7, 9));
            retorno.Add(new DateTime(ano, 9, 7));
            retorno.Add(new DateTime(ano, 10, 12));
            retorno.Add(new DateTime(ano, 11, 2));
            retorno.Add(new DateTime(ano, 11, 15));
            retorno.Add(new DateTime(ano, 11, 20));
            retorno.Add(new DateTime(ano, 12, 25));
            retorno.Sort();
            return retorno;
        }
        public DateTime feriadosReligiosos(int index, int ano)
        {
            DateTime feriado = new DateTime();
            if (index == 0)
            {
                feriado = diaDePascoa(ano).AddDays(-48);
                return feriado;
            }
            else if (index == 1)
            {
                feriado = diaDePascoa(ano).AddDays(-47);
                return feriado;
            }
            else if (index == 2)
            {
                feriado = diaDePascoa(ano).AddDays(-2);
                return feriado;
            }
            else if (index == 3)
            {
                feriado = diaDePascoa(ano).AddDays(60);
                return feriado;
            }
            return feriado;
        }
        public DateTime diaDePascoa(int ANO)
        {
            int X = 24;
            int Y = 5;
            int a = ANO % 19;
            int b = ANO % 4;
            int c = ANO % 7;
            int d = (19 * a + X) % 30;
            int e = (2 * b + 4 * c + 6 * d + Y) % 7;
            int DIA;
            int MES;
            if ((d + e) > 9)
            {
                DIA = (d + e - 9);
                MES = 4;
            }
            else
            {
                DIA = (d + e + 22);
                MES = 3;
            }
            DateTime pascoa = new DateTime(ANO, MES, DIA);
            return pascoa;
        }
    }
}
