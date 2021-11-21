using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyApi.AppCode.Genres
{
    public class GenreDTO
    {
        [Required]
        public string Name { get; set; }
        
        public string Image { get; set; }
        
        
    }
}