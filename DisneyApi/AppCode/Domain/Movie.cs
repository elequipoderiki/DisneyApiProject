using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyApi.AppCode.Domain
{
    public class Movie
    {
        public int MovieId { get; set; }
        public DateTime CreateDate { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public char Rating { get; set; }
        public int? GenreId { get; set; }
        public Genre Genre { get; set; }
        public List<Playing> Cast {get; set;}
    }
}