using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using News.Models;

namespace News.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(int websiteid=3)
        {

            ArticleContext articleContext = HttpContext.RequestServices.GetService(typeof(News.Models.ArticleContext)) as ArticleContext;
            ViewData["articles"] = articleContext.ListArticles(websiteid);

            GenreContext genreContext = HttpContext.RequestServices.GetService(typeof(News.Models.GenreContext)) as GenreContext;
            ViewData["genres"] = genreContext.ListGenres(websiteid);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
