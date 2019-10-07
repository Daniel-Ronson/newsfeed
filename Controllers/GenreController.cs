using News.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace News.Controllers
{
    public class GenresController : Controller
    {
        public IActionResult Index()
        {
            Genre MD = new Genre();

            
            GenreContext context = HttpContext.RequestServices.GetService(typeof(News.Models.GenreContext)) as GenreContext;
            return View(MD, context.ListGenre());
        }

        [HttpPost]
        public IActionResult Index(GenreContext model)
        {

            GenreContext context = HttpContext.RequestServices.GetService(typeof(News.Models.GenreContext)) as GenreContext;
            return View(context.ListGenres());
        }
    }
}