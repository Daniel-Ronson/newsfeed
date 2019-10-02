using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Models
{
    public class Genre
    {
        private GenreContext context;

        public int genreID { get; set; }
        public string genre { get; set; }
    }
}
