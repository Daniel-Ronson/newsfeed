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
            
            LoginContext context = HttpContext.RequestServices.GetService(typeof(News.Models.LoginContext)) as LoginContext;
            var test = context.CheckCredentials(model.LoginUserEmail, model.LoginPassword);


            return Content($"{test}");
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
         private string Hashing(string password)
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


    }
}