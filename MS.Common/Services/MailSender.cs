using System;
using System.Net;
using System.Net.Mail;

namespace MS.WebSite.Services
{
    public static class MailSender
    {
        public static void Send(string email,int verificationCode)
        {
            try
            {
                MailMessage mail = new MailMessage("", email);
                SmtpClient client = new SmtpClient
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = ""
                };
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("", "***");
                mail.Subject = "Your verification code from Menagement system";
                mail.IsBodyHtml = true;
                mail.Body = "Enter this code in the site: <b>" + verificationCode + "</b>";
                client.Send(mail);
            }
            catch (Exception e) { }
        }
        public static int GenerateValidationCode()
        {
            Random rand = new Random();
            return rand.Next(100000, 999999);
        }
    }
}