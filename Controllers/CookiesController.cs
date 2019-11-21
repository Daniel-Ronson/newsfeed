﻿using System;
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
        public IActionResult Index()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("session_id"))
            {
                CookieOptions cookieOptions = new CookieOptions
                {
                    Expires = new DateTimeOffset(DateTime.Now.AddDays(2))
                };
                HttpContext.Response.Cookies.Append("date_of_creation", DateTime.Now.ToString(),cookieOptions); //set cookie
                HttpContext.Response.Cookies.Append("session_id", "12345", cookieOptions); //set cookie
                return Content("Welcome, new visitor!" + HttpContext.Request.Cookies["session_id"]);
            }
            else
            {
                DateTime firstRequest = DateTime.Parse(HttpContext.Request.Cookies["date_of_creation"]);
                string sessionid = HttpContext.Request.Cookies["session_id"];
                //cookieOptions.Expires = DateTime.Now.AddDays(2);
                return Content("Welcome back, user! You first visited us on: " + firstRequest.ToString()+ " id: " + sessionid);
            }
        }
    }
}
