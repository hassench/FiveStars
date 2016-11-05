using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class ServiceEmail
    {
        static public void sendEmail(string to,string subject,string body)
        {

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("fivestarssagemcom@gmail.com");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("fivestarssagemcom", "FiveSagemStarsCom");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

        static public void sendHTMLEmail(string to, string subject, string body)
        {

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            Console.WriteLine("STMP Client Created");
            mail.From = new MailAddress("fivestarssagemcom@gmail.com");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("fivestarssagemcom", "FiveSagemStarsCom");
            SmtpServer.EnableSsl = true;
            Console.WriteLine("NetworkCredential created ");
            SmtpServer.Send(mail);
            Console.WriteLine("Mail sent");
        }

        static async public Task sendEmailAsync(string to, string subject, string body)
        {

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("fivestarssagemcom@gmail.com");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("fivestarssagemcom", "FiveSagemStarsCom");
            SmtpServer.EnableSsl = true;

            await SmtpServer.SendMailAsync(mail);
        }

        static async public Task sendHTMLEmailAsync(string to, string subject, string body)
        {

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            Console.WriteLine("STMP Client Created");
            mail.From = new MailAddress("fivestarssagemcom@gmail.com");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("fivestarssagemcom", "FiveSagemStarsCom");
            SmtpServer.EnableSsl = true;
            Console.WriteLine("NetworkCredential created ");
            await SmtpServer.SendMailAsync(mail);
            
            Console.WriteLine("Mail sent");
        }

        static public void sendBulkEmail(List<string> tolist, string subject, string body)
        {

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("fivestarssagemcom@gmail.com");
            foreach (var to in tolist)
            {
                mail.To.Add(to);
            }        
            mail.Subject = subject;
            mail.Body = body;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("fivestarssagemcom", "FiveSagemStarsCom");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
    }
}
