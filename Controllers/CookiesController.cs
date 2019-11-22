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
    public class CookiesController : Controller
    {
         //HttpContext context = HttpContext.Request;

        public void Set()
        {
           // HttpContext context = HttpContext.Current;

           // if(HttpContext.Request.Cookies["session_id"] == null)
           if (!HttpContext.Request.Cookies.ContainsKey("session_id")) 
           {
                string SessionId = Guid.NewGuid().ToString();
                CookieOptions cookieOptions = new CookieOptions
                {
                    Expires = new DateTimeOffset(DateTime.Now.AddDays(1))
                };
                HttpContext.Response.Cookies.Append("date_of_creation", DateTime.Now.ToString(),cookieOptions); //set cookie
                HttpContext.Response.Cookies.Append("session_id", SessionId, cookieOptions); //set cookie
                //return Content( "Welcome, new visitor! Session Id: " + HttpContext.Request.Cookies["session_id"]);
            }
           
            else
            {
                DateTime firstRequest = DateTime.Parse(HttpContext.Request.Cookies["date_of_creation"]);
                string sessionid = HttpContext.Request.Cookies["session_id"];
                //cookieOptions.Expires = DateTime.Now.AddDays(2);
                //return Content("Welcome back, user! You first visited us on: " + firstRequest.ToString()+ " Sessionid: " + HttpContext.Request.Cookies["session_id"]);
            }
            
        }
    }
}
