using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKRebirth2
{
    public class ValidadorCPF
    { 
        private static readonly int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        private static readonly int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        public static bool Validar(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            string auxCPF = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(auxCPF[i].ToString()) * multiplicador1[i];
            }

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            auxCPF = auxCPF + digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(auxCPF[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
    }
}
