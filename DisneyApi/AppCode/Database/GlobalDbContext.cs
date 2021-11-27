using System.Collections.Generic;
using System.Linq;
using DisneyApi.AppCode.Database;
using DisneyApi.AppCode.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DisneyApi.AppCode.Db
{
    public class GlobalDbContext : DbContext, IDbContext
    {
        public GlobalDbContext(DbContextOptions<GlobalDbContext> options) : base(options)        
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Playing> Playings {get; set;}
        public DbSet<Movie> Movies {get; set;}
        public DbSet<Genre> Genres {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entidad => 
              {
                entidad.HasKey(e => e.UserId);
                entidad.Property(e => e.email)
                  .IsRequired()
                  .HasMaxLength(200);
                entidad.Property(e => e.password)
                  .IsRequired()
                  .HasMaxLength(200);                
              }
            );

            modelBuilder.Entity<Movie>(entidad => 
            {
                entidad.HasKey(e => e.MovieId);
                entidad.Property(e => e.Title)
                  .IsRequired()
                  .HasMaxLength(200);
                entidad.Property(e => e.Image)
                  .HasMaxLength(200);
                entidad.Property(m => m.CreateDate)
                  .IsRequired();
                entidad.HasIndex(m => m.Title);
            });

            modelBuilder.Entity<Character>(entidad =>
            {
                entidad.HasKey(e => e.CharacterId);
                entidad.Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(50); 
                entidad.Property(e => e.Image)
                  .HasMaxLength(200);
                entidad.HasIndex(c => c.Name);
            });

            modelBuilder.Entity<Playing>()
              .HasKey(p => new {p.CharacterId, p.MovieId});              

            modelBuilder.Entity<Genre>(entidad =>
            {
              entidad.HasKey(e => e.GenreId);
              entidad.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50); 
              entidad.Property(e => e.Image)
                .HasMaxLength(200);
            });

            modelBuilder.Entity<Genre>()
              .HasMany<Movie>(g => g.Movies)
              .WithOne(s => s.Genre)
              .HasForeignKey(s => s.GenreId)
              .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Seed();
        }

        public DbSet<Genre> ActualGenres()
        {
            return this.Set<Genre>();
        }
        public DbSet<Playing> ActualPlayings()
        {
            return this.Playings;
        }
        public DbSet<Character> ActualCharacters()
        {
            return this.Set<Character>();
        }
        public IQueryable<User> RegisteredUsers()
        {
            return  this.Set<User>();
        }
        public IQueryable<Movie> ActualMovies()
        {
            return  this.Set<Movie>();
        }

    }

}

