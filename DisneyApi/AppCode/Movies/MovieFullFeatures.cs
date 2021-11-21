using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyApi.AppCode.Movies
{
    public class MovieFullFeatures : MoviePrincipalFeatures
    {
        public int Id { get; set;}
                
        public char Rating { get; set; }
                
        public int? GenreId { get; set; } 

        public List<int> CharacterIds {get; set;} = new List<int>();
 
    }
}