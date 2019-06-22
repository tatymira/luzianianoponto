using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Sig.Infra
{
    public class EmailApp
    {
        public static void RegistrarSenha(string email, IList<KeyValuePair<string, string>> parametros)
        {

            string path = "E:\\LuzianiaNoPonto\\SIG\\Sig.Infra\\Email\\";
            string[] paths = { path, "RegistrarSenha.html" };
            StreamReader sr = new StreamReader(Path.Combine(paths));
            string content = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            foreach (var item in parametros)
            {
                content = content.Replace(item.Key, item.Value);
            }


            MailMessage objEmail = new MailMessage();
            objEmail.From = new MailAddress("luzianianoponto@gmail.com", "Luziânia no Ponto", System.Text.Encoding.UTF8);
            objEmail.To.Add(email);
            objEmail.Subject = "Registre sua senha";
            objEmail.SubjectEncoding = System.Text.Encoding.UTF8;
            objEmail.Body = content;
            objEmail.BodyEncoding = System.Text.Encoding.UTF8;
            objEmail.IsBodyHtml = true;
            objEmail.Priority = MailPriority.Normal;
            
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("luzianianoponto@gmail.com", "@sigluzianianoponto");
            client.EnableSsl = true;

            client.Send(objEmail);

        }
    }
}
