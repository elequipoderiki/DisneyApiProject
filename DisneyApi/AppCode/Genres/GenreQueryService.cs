using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DisneyApi.AppCode.Common;
using DisneyApi.AppCode.Db;

namespace DisneyApi.AppCode.Genres
{
    public interface IGenreQueryService
    {
        GenrePrincipalFeatures GetGenreById(int id);
        List<GenrePrincipalFeatures> GetGenres();
    } 
    public class GenreQueryService : IGenreQueryService
    {
        private readonly IDbContext _context;
        public GenreQueryService(IDbContext context)
        {
            _context = context;
        }

        public GenrePrincipalFeatures GetGenreById(int id)
        {
            var genre = _context.ActualGenres()
                .Where(g => g.GenreId == id).FirstOrDefault();
            
            Validate.ValidateNotNull(genre, string.Format("Character with id {0} not found", id));

            return new GenrePrincipalFeatures()
            {
                Name = genre.Name,
                Image = genre.Image
            };
        }

        public List<GenrePrincipalFeatures> GetGenres()
        {
            return  _context.ActualGenres()
                .Select(g => new GenrePrincipalFeatures(){
                    Name = g.Name,
                    Image = g.Image
                })
                .ToList();
        }
    }
}