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
    public class CookiesController : Controller
    {
        /*
        public void Set()
        {

           if (!HttpContext.Request.Cookies.ContainsKey("session_id")) 
           {
                string SessionId = Guid.NewGuid().ToString();
                CookieOptions cookieOptions = new CookieOptions
                {
                    Expires = new DateTimeOffset(DateTime.Now.AddDays(1))
                };
                HttpContext.Response.Cookies.Append("date_of_creation", DateTime.Now.ToString(),cookieOptions); //set cookie
                HttpContext.Response.Cookies.Append("session_id", SessionId, cookieOptions); //set cookie
            }
           
            else
            {
                DateTime firstRequest = DateTime.Parse(HttpContext.Request.Cookies["date_of_creation"]);
                string sessionid = HttpContext.Request.Cookies["session_id"];
            }
            
        }
        */
    }
}
