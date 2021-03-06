using System.Collections.Generic;

namespace DisneyApi.AppCode.Characters
{
    public class CharacterFullFeatures : CharacterPrincipalFeatures
    {
        public int Id { get; set; }
                
        public int Age { get; set; }        
        
        public decimal Weight { get; set; }
        
        public string History { get; set; }
                
        public List<int> MovieIds {get; set;} = new List<int>();

    }
}