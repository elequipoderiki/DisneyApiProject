using System;
using System.Collections.Generic;
using System.Linq;
using DisneyApi.AppCode.Common;
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

        public IEnumerable<CharacterPrincipalFeatures> GetCharacters(string name, int age, int movieId)
        {
            if(movieId >= 0)
            {
                return GetCharactersByMovie(movieId);
            } 
            if(name != null)
            {
                return GetCharactersByName(name);
            } 
            if(age >= 0)
            {
                return GetCharactersByAge(age);
            }
            return GetAllCharacters();
        }

        private IEnumerable<CharacterPrincipalFeatures> GetAllCharacters()
        {
            Func<Character, bool> predicate = c => true;
            Func<Character, CharacterPrincipalFeatures> selector = c => 
                    new CharacterPrincipalFeatures{
                        Name = c.Name,
                        Image = c.Image
                    };
            return CharacterFilter<CharacterPrincipalFeatures>(predicate, selector);
        }

        private IEnumerable<CharacterPrincipalFeatures> GetCharactersByAge(int age)
        {
            Func<Character, bool> predicate = c => c.Age == age;
            Func<Character, CharacterFullFeatures> selector = character => 
                GetFullFeatures(character);
            return CharacterFilter<CharacterFullFeatures>(predicate, selector);
        }

        private IEnumerable<CharacterPrincipalFeatures> GetCharactersByName(string name)
        {
            Func<Character, bool> predicate = c => c.Name == name;
            Func<Character, CharacterFullFeatures> selector = character => 
                GetFullFeatures(character);
            return CharacterFilter<CharacterFullFeatures>(predicate, selector);
        }

        private IEnumerable<CharacterPrincipalFeatures> GetCharactersByMovie(int movieId)
        {
            return _context.ActualPlayings()
                .Where(p => p.MovieId == movieId).ToList()
                .Join(_context.ActualCharacters(),
                    p => p.CharacterId,
                    c => c.CharacterId,
                    (p, c) => GetFullFeatures(c))
                .ToList();
        }

        private IEnumerable<T> CharacterFilter<T>(Func<Character, bool> predicate, Func<Character, T> selector)
        {
            return (IEnumerable<T>) _context.ActualCharacters().ToList()
                .Where(predicate)
                .Select(selector)
                .ToList();
        }

        public CharacterPrincipalFeatures GetCharacterById(int id)
        {
            var character = _context.ActualCharacters()
                .Where(c => c.CharacterId == id).FirstOrDefault();
            
            Validate.ValidateNotNull(character, string.Format("Character with id {0} not found", id));

            return GetFullFeatures(character);
        }

        private CharacterFullFeatures GetFullFeatures(Character character)
        {
 
            Validate.ValidateNotNull(character, string.Format("Character with id {0} not found", character.CharacterId));

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