using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MaimApp.Class.RegistrC
{
    public class MessageSend
    {
        public string Mail { get; set; }
        public string Login { get; set; }

        private static Random random = new Random();

        public MessageSend(string mail, string login)
        {
            Mail = mail;
            Login = login;
        }

        public static string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 5)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void SendMessage()
        {
            MailAddress FromAdress = new MailAddress("maimproject@mail.ru","Maim");
            MailAddress ToAdress = new MailAddress(Mail,Login);
            MailMessage Message = new MailMessage(FromAdress,ToAdress);

            Message.Body = $"Для продолжения авторизации введите следующий код: {Environment.NewLine}{RandomString()}";
            Message.Subject = "Подтверждение электронной почты";

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.mail.ru";
            smtpClient.Port = 465;
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(FromAdress.Address, "QPALZMg1");

            smtpClient.Send(Message);
        }

        public void SendMessage2()
        {
            try
            {

                SmtpClient mySmtpClient = new SmtpClient("smtp.mail.ru");

                mySmtpClient.UseDefaultCredentials = true;
                mySmtpClient.EnableSsl = true;

                // set smtp-client with basicAuthentication
                System.Net.NetworkCredential basicAuthenticationInfo = new
                   System.Net.NetworkCredential("maimproject@mail.ru", "HwGa8t7dmAF5QvHdnqRj");
                mySmtpClient.Credentials = basicAuthenticationInfo;

                // add from,to mailaddresses
                MailAddress from = new MailAddress("maimproject@mail.ru", "Maim");
                MailAddress to = new MailAddress(Mail, Login);
                MailMessage myMail = new System.Net.Mail.MailMessage(from, to);

                // add ReplyTo
                MailAddress replyTo = new MailAddress("maimproject@mail.ru");
                myMail.ReplyToList.Add(replyTo);

                // set subject and encoding
                myMail.Subject = "Test message";
                myMail.SubjectEncoding = System.Text.Encoding.UTF8;

                // set body-message and encoding
                myMail.Body = "<b>Test Mail</b><br>using <b>HTML</b>.";
                myMail.BodyEncoding = System.Text.Encoding.UTF8;
                // text or html
                myMail.IsBodyHtml = true;

                mySmtpClient.Send(myMail);
            }

            catch (SmtpException ex)
            {
                throw new ApplicationException
                  ("SmtpException has occured: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
