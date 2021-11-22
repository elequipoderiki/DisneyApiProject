using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DisneyApi.AppCode.Common;
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
            if(genre >= 0)
            {
                return GetMoviesByGenre(genre);
            } 
            if(name != null)
            {
                return GetMoviesByTitle(name);
            } 
            if(order > 0)
            {
                return GetMoviesByOrder(order);
            }
            return GetAllMovies();
        }

        private IEnumerable<MoviePrincipalFeatures> GetAllMovies()
        {
            Func<Movie, bool> predicate = m => true;
            Func<Movie, MoviePrincipalFeatures> selector = m => 
                    new MoviePrincipalFeatures{
                        Title = m.Title,
                        Image = m.Image,
                        CreateDate = m.CreateDate
                    };
            return MovieFilter<MoviePrincipalFeatures>(predicate, selector);
        }

        private IEnumerable<MoviePrincipalFeatures> GetMoviesByOrder(int order)
        {
            Expression<Func<Movie, DateTime>> keySelector = m => m.CreateDate;
            IQueryable<Movie> res = ApplyOrderByKey(order, keySelector);
            return res.ToList()
                .Select(movie => GetFullFeatures(movie))
                .ToList();
        }

        private IQueryable<Movie> ApplyOrderByKey(int order, Expression<Func<Movie, DateTime>> keySelector)
        {
            var isAscOrd = order == (int) OrderDirection.ASC ? true : false;
            var res = _context.ActualMovies();
            if(isAscOrd)
            {
                return res.OrderBy(keySelector);
            }
            else
            {
                return res.OrderByDescending(keySelector);
            }
        }

        private IEnumerable<MoviePrincipalFeatures> GetMoviesByTitle(string title)
        {
            Func<Movie, bool> predicate = m => m.Title == title;
            Func<Movie, MovieFullFeatures> selector = movie => 
                GetFullFeatures(movie);
            return MovieFilter<MovieFullFeatures>(predicate, selector);
        }

        private IEnumerable<MoviePrincipalFeatures> GetMoviesByGenre(int genreId)
        {
            Func<Movie, bool> predicate = m => m.GenreId == genreId;
            Func<Movie, MovieFullFeatures> selector = movie => 
                GetFullFeatures(movie);
            return MovieFilter<MovieFullFeatures>(predicate, selector);
        }

        private IEnumerable<T> MovieFilter<T>(Func<Movie, bool> predicate, Func<Movie, T> selector)
        {
            return (IEnumerable<T>) _context.ActualMovies().ToList()
                .Where(predicate)
                .Select(selector)
                .ToList();
        }

        private MovieFullFeatures GetFullFeatures(Movie movie)
        {
            MovieFullFeatures result = new MovieFullFeatures
            {
                Id = movie.MovieId,
                Rating = movie.Rating,
                GenreId = movie.GenreId == 0 ? null : (Nullable<int>) movie.GenreId,
                CreateDate = movie.CreateDate,
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