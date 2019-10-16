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
        
        // GET: Filter
        public ActionResult Index()
        {
            return View();
        }

        // GET: Filter/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Filter/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Filter/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Filter/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Filter/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Filter/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Filter/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}