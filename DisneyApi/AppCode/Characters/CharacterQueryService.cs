using System.Collections.Generic;
using System.Linq;
using DisneyApi.AppCode.Db;
using DisneyApi.AppCode.Domain;

namespace DisneyApi.AppCode.Characters
{
    public interface ICharacterQueryService
    {
        IEnumerable<CharacterPrincipalFeatures> GetCharacters(string name, int age, int movieId);
        CharacterPrincipalFeatures GetCharacterById(int id);
    }

    public class CharacterQueryService : ICharacterQueryService
    {
        private readonly IDbContext _context;

        public CharacterQueryService(IDbContext context)
        {
            _context = context;
        }

        public CharacterPrincipalFeatures GetCharacterById(int id)
        {
            var character = _context.ActualCharacters()
                .Where(c => c.CharacterId == id).FirstOrDefault();

            if(character == null)           
                return null;

            return GetFullFeatures(character);
        }

        public IEnumerable<CharacterPrincipalFeatures> GetCharacters(string name, int age, int movieId)
        {
            if(movieId < 0 && name == null && age < 0)
                return GetAllCharacters();

            IQueryable<Character> query = _context.ActualCharacters();
            if(name != null)
            {
                query = GetCharactersByName(query, name);
            } 
            if(age >= 0)
            {
                query = GetCharactersByAge(query, age);
            }
            if(movieId >= 0)
            {
                query = GetCharactersByMovie(query, movieId);
            } 
            return query.ToList()
                .Select(character => GetFullFeatures(character))
                .ToList();
        }

        private IEnumerable<CharacterPrincipalFeatures> GetAllCharacters()
        {
            return  _context.ActualCharacters()
                .Select(c => new CharacterPrincipalFeatures
                    {
                        Name = c.Name,
                        Image = c.Image
                    })
                .ToList();
        }

        private IQueryable<Character> GetCharactersByAge(IQueryable<Character> query, int age)
        {
            return query.Where(c => c.Age == age);
        }

        private IQueryable<Character> GetCharactersByName(IQueryable<Character> query, string name)
        {
            return query.Where(c => c.Name == name);
        }

        private IQueryable<Character> GetCharactersByMovie(IQueryable<Character> query, int movieId)
        {
            return _context.ActualPlayings()
                .Where(p => p.MovieId == movieId)
                .Join(query,
                    p => p.CharacterId,
                    c => c.CharacterId,
                    (p, c) => c);
        }

        private CharacterFullFeatures GetFullFeatures(Character character)
        {
             CharacterFullFeatures result = new CharacterFullFeatures
            {
                Id = character.CharacterId,
                Name = character.Name,
                Age = character.Age,
                Weight = character.Weight,
                History = character.History,
                Image = character.Image,
            };

            List<int> relatedIds = _context.ActualPlayings()
                .Where(p => p.CharacterId == character.CharacterId)
                .Join(_context.ActualMovies(),
                    p => p.MovieId,
                    m => m.MovieId,
                    (p, m) => m.MovieId)
                .ToList();

            result.MovieIds = relatedIds;
            return result;
        }
    }
}