using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DisneyApi.AppCode.Db;
using DisneyApi.AppCode.Domain;
using static DisneyApi.AppCode.Common.Utils;

namespace DisneyApi.AppCode.Movies
{
    public interface IMovieQueryService
    {
        IEnumerable<MoviePrincipalFeatures> GetMovies(string name, int genre, int order);
        MovieFullFeatures GetMovieById(int id);
    }
    public class MovieQueryService : IMovieQueryService
    {
        private readonly IDbContext _context;

        public MovieQueryService(IDbContext context)
        {
            _context = context;
        }

        public MovieFullFeatures GetMovieById(int id)
        {
            var movie = _context.ActualMovies()
                .Where(m => m.MovieId == id).FirstOrDefault();
            if(movie == null )
                return null;

            return GetFullFeatures(movie);
        }

        public IEnumerable<MoviePrincipalFeatures> GetMovies(string name, int genre, int order)
        {
            if(order < 0 && name == null && genre < 0)
                return GetAllMovies();

            IQueryable<Movie> query = _context.ActualMovies();
            if(genre >= 0)
            {
                query = GetMoviesByGenre(query, genre);
            } 
            if(name != null)
            {
                query = GetMoviesByTitle(query, name);
            } 
            if(order > 0)
            {
                query = GetMoviesByOrder(query, order);
            }
            return query.ToList()
                .Select(movie => GetFullFeatures(movie))
                .ToList();
        }

        private IEnumerable<MoviePrincipalFeatures> GetAllMovies()
        {
            return _context.ActualMovies()
                .Select(m => new MoviePrincipalFeatures
                    {
                        Title = m.Title,
                        Image = m.Image,
                        CreateDate = m.CreateDate
                    })
                .ToList();
        }

        private IQueryable<Movie> GetMoviesByOrder(IQueryable<Movie> query, int order)
        {
            var isAscOrd = order == (int) OrderDirection.ASC ? true : false;
            var res = _context.ActualMovies();
            if(isAscOrd)
            {
                return query.OrderBy(m => m.CreateDate);
            }
            else
            {
                return query.OrderByDescending(m => m.CreateDate);
            }            
        }

        private IQueryable<Movie> GetMoviesByTitle(IQueryable<Movie> query, string title)
        {
            return query.Where(m => m.Title == title);
        }

        private IQueryable<Movie> GetMoviesByGenre(IQueryable<Movie> query, int genreId)
        {
            return query.Where(m => m.GenreId == genreId);
        }

        private MovieFullFeatures GetFullFeatures(Movie movie)
        {
            MovieFullFeatures result = new MovieFullFeatures
            {
                Id = movie.MovieId,
                Rating = movie.Rating,
                GenreId = movie.GenreId == 0 ? null : (Nullable<int>) movie.GenreId,
                CreateDate = movie.CreateDate.Date,
                Title = movie.Title,
                Image = movie.Image
            };
            List<int> relatedCharacters = _context.ActualPlayings()
                .Where(m => m.MovieId == movie.MovieId)
                .Join(_context.ActualCharacters(),
                    p => p.CharacterId,
                    c => c.CharacterId,
                    (p, c) => c.CharacterId)
                .ToList();
            result.CharacterIds = relatedCharacters;
            return result;
        }

    }
}