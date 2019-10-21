﻿using System;
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
        public IActionResult Index(int websiteid=1)
        {
<<<<<<< Updated upstream
            ArticleContext context = HttpContext.RequestServices.GetService(typeof(News.Models.ArticleContext)) as ArticleContext;
            List<Article> articleList = new List<Article>();
            articleList = context.ListArticles();
            ViewData["articles"] = context.ListArticles();
=======
            ArticleContext articleContext = HttpContext.RequestServices.GetService(typeof(News.Models.ArticleContext)) as ArticleContext;
            var articleList = articleContext.ListArticles(websiteid);
            var genreListforWebsite = articleList.Item2;
            ViewData["articles"] = articleList.Item1;

            GenreContext genreContext = HttpContext.RequestServices.GetService(typeof(News.Models.GenreContext)) as GenreContext;
            ViewData["genres"] = genreContext.ListGenres(genreListforWebsite);
>>>>>>> Stashed changes
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
