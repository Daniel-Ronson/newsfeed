using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.Models;
using News.DAL;
namespace News.Controllers
{
    public class HomeController : Controller
    {
            
        public IActionResult Index()
        {
            int UserId = this.CheckCookie("session_id"); //get user id

            if (UserId !=0) //if user does not exist, UserId will be 0
            {
              //  return Content("hello" + UserId); //test case
            }
            var websiteid = 3;
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

        //Check if not null
        //Check if sesion id exists in database
        //get the user id related to the session id
        public int CheckCookie(string KeyValue)
        {
            int UserId;
            string SessionId = HttpContext.Request.Cookies[KeyValue];
            if (SessionId != null)
            {
                CookiesContext cookieContext = HttpContext.RequestServices.GetService(typeof(CookiesContext)) as CookiesContext;
                UserId = cookieContext.GetSession(SessionId);
            }
            else
            {
                UserId = 0;
            }
            return UserId;
        }

    }
}
