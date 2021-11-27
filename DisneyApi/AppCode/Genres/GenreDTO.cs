using System.ComponentModel.DataAnnotations;

namespace DisneyApi.AppCode.Genres
{
    public class GenreDTO
    {
        [Required]
        public string Name { get; set; }
        
        public string Image { get; set; }
        
        
    }
}