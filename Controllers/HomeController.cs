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
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var websiteid = 3;
            HttpContext.Session.SetInt32("websiteId", websiteid);

            ArticleContext articleContext = HttpContext.RequestServices.GetService(typeof(News.Models.ArticleContext)) as ArticleContext;

            if (!articleContext.CheckConnection())
            {
                return Content("Could not establish connection to the database.");
            }
            
            ViewData["articles"] = articleContext.GetAllArticles(websiteid).OrderByDescending(a => a.Date).ToList();

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
