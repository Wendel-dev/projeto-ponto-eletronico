using Ponto.Classes;
using Ponto.DataBase;
using Ponto.Interface;
using System;
using System.Windows.Forms;

namespace Ponto
{
    internal class teste
    {
        public teste()
        {
            BancoDeDados bd = new BancoDeDados();
            BancoDeDados.setConexao("wendel");
            PaginaInicial p = new PaginaInicial();
            Datas data = new Datas();
            DateTime inicio = new DateTime(2024, 01, 01);
            DateTime final = new DateTime(2024, 03, 17, 01, 00, 00);
            var teste = data.calculateDiasUteis(inicio, final);
            if (teste == 0) MessageBox.Show("valor ok");
        }
    }
}
