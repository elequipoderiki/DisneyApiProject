using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DisneyApi.AppCode.Characters
{
    public class CharacterDTO 
    {        
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string Image { get; set; }
        
        public int Age { get; set; }        
        
        public decimal Weight { get; set; }
        
        public string History { get; set; }
                
        public List<int> MovieIds {get; set;} = new List<int>();
    }
}