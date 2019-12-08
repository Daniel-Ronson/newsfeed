using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.Models;



namespace News.Controllers
{
    public class PasswordResetController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private string GetUsername(string email)
        {
            UserContext context = HttpContext.RequestServices.GetService(typeof(UserContext)) as UserContext;
            var userId = context.GetUserId(email);
            return context.GetUser(userId).Username;
        }

        private string GetResetKey()
        {
            var uuid = Guid.NewGuid();

            return uuid.ToString();
        }
        
        public void SendEmail(string email)
        {
            var subject = "Outright News - Forgot Password";
            string body = "<p> Hello, " + GetUsername(email) + "</p><br/>";
            body += "<p>We've received your request for a password reset with your Outright News account.</p></br>";
            string url = "http://outrightnews.azurewebsites.net/PasswordReset/CheckLink?key=" + GetResetKey();
            body += "<a html=" + url + ">Click this link to reset your password</a></br>";
            body += "<p>Thanks, </p></br><p>The Outright News team</p>";
            
            SmtpClient smtpClient = new SmtpClient("smtp.mail.yahoo.com", 465);

            smtpClient.Credentials = new System.Net.NetworkCredential("ozzyisthebest99@yahoo.com", "shawyan9");
            smtpClient.UseDefaultCredentials = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();

            mail.IsBodyHtml = true;
            //Setting From , To and CC
            mail.From = new MailAddress("ozzyisthebest99@yahoo.com");
            mail.To.Add(new MailAddress(email));
            mail.Subject = subject;
            mail.Body = body;

            smtpClient.Send(mail);
        }

    }
}
