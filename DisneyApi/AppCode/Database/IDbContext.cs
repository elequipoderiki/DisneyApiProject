using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DisneyApi.AppCode.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DisneyApi.AppCode.Db
{
    public interface IDbContext 
    {
        EntityEntry<T>  Add<T>(T theElement) where T : class;
        IQueryable<User> RegisteredUsers();
        DbSet<Genre> ActualGenres();
        DbSet<Character> ActualCharacters();
        DbSet<Playing> ActualPlayings();
        IQueryable<Movie> ActualMovies();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry<T> Remove<T>(T theElement) where T : class;
        int SaveChanges();
        EntityEntry<T> Update<T>(T entity) where T : class;
    }
}