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
            bool LoginFlag =  context.CheckCredentials(model.LoginUserEmail, model.LoginPassword);

            if (LoginFlag == true)
            {
                this.Set();
                return Content($"User Does exist");
            }
            else
                return Content($"user does not exist");

            //return Content($"LoginForm!" +
            //    $"{model.LoginUserEmail}" +
            //    $"{model.LoginPassword}");
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
        public void Set()
        {
            // HttpContext context = HttpContext.Current;

            // if(HttpContext.Request.Cookies["session_id"] == null)
            if (!HttpContext.Request.Cookies.ContainsKey("session_id"))
            {
                string SessionId = Guid.NewGuid().ToString();
                CookieOptions cookieOptions = new CookieOptions
                {
                    Expires = new DateTimeOffset(DateTime.Now.AddDays(1))
                };
                HttpContext.Response.Cookies.Append("date_of_creation", DateTime.Now.ToString(), cookieOptions); //set cookie
                HttpContext.Response.Cookies.Append("session_id", SessionId, cookieOptions); //set cookie
                //return Content( "Welcome, new visitor! Session Id: " + HttpContext.Request.Cookies["session_id"]);
            }

            else
            {
                DateTime firstRequest = DateTime.Parse(HttpContext.Request.Cookies["date_of_creation"]);
                string sessionid = HttpContext.Request.Cookies["session_id"];
                //cookieOptions.Expires = DateTime.Now.AddDays(2);
                //return Content("Welcome back, user! You first visited us on: " + firstRequest.ToString()+ " Sessionid: " + HttpContext.Request.Cookies["session_id"]);
            }

        }

    }
}