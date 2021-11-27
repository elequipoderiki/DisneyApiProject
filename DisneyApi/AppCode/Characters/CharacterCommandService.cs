using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DisneyApi.AppCode.Db;
using DisneyApi.AppCode.Domain;
using DisneyApi.AppCode.Playings;
using Microsoft.EntityFrameworkCore;

namespace DisneyApi.AppCode.Characters
{
    public interface ICharacterCommandService
    {
        Task<int> CreateCharacterAsync(CharacterDTO character);
        Task<bool> UpdateCharacterAsync(CharacterDTO character);
        bool DeleteCharacter(int characterId);
    }

    public class CharacterCommandService : ICharacterCommandService
    {
        private readonly IDbContext _context;
        private readonly IPlayingService _playingService;

        public CharacterCommandService(IDbContext context, IPlayingService playingService)
        {
            _context = context;
            _playingService = playingService;
        }

        public async Task<int> CreateCharacterAsync(CharacterDTO model)
        {
            Character character = new Character();
            MapModelToCharacter(character, model);
            _context.Add(character);
            await _context.SaveChangesAsync();
            LinkToMovies(model.MovieIds, character.CharacterId);
            return character.CharacterId;
        }

        public async Task<bool> UpdateCharacterAsync(CharacterDTO model)
        {
            Character character = _context.ActualCharacters().Where(c => c.CharacterId == model.Id)
                .FirstOrDefault();
            if(character == null)   
                return false;

            MapModelToCharacter(character, model);
            List<int> newLinks = model.MovieIds;
            LinkToMovies(newLinks, character.CharacterId);
            IEnumerable<Playing> exceptedLinks = _context.ActualPlayings()
                .Where(p => !(newLinks.Contains(p.MovieId)) && p.CharacterId == character.CharacterId)
                .ToList();
            _context.ActualPlayings().RemoveRange(exceptedLinks);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool DeleteCharacter(int characterID)
        {
            Character character = _context.ActualCharacters().Where(c => c.CharacterId == characterID)
                .Include(e => e.Playings).FirstOrDefault();
            if(character == null)
                return false;

            _context.Remove(character);
            _context.SaveChanges();
            return true;
        }

        private void LinkToMovies(List<int> movieIds, int characterId)
        {
            foreach(var movieId in movieIds)
            {
                _playingService.CreatePlayingLink(movieId, characterId);
            }
        }

        private void MapModelToCharacter(Character character, CharacterDTO model)
        {            
            character.Name = model.Name;
            character.Age = model.Age;
            character.Weight = model.Weight;
            character.History = model.History;
            character.Image = model.Image;
        }

    }
}