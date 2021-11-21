using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DisneyApi.AppCode.Domain;

namespace DisneyApi.AppCode.Movies
{
    public class MovieDTO
    {
        public int Id { get; set;}
        
        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public string Title { get; set; }
        
        public string Image { get; set; }
        
        public char Rating { get; set; }
                
        public int GenreId { get; set; } 

        public List<int> CharacterIds {get; set;} = new List<int>();
    }
}