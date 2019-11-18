using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return Content($"LoginForm!" +
                $"{model.LoginUserEmail}" +
                $"{model.LoginPassword}");
        }

        private IActionResult HandleRegister(LoginRegisterForm model)
        {
            return Content($"RegisterForm!" +
                $"{model.RegisterEmail}" +
                $"{model.RegisterUsername}" +
                $"{model.RegisterPassword}" +
                $"{model.RegisterPasswordCheck}");
        }
    }
}