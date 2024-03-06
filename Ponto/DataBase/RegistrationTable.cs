using System;
using System.Collections;
using System.Windows.Forms;

namespace Ponto.DataBase
{
    public class RegistrationTable
    {
        private static string[] tables = { "dadosusuarios" };
        private static string[] tableUserData = { "usuario", "senha" };
        public static string[] menssagemDeErro { get; private set; }
        public RegistrationTable() 
        {
            menssagemDeErro = new string[]
            {
                "Usuario digitado deve ser não nulo ",
                "Senha digitada deve ser não nula ",
                "Senha digitada deve ser iagual a confirmação de senha"
            };
        }
        public static string getTables(int i)
        {
            if (i >= tables.Length) MessageBox.Show("A tabela inserida não está cadastrada", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return tables[i];
        }
        public static string getColumnTableUserData(int i)
        {
            if (i >= tableUserData.Length) MessageBox.Show("A tabela inserida não está cadastrada", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return tableUserData[i];
        }
    }
}
