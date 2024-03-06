using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Ponto
{
    internal class TratarDados
    {
        public static string doubleBD(double d)
        {
            string retorno = "";
            if (!configuracao())
            {
                retorno += d.ToString("N", CultureInfo.GetCultureInfo("en-US"));
            }
            else
            {
                retorno += d.ToString();
            }
            return retorno;
        }
        public static bool configuracao()
        {
            return CultureInfo.CurrentCulture.Name.Equals("en-US");
        }
        public static string dataToBD(DateTime data)
        {
            return data.ToString("yyyy-MM-dd");
        }
        public static bool usuarioNotNull(string usuario) 
        { 
            return (usuario != null && usuario.Trim(' ') != ""); 
        }
        public static bool senhaNotNull(string senha) 
        { 
            return (senha != null && senha.Trim(' ') != ""); 
        }
        public static bool senhaEqualsConfirmSenha(string senha, string senha1) 
        { 
            return senha.Equals(senha1);
        }
        public static void MensagemDeErro(string mensagem)
        {
            MessageBox.Show(mensagem, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static string RemoveCharacters(string str, string charsToRemove)
        {
            foreach (char c in charsToRemove)
            {
                str = str.Replace(c.ToString(), "");
            }

            return str;
        }
    }
}
