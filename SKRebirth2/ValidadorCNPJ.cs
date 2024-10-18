using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKRebirth2
{
    public class ValidadorCNPJ
    {
        private static readonly int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        private static readonly int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        public static bool ValidarCNPJ(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return false;

            cnpj = cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
                return false;

            string auxCNPJ = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
            {
                soma += int.Parse(auxCNPJ[i].ToString()) * multiplicador1[i];
            }

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            auxCNPJ = auxCNPJ + digito;

            soma = 0;
            for (int i = 0; i < 13; i++)
            {
                soma += int.Parse(auxCNPJ[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }

    }
}
