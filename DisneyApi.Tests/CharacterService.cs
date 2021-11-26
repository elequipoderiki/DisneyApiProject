using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DisneyApi.AppCode.Characters;

namespace DisneyApi.Tests
{
    public class FakeCharacterService : ICharacterQueryService,  ICharacterCommandService
    {
        private List<CharacterFullFeatures> _characters;
        private static int NewCharacterId = 1;

        public FakeCharacterService()
        {
            _characters = new  List<CharacterFullFeatures>();
        }

        public void SeedTwoCharactersInOrderWithNames(string name1, string name2)
        {
            _characters.Add(
                new CharacterFullFeatures()
                {
                    Id = 1,
                    Name = name1,
                    Age = 30,
                    Weight = 100,
                    History = "",
                    Image = ""
                }
            ); 
            _characters.Add(
                new CharacterFullFeatures ()
                {
                    Id = 2,
                    Name = name2,
                    Age = 30,
                    Weight = 100,
                    History = "",
                    Image = ""
                }
            );
        }

        public async Task<int> CreateCharacterAsync(CharacterDTO character)
        {
            CharacterFullFeatures newCharacter = new CharacterFullFeatures()
            {
                Age = character.Age,
                History = character.History,
                Image = character.Image,
                Name = character.Name,
                Weight = character.Weight,
                Id =  NewCharacterId
            };
            _characters.Add(newCharacter);
            NewCharacterId++;
            await Task.Delay(1000);
            return newCharacter.Id;
        }

        public bool DeleteCharacter(int characterId)
        {
            var existing = _characters.First(c => c.Id == characterId);
            var success = _characters.Remove(existing);
            return success;
        }

        public async Task<bool> UpdateCharacterAsync(CharacterDTO character)
        {
            var existing = _characters.First(c => c.Id == character.Id);
            existing.Age = character.Age;
            existing.History = character.History;
            existing.Image = character.Image;
            existing.Name = character.Name;
            existing.Weight = character.Weight;
            
            await Task.Delay(1000);
            return true;
        }

        public CharacterPrincipalFeatures GetCharacterById(int id)
        {
            return _characters.Where(c => c.Id == id).FirstOrDefault();
        }

        public IEnumerable<CharacterPrincipalFeatures> GetCharacters(string name, int age, int movieId)
        {
            var result = new List<CharacterPrincipalFeatures>();
            if(name != null || age > 0 || movieId > 0)
            {
                result.Add(_characters[0]);
                return result;
            }            

            return  _characters.Select(c => new CharacterPrincipalFeatures(){
                Name = c.Name,
                Image = c.Image
            });
        }

    }
}