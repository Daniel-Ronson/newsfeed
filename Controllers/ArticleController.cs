﻿using Microsoft.AspNetCore.Mvc;
using News.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace News.Controllers
{
    public class ArticlesController : Controller
    {

        private ArticleContext context;


        public IActionResult Index()
        {
            context = HttpContext.RequestServices.GetService(typeof(ArticleContext)) as ArticleContext;

            return View(context.ListArticles());
        }
           
        [HttpPost]
        public JsonResult getArticles(string id)
        {
            List<Article> articles = new List<Article>();
            articles = context.ListArticles();
            return Json(articles);
        }

        
    }
}