﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
//using System.Web.Mvc;

namespace News.Models
{
    public class Article
    {
        [Display(Name = "id")]
        public string ID { get; set; }

        [Display(Name = "title")]
        public string Title { get; set; }

        [Display(Name = "url")]
        public string Url { get; set; }

        [Display(Name = "date")]
        public string Date { get; set; }

        [Display(Name = "websiteName")]
        public string WebsiteId { get; set; }

        [Display(Name = "genre")]
        public string Genre { get; set; }

        [Display(Name = "description")]
        public string Description { get; set; }


    }
}