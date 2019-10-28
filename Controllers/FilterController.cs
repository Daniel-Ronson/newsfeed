using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using News.Models;

namespace News.Controllers
{
    public class FilterController : Controller
    {
        private static List<String> genres = new List<String>();

        public IActionResult addGenre(string genre)
        {
            genres.Add(genre);

            return getFilteredArticles(HttpContext.Session.GetInt32("websiteId").Value);
        }

        public IActionResult removeGenre(string genre)
        {
            genres.Remove(genre);

            return getFilteredArticles(HttpContext.Session.GetInt32("websiteId").Value);
        }

        public IActionResult getFilteredArticles(int websiteId)
        {
            HttpContext.Session.SetInt32("websiteId", websiteId);

            ArticleContext context = HttpContext.RequestServices.GetService(typeof(News.Models.ArticleContext)) as ArticleContext;
            var articles = context.ListArticles(websiteId);
            
            if (genres.Count() != 0)
            {
                articles = articles.Where(a => genres.Except(a.Genres).Count() < genres.Count()).ToList();
            }

            ViewData["articles"] = articles;

            return PartialView("_Articles");
        }
        
        public IActionResult getFilteredGenres(int websiteId)
        {
            HttpContext.Session.SetInt32("websiteId", websiteId);

            GenreContext genreContext = HttpContext.RequestServices.GetService(typeof(News.Models.GenreContext)) as GenreContext;

            ViewData["genres"] = genreContext.ListGenres(websiteId);

            return PartialView("_Genres");
        }
    }
}