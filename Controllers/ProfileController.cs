using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.Models;


namespace News.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            UserContext userContext = HttpContext.RequestServices.GetService(typeof(News.Models.UserContext)) as UserContext;

            if (!userContext.checkConnection())
            {
                return Content("Could not establish connection to the database.");
            }
            User model = new User();
            model = userContext.GetUser();
            ViewData["user"] = userContext.GetUser();
            return View(model);
        }
    }

}
