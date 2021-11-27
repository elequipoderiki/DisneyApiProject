using System;
using DisneyApi.AppCode.Domain;
using Microsoft.EntityFrameworkCore;

namespace DisneyApi.AppCode.Database
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    MovieId = 5,
                    Image = null,
                    Title = "Pluto",
                    Rating = '4',
                    GenreId = 2,
                    CreateDate = new DateTime(1940,03,04)
                },
                new Movie
                {
                    MovieId = 6,
                    Image = null,
                    Title = "El rey león",
                    Rating = '4',
                    GenreId = 2,
                    CreateDate = new DateTime(1994, 02, 24)
                },
                new Movie
                {
                    MovieId = 12,
                    Image = null,
                    Title = "Pinocho",
                    Rating = '4',
                    GenreId = null,
                    CreateDate = new DateTime(1950, 02, 10)
                }

            );

            modelBuilder.Entity<Character>().HasData(
                new Character
                {
                    CharacterId = 6,
                    Name = "Pluto",
                    Age = 14,
                    Weight = 20.00M,
                    History = null,
                    Image = null
                },
                new Character
                {
                    CharacterId = 7,
                    Name = "Donald",
                    Age = 24,
                    Weight = 3.00M,
                    History = null,
                    Image = null
                },
                new Character
                {
                    CharacterId = 10,
                    Name = "Mickey",
                    Age = 24,
                    Weight = 30.00M,
                    History = null,
                    Image = null
                },
                new Character
                {
                    CharacterId = 12,
                    Name = "Dumbo",
                    Age = 20,
                    Weight = 30.00M,
                    History = null,
                    Image = null
                },
                new Character
                {
                    CharacterId = 15,
                    Name = "Tio rico",
                    Age = 98,
                    Weight = 30.00M,
                    History = null,
                    Image = null
                },
                new Character
                {
                    CharacterId = 16,
                    Name = "Bruno",
                    Age = 24,
                    Weight = 30.00M,
                    History = null,
                    Image = null
                }
            );

            modelBuilder.Entity<Playing>().HasData(
                    new Playing
                    {
                        CharacterId = 7,
                        MovieId = 5
                    },
                    new Playing
                    {
                        CharacterId = 10,
                        MovieId = 5
                    },
                    new Playing
                    {
                        CharacterId = 16,
                        MovieId = 5
                    },
                    new Playing
                    {
                        CharacterId = 16,
                        MovieId = 6
                    }
            );

            modelBuilder.Entity<Genre>().HasData(
                new Genre
                {
                    GenreId = 1,
                    Name = "Western",
                    Image = null
                },
                new Genre
                {
                    GenreId = 2,
                    Name = "Cartoon",
                    Image = null
                },
                new Genre
                {
                    GenreId = 4,
                    Name = "Terror",
                    Image = null
                },
                new Genre
                {
                    GenreId = 5,
                    Name = "Comedy",
                    Image = null
                },
                new Genre
                {
                    GenreId = 10,
                    Name = "Adventures",
                    Image = null
                },
                new Genre
                {
                    GenreId = 11,
                    Name = "Biography",
                    Image = null
                },
                new Genre
                {
                    GenreId = 12,
                    Name = "Future",
                    Image = null
                }           
            );
        }
    }
}