using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKRebirth2
{
    public static class ValidarEmail
    {
        public static bool IsValidEmail(string email)
        {

            try
            {
                var enderecoEmail = new System.Net.Mail.MailAddress(email);
                return enderecoEmail.Address == email;
            }
            catch
            {
                return false;
            }

        }
    }
 }
