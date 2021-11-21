using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DisneyApi.AppCode.Db;
using DisneyApi.AppCode.Domain;
using DisneyApi.AppCode.Playings;
using DisneyApi.AppCode.Movies;
using Microsoft.EntityFrameworkCore;
using DisneyApi.AppCode.Common;

namespace DisneyApi.AppCode.Genres
{
    public interface IGenreCommandService
    {
        Task<int> CreateGenre(GenreDTO model);
        void DeleteGenre(int genreId);
    }

    public class GenreCommandService : IGenreCommandService
    {
        private readonly IDbContext _context;
         public GenreCommandService(IDbContext context, IPlayingService playingService, IMovieCommandService movieCmdService)
        {
            _context = context;
        }

        public async Task<int> CreateGenre(GenreDTO model)
        {             
            bool nameAlreadyExists = _context.ActualGenres().Where(g => g.Name == model.Name).Count() > 0; 
            Validate.ValidateIsFalse(nameAlreadyExists, string.Format("Genre with name {0} already exists", model.Name));

            Genre genre = new Genre()
            {
                Name = model.Name,
                Image = model.Image
            };
            _context.Add(genre);
            await _context.SaveChangesAsync();
            return genre.GenreId;
        }

        public void DeleteGenre(int genreId)
        {
            Genre genre = _context.ActualGenres().Where(g => g.GenreId == genreId)
                .Include(e => e.Movies).FirstOrDefault();
      
            Validate.ValidateNotNull(genre, string.Format("Genre with ID {0} not found", genreId));
                  
            _context.Remove(genre);
            _context.SaveChanges();
        }

    }
}