using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.Models;

namespace News.Controllers
{
    public class FilterController : Controller
    {
        private static List<String> genres = new List<String>();

        public IActionResult addGenre(string genre)
        {
            genres.Add(genre);

            return getFilteredArticles();
        }

        public IActionResult removeGenre(string genre)
        {
            genres.Remove(genre);

            return getFilteredArticles();
        }

        public IActionResult getFilteredArticles()
        {
            ArticleContext context = HttpContext.RequestServices.GetService(typeof(News.Models.ArticleContext)) as ArticleContext;
            var articles = context.ListArticles();
            
            if (genres.Count() != 0)
            {
                articles = articles.Where(a => genres.Except(a.Genres).Count() < genres.Count()).ToList();
            }

            ViewData["articles"] = articles;

            return PartialView("_Articles");
        }
        
    }
}