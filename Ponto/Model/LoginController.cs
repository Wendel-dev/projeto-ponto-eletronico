using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ponto;
using Ponto.DataBase;

namespace Ponto.Controller
{
    public class LoginController
    {
        private static string senha;
        private static string usuario;
        public static bool senhaCorreta(string usuario, string senha)
        {
            LoginController.usuario = usuario.Trim();
            LoginController.senha = senha.Trim();
            string coluna1 = RegistrationTable.getColumnTableUserData(0);
            string coluna2 = RegistrationTable.getColumnTableUserData(1);

            string senhaDoBancoDeDados = BancoDeDados.lerBDstring(coluna2, RegistrationTable.getTables(0), coluna1, $"'{usuario}'");
            if (senha.Equals(senhaDoBancoDeDados))
            {
                return true;
            }
            return false;
        }
        public static void iniciarBancoDeUsuario()
        {
            BancoDeDados.setConexao(usuario);
            RunApplication.iniciarPaginaInicial();
        }
    }
}
