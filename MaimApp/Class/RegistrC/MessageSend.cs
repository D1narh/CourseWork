﻿using System;
using System.Linq;
using System.Net.Mail;

namespace MaimApp.Class.RegistrC
{
    public class MessageSend
    {
        public string Mail { get; set; }

        private static Random random = new Random();

        public MessageSend(string mail) => Mail = mail;

        public static string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 5)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void SendMessage()
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
                MailAddress to = new MailAddress(Mail);
                MailMessage myMail = new System.Net.Mail.MailMessage(from, to);

                // add ReplyTo
                MailAddress replyTo = new MailAddress("maimproject@mail.ru");
                myMail.ReplyToList.Add(replyTo);

                // set subject and encoding
                myMail.Subject = "Аутентификация";
                myMail.SubjectEncoding = System.Text.Encoding.UTF8;

                // set body-message and encoding
                myMail.Body = $"<b>КОД ПОДТВЕРЖДЕНИЯ</b><br>{RandomString()}</b>";
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
