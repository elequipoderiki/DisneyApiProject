using System.Collections.Generic;

namespace DisneyApi.AppCode.Domain
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public List<Movie> Movies { get; set; }
    }
}