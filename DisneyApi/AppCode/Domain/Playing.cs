
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