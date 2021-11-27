using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisneyApi.AppCode.Domain
{
    public class Character
    {
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        
        [Column(TypeName = "decimal(8,2)")]
        public decimal Weight { get; set; }
        public string History { get; set; }
        public string Image { get; set; }
        public List<Playing> Playings {get; set;}
    }
}