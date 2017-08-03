using System;
using System.Net;
using System.Net.Mail;

namespace LF.System.Shared.Utils
{
    public static class Helpers
    {
        public static string Capitalize(string texto)
        {
            // Se existem  0 ou 1 caracteres, apenas retorna a string
            if (texto == null) return texto;
            if (texto.Length < 2) return texto.ToUpper();
            // Divide a string em palavras
            string[] palavras = texto.Split(
                new char[] { },
                StringSplitOptions.RemoveEmptyEntries);
            // Combina as palavras 
            string resultado = "";
            foreach (string palavra in palavras)
            {
                resultado +=
                    palavra.Substring(0, 1).ToUpper() +
                    palavra.Substring(1) + " ";
            }
            return resultado;
        }

        public static void GerarEmail(string emailDest, string titulo, string mensagem)
        {
            SmtpClient smtp = new SmtpClient();

            smtp.Host = "smtp-mail.outlook.com";
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("prog.antoniocarlos@outlook.com", "isabella*15");

            MailMessage email = new MailMessage();

            email.From = new MailAddress("prog.antoniocarlos@outlook.com");
            email.To.Add(new MailAddress(emailDest));
            email.CC.Add(new MailAddress("prog.antoniocarlos@outlook.com"));
            email.Subject = titulo;
            email.IsBodyHtml = true;
            email.Body = mensagem;

            smtp.Send(email);
        }

    }
}
