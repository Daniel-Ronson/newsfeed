using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.Models;

namespace News.Controllers
{
    public class FormController : Controller
    {
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Index(LoginRegisterForm model)
        {
            return model.LoginPassword != null ? HandleLogin(model) : HandleRegister(model);
        }

        private IActionResult HandleLogin(LoginRegisterForm model)
        {
            CookiesController UserCookie = new CookiesController();
            LoginContext context = HttpContext.RequestServices.GetService(typeof(News.Models.LoginContext)) as LoginContext;
            int UserId =  context.CheckCredentials(model.LoginUserEmail, model.LoginPassword);

            if (UserId != 0)
            {
                string SessionId = HttpContext.Request.Cookies["session_id"];
                this.SetCookie();
                string error = context.AddSession(UserId, SessionId);
                return Content($"User Does exist : " + UserId + "   " + error +  HttpContext.Request.Cookies["session_id"]);
            }
            else
                return Content($"user does not exist");

        }

        private IActionResult HandleRegister(LoginRegisterForm model)
        {
            if (model.RegisterPassword == model.RegisterPasswordCheck)
            {
                string hashed = Hashing(model.RegisterPassword);
                LoginContext context = HttpContext.RequestServices.GetService(typeof(News.Models.LoginContext)) as LoginContext;
                string result = context.AddUser(model.RegisterEmail, hashed, model.RegisterUsername, model.RegisterPasswordCheck);
                return Content($"{result}");

            }

            return Content($"RegisterForm!");

        }
         public string Hashing(string password)
        {
            string hashed;
            MD5 md5Hash = MD5.Create();
            hashed = GetMd5Hash(md5Hash, password);
            return hashed;
            
            }
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        public void SetCookie()
        {
            string SessionId;
            if (!HttpContext.Request.Cookies.ContainsKey("session_id"))
            {
                SessionId = Guid.NewGuid().ToString();

                CookieOptions cookieOptions = new CookieOptions
                {
                    Expires = new DateTimeOffset(DateTime.Now.AddDays(1))
                };
                HttpContext.Response.Cookies.Append("date_of_creation", DateTime.Now.ToString(), cookieOptions); //set cookie
                HttpContext.Response.Cookies.Append("session_id", SessionId, cookieOptions); //set cookie
            }

            else
            {
                DateTime firstRequest = DateTime.Parse(HttpContext.Request.Cookies["date_of_creation"]); //update cookie
                //todo: update datetime in session table
            }

        }

    }
}