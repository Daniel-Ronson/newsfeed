using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.Models;

namespace News.Controllers
{
    public class WebsiteController : Controller
    {

        public IActionResult getWebsiteArticles()
        {
            ArticleContext articleContext = HttpContext.RequestServices.GetService(typeof(News.Models.ArticleContext)) as ArticleContext;

            var articles = articleContext.ListArticles(1);


            ViewData["articles"] = articles;
            return Content("hi");
           // return PartialView("_Articles");
        }
    }
}
