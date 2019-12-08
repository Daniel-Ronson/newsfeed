using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace News.DAL
{
    public class ForgotPassword
    {
     
        public void SendEmail()
        {
            SmtpClient smtpClient = new SmtpClient("smtp.mail.yahoo.com", 465);

            smtpClient.Credentials = new System.Net.NetworkCredential("ozzyisthebest99@yahoo.com", "shawyan9");
            smtpClient.UseDefaultCredentials = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();

            //Setting From , To and CC
            mail.From = new MailAddress("ozzyisthebest99@yahoo.com");
            mail.To.Add(new MailAddress("csebra9@gmail.com"));
            mail.Subject = "ASP.NET e-mail test";
            mail.Body = "Hello world,\n\nThis is an ASP.NET test e-mail!";

            smtpClient.Send(mail);
        }




    }
}
