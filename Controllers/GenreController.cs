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

            GenreContext context = HttpContext.RequestServices.GetService(typeof(News.Models.GenreContext)) as GenreContext;
            return View(context.ListGenres());
        }
    }
}