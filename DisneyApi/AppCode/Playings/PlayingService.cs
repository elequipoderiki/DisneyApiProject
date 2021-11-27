using System.Linq;
using DisneyApi.AppCode.Db;
using DisneyApi.AppCode.Domain;

namespace DisneyApi.AppCode.Playings
{
    public interface IPlayingService
    {
        void CreatePlayingLink(int movieId, int characterId);
    }
    public class PlayingService : IPlayingService
    {
        private IDbContext _context;
        public PlayingService(IDbContext dbContext)
        {
            _context = dbContext;
        }
 
        public void CreatePlayingLink(int movieId, int characterId)
        {   
            if(NoLink(movieId, characterId))
            {   
                var playing = new Playing
                {
                    MovieId = movieId,
                    CharacterId = characterId
                };
                _context.Add(playing);
                _context.SaveChanges();
            }
        }

        private bool NoLink(int movieId, int characterId)
        {
            return _context.ActualPlayings().Where(p => (p.CharacterId == characterId && p.MovieId == movieId)).FirstOrDefault() == null;
        }
    }
}