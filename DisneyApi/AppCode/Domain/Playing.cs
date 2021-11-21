using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyApi.AppCode.Domain
{
    public class Playing
    {
        public int CharacterId { get; set; }
        public Character Character {get; set;}
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}