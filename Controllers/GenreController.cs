using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using News.Models;

namespace News.Controllers
{
    public class GenreController : Controller
    {
        public IActionResult Index()
        {

            GenreContext context = HttpContext.RequestServices.GetService(typeof(News.Models.GenreContext)) as GenreContext;
            return View(context.ListGenres());
        }
    }
}