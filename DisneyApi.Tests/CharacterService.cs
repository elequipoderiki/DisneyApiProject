using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DisneyApi.AppCode.Characters;

namespace DisneyApi.Tests
{
    public class CharacterService : ICharacterQueryService
    {
        List<CharacterFullFeatures> _characters;
        public CharacterService()
        {
            _characters = new  List<CharacterFullFeatures>() 
            {
                new CharacterFullFeatures()
                {
                    Id = 1,
                    Name = "Tribilin",
                    Age = 25,
                    Weight = 200,
                    History = "",
                    Image = ""
                },
                new CharacterFullFeatures()
                {
                    Id = 2,
                    Name = "Mickey",
                    Age = 30,
                    Weight = 100,
                    History = "",
                    Image = ""
                },
                new CharacterFullFeatures ()
                {
                    Id = 3,
                    Name = "Donald",
                    Age = 30,
                    Weight = 100,
                    History = "",
                    Image = ""
                }
            };
        }

        public CharacterPrincipalFeatures GetCharacterById(int id)
        {
            return _characters.Where(c => c.Id == id).FirstOrDefault();
        }

        public IEnumerable<CharacterPrincipalFeatures> GetCharacters(string name, int age, int movieId)
        {
            if(name != null || age > 0 || movieId > 0)
                return new List<CharacterPrincipalFeatures>() {_characters[2]};
            
            return _characters;
        }
    }
}