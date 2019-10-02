using News.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Mvc;

namespace News.Controllers
{
    public class ArticlesController : Controller
    {

            public IActionResult Index()
            {
            ArticleContext context = HttpContext.RequestServices.GetService(typeof(News.Models.ArticleContext)) as ArticleContext;

                return View(context.ListArticles());
            }


        
    }
}