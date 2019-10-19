namespace MoviesDB.Data.Migrations
{
    using MoviesDB.Business.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MoviesDB.Data.MovieDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MoviesDB.Data.MovieDbContext context)
        {
            context.NewsCategorySet.Add(new NewsCategory() { Name = "Top News" });
            context.NewsCategorySet.Add(new NewsCategory() { Name = "Movie News" });
            context.NewsCategorySet.Add(new NewsCategory() { Name = "TV News" });
            context.NewsCategorySet.Add(new NewsCategory() { Name = "Celebrity News" });
            context.NewsCategorySet.Add(new NewsCategory() { Name = "Indie News" });

            context.CountrySet.Add(new Country() { Name = "N/A" });
            context.CountrySet.Add(new Country() { Name = "USA" });
            context.CountrySet.Add(new Country() { Name = "Soviet Union" });
            context.CountrySet.Add(new Country() { Name = "France" });
            context.CountrySet.Add(new Country() { Name = "Germany" });
            context.CountrySet.Add(new Country() { Name = "UK" });
            context.CountrySet.Add(new Country() { Name = "Italy" });
            context.CountrySet.Add(new Country() { Name = "Mexico" });
            context.CountrySet.Add(new Country() { Name = "Japan" });
            context.CountrySet.Add(new Country() { Name = "Sweden" });
            context.CountrySet.Add(new Country() { Name = "Spain" });
            context.CountrySet.Add(new Country() { Name = "Cuba" });
            context.CountrySet.Add(new Country() { Name = "Hong Kong" });
            context.CountrySet.Add(new Country() { Name = "West Germany" });
            context.CountrySet.Add(new Country() { Name = "Australia" });
            context.CountrySet.Add(new Country() { Name = "New Zealand" });
            context.CountrySet.Add(new Country() { Name = "Canada" });
            context.CountrySet.Add(new Country() { Name = "Russia" });
            context.CountrySet.Add(new Country() { Name = "Denmark" });
            context.CountrySet.Add(new Country() { Name = "China" });
            context.CountrySet.Add(new Country() { Name = "Taiwan" });
            context.CountrySet.Add(new Country() { Name = "Brazil" });
            context.SaveChanges();

            context.StateSet.Add(new State() { Name = "N/A", CountryId = 1});
            context.SaveChanges();

            context.CountySet.Add(new County() { Name = "N/A", StateId = 1 });
            context.SaveChanges();

            foreach (var user in Seeder.Users())
            {
                context.UserSet.AddOrUpdate(t => t.FirstName, user);
            }

            foreach (var genre in Seeder.Genres())
            {
                context.GenreSet.AddOrUpdate(t => t.Name, genre);
            }
            context.GenreSet.Add(new Genre() { Name = "Short", IsSubGenre = true});
            context.GenreSet.Add(new Genre() { Name = "Film-Noir", IsSubGenre = true});

            context.LanguageSet.Add(new Language() { Name = "N/A" });
            context.LanguageSet.Add(new Language() { Name = "English" });
            context.SaveChanges();

            foreach (var director in Seeder.Directors())
            {
                context.DirectorSet.AddOrUpdate(t => t.FullName, director);
            }

            foreach (var movie in Seeder.Movies())
            {
                context.MovieSet.AddOrUpdate(movie);
            }
            
            foreach (var actor in Seeder.Actors())
            {
                context.ActorSet.AddOrUpdate(t => t.FullName, actor);
            }

            context.SaveChanges();

            foreach (var rating in Seeder.Ratings())
            {
                context.RatingSet.AddOrUpdate(t => t.DateRated, rating);
            }

            context.SaveChanges();

            Seeder.SeedGenreMovieMappingTable();
        }
    }
}
