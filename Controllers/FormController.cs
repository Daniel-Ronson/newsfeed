using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.Models;

namespace News.Controllers
{
    public class FormController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Index(LoginRegisterForm model)
        {
            if (model.LoginPassword != null)
            {
                return HandleLogin(model);
            } else
            {
                return HandleRegister(model);
            }
        }

        private IActionResult HandleLogin(LoginRegisterForm model)
        {
            
            LoginContext context = HttpContext.RequestServices.GetService(typeof(News.Models.LoginContext)) as LoginContext;
           string test =  context.CheckCredentials(model.LoginUserEmail, model.LoginPassword);


            return Content($"{test}");
            //return Content($"LoginForm!" +
            //    $"{model.LoginUserEmail}" +
            //    $"{model.LoginPassword}");
        }

        private IActionResult HandleRegister(LoginRegisterForm model)
        {
            if (model.RegisterPassword == model.RegisterPasswordCheck)
            {

                LoginContext context = HttpContext.RequestServices.GetService(typeof(News.Models.LoginContext)) as LoginContext;
                string result = context.AddUser(model.RegisterEmail, model.RegisterPassword, model.RegisterUsername, model.RegisterPasswordCheck);
                return Content($"{result}");

            }

            return Content($"RegisterForm!");

        }
    }
}