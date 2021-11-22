using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DisneyApi.AppCode.Common;
using DisneyApi.AppCode.Db;
using DisneyApi.AppCode.Domain;
using DisneyApi.AppCode.Playings;
using Microsoft.EntityFrameworkCore;

namespace DisneyApi.AppCode.Movies
{
    public interface IMovieCommandService
    {
        Task<int> CreateMovie(MovieDTO model);
        Task<bool> UpdateMovieAsync(MovieDTO model);
        bool DeleteMovie(int movieId);
    }
    public class MovieCommandService : IMovieCommandService
    {
        private readonly IDbContext _context;
        private readonly IPlayingService _playingService;

        public MovieCommandService(IDbContext context, IPlayingService playingService)
        {
            _context = context;
            _playingService = playingService;
        }

        public async Task<int> CreateMovie(MovieDTO model)
        {
            Movie movie = new Movie();
            MapModelToMovie(movie, model);
            _context.Add(movie);
            await _context.SaveChangesAsync();
            LinkToCharacters(model.CharacterIds, movie.MovieId);
            return movie.MovieId;
        }

        public bool DeleteMovie(int movieId)
        {
            Movie movie = _context.ActualMovies().Where(s => s.MovieId == movieId)
                .Include(e => e.Cast).FirstOrDefault();
            if(movie == null)            
                return false;

            _context.Remove(movie);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> UpdateMovieAsync(MovieDTO model)
        {
            Movie movie = _context.ActualMovies().Where(s => s.MovieId == model.Id).FirstOrDefault();
            if(movie == null)
                return false;

            MapModelToMovie(movie, model);
            LinkToCharacters(model.CharacterIds, movie.MovieId);
            List<int> newLinks = model.CharacterIds;
            IEnumerable<Playing> exceptedLinks = _context.ActualPlayings()
                 .Where(p => !(newLinks.Contains(p.CharacterId)) && p.MovieId == movie.MovieId)
                 .ToList();
            _context.ActualPlayings().RemoveRange(exceptedLinks);
            await _context.SaveChangesAsync();
            return true;
        }
        
        private void LinkToCharacters(List<int> characterIds, int movieId)
        {
            foreach(var characterId in characterIds)
            {
                _playingService.CreatePlayingLink(movieId, characterId);
            }
        }

        private void MapModelToMovie(Movie movie, MovieDTO model)
        {
            movie.CreateDate = model.CreateDate;
            movie.Title  = model.Title;
            movie.Image = model.Image;
            movie.Rating = model.Rating;
            movie.GenreId = model.GenreId == 0 ? null : (Nullable<int>) model.GenreId;
        }

    }
}