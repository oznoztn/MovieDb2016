using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MoviesDB.Business.Entities;
using System.Drawing;
using System.Xml.Linq;

namespace MoviesDB.Data
{
    public static class Seeder
    {
        private const int LoremCount = 50;
        private static readonly string[] LoremText;
        static Seeder()
        {
            LoremText = File.OpenText(@"C:\seed\lorem.txt").ReadToEnd().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static List<Rating> Ratings()
        {
            var randomDateTime = new RandomDateTime();
            var list = new List<Rating>();

            string[] ratings = File.ReadAllLines(@"C:\seed\myratings.txt", Encoding.Default);
            string[] myratingDates = File.ReadAllLines(@"C:\seed\movie_rate_date.txt");

            var id = 1;
            foreach (string rating in ratings)
            {
                list.Add(new Rating()
                {
                    Id = id,
                    DateRated = ReturnProperDate(myratingDates[id-1]),
                    UserId = 1,
                    MovieId = id,
                    Rate = int.Parse(rating)
                });
                id++;
            }

            return list;
        }

        public static List<Role> Roles()
        {
            return new List<Role>()
            {
                new Role()
                {
                    Id = 1,
                    Name = "System Operator"
                },
                new Role()
                {
                    Id = 2,
                    Name = "Moderator",
                },
                new Role()
                {
                    Id = 3,
                    Name = "Premium User"
                },    
                new Role()
                {
                    Id = 4,
                    Name = "User"
                }
            };
        }

        public static List<User> Users()
        {
            return new List<User>()
            {
                new User()
                {
                    IsActivated = true,
                    FirstName = "Ozan",
                    LastName = "Oz",
                    Username = "ozn",
                    Age = 24,
                    Avatar = "",
                    Facebook = "",
                    Twitter = "",                    
                    Instagram = "",
                    Google = "",
                    CreateDate = DateTime.Now,
                    LastLoginDate = DateTime.Now,                    
                    IsSuspended = false,
                    IsPremium = true,
                    IsSystemAccount = true, 
                    Mail = "Ozan@moviesdb.com",
                    Website = "www.mymoviedb.com",
                    Signature = "My creation",
                    Location = "Somewhere", 
                    DisableEmailNotifications = true, 
                    DisablePrivateMessages = true, 
                    Roles = new Collection<Role>()
                    {
                        Roles().Single(t => t.Id == 1)
                    }
                },
                new User()
                {
                    IsActivated = true,
                    FirstName = "Eren",
                    LastName = "Er",
                    Username = "ern",
                    Age = 22,
                    Avatar = "",
                    Facebook = "",
                    Twitter = "",   
                    Instagram = "",
                    Google = "",
                    CreateDate = DateTime.Now,
                    LastLoginDate = DateTime.Now,                    
                    IsSuspended = false,
                    IsPremium = true,
                    IsSystemAccount = true, 
                    Mail = "Eren@moviesdb.com",
                    Website = "www.mymoviedb.com",
                    Signature = "My creation",
                    Location = "Somewhere", 
                    DisableEmailNotifications = true, 
                    DisablePrivateMessages = true, 
                    Roles = new Collection<Role>()
                    {
                        Roles().Single(t => t.Id == 2)
                    }  
                },
                new User()
                {
                    IsActivated = true,
                    FirstName = "Onur",
                    LastName = "On",
                    Username = "on",
                    Age = 30,
                    Avatar = "",
                    Facebook = "",
                    Twitter = "",    
                    Instagram = "",
                    Google = "",
                    CreateDate = DateTime.Now,
                    LastLoginDate = DateTime.Now,                    
                    IsSuspended = false,
                    IsPremium = true,
                    IsSystemAccount = true, 
                    Mail = "Onur@moviesdb.com",
                    Website = "www.mymoviedb.com",
                    Signature = "My creation",
                    Location = "Somewhere", 
                    DisableEmailNotifications = true, 
                    DisablePrivateMessages = true, 
                    Roles = new Collection<Role>()
                    {
                        Roles().Single(t => t.Id == 3)
                    }  
                },
                new User()
                {
                    IsActivated = true,
                    FirstName = "Experimento",
                    LastName = "Exx",
                    Username = "eXper",
                    Age = 99,
                    Avatar = "",
                    Facebook = "",
                    Twitter = "",   
                    Instagram = "",
                    Google = "",
                    CreateDate = DateTime.Now,
                    LastLoginDate = DateTime.Now,                    
                    IsSuspended = false,
                    IsPremium = true,
                    IsSystemAccount = true, 
                    Mail = "exx@moviesdb.com",
                    Website = "www.mymoviedb.com",
                    Signature = "My creation",
                    Location = "Never-land", 
                    DisableEmailNotifications = true, 
                    DisablePrivateMessages = true, 
                    Roles = new Collection<Role>()
                    {
                        Roles().Single(t => t.Id == 4)
                    }  
                }
            };
        }

        public static List<Genre> Genres()
        {
            var genreList = new List<Genre>();
            string[] genres = File.ReadAllLines(@"C:\seed\genres.txt", Encoding.Default);

            int id = 1;
            foreach (var genre in genres)
            {
                genreList.Add(new Genre()
                {
                    Id = id, 
                    Name = genre.Trim(), 
                    IsSubGenre = false
                });
                id++;
            }

            return genreList;
        }

        public static List<Actor> Actors()
        {
            var actors = new List<Actor>();

            string[] yearsListNoTextRaw = File.ReadAllLines(@"C:\seed\actors_years_notext.txt", Encoding.Default);
            string[] actorsConsts = File.ReadAllLines(@"C:\seed\actors_nmconst.txt", Encoding.Default);
            string[] actors500 = File.ReadAllLines(@"C:\seed\actors.txt", Encoding.Default);

            var firstLastNameSeperated = new List<FirstLastName>();
            foreach (var actorFullName in actors500)
            {
                var splitted = actorFullName.Split(' ');
                switch (splitted.Count())
                {
                    case 1:                        
                        break;
                    case 2:
                        firstLastNameSeperated.Add(new FirstLastName() { First = splitted[0], Last = splitted[1] });
                        break;

                    case 3:
                        firstLastNameSeperated.Add(new FirstLastName() { First = splitted[0], Last = splitted[1] + " " + splitted[2] });
                        break;

                    case 4:
                        firstLastNameSeperated.Add(new FirstLastName() { First = splitted[0], Last = splitted[1] + " " + splitted[2] + " " + splitted[3] });
                        break;
                }
            }

            var dateFinalList = new List<DateTime>();
            foreach (var year in yearsListNoTextRaw)
            {
                DateTime dateOut;
                DateTime.TryParse(year, out dateOut);
                var dateFinal = dateOut.ToShortDateString();

                dateFinalList.Add(dateOut);
            }

            var randomDateTime = new RandomDateTime();
            for (int i = 0; i < actors500.Count(); i++)
            {
                actors.Add(new Actor()
                {
                    FullName = actors500[i],
                    BirthDate = dateFinalList[i],
                    DeathDate = null,
                    Biography = null,
                    CreatedAt = randomDateTime.Next(),
                    FirstName = firstLastNameSeperated[i].First,
                    LastName = firstLastNameSeperated[i].Last,
                    ImdbLink = actorsConsts[i],
                    Gender = true,
                    CountryId = 1,
                    StateId = 1,
                    CountyId = 1
                });
            }

            return actors.ToList();
        }

        public static void SeedActorsTop1000()
        {
            var actors = XDocument.Load(@"C:\seed\New_\active.xml");

            var actorsName = actors.Elements("Top1000").Elements("Actor").Elements("Name").Select(t => t.Value).ToList();
            var actorsConst = actors.Elements("Top1000").Elements("Actor").Elements("Const").Select(t => t.Value).ToList();
            var actorsKnownFor = actors.Elements("Top1000").Elements("Actor").Elements("KnownFor").Select(t => t.Value).ToList();
            var actorsIsMale = actors.Elements("Top1000").Elements("Actor").Elements("IsMale").Select(t => t.Value).ToList();
            var actorsBirthDate = actors.Elements("Top1000").Elements("Actor").Elements("Birth").Select(t => t.Value).ToList();
            var forCount = actorsName.Count;

            var list = new List<string>();
            var firstLastNameSeperated = new List<Seeder.FirstLastName>();
            foreach (var actorFullName in actorsName)
            {
                var splitted = actorFullName.Split(' ');
                switch (splitted.Count())
                {
                    case 1:
                        list.Add(actorFullName);
                        break;

                    case 2:
                        firstLastNameSeperated.Add(new Seeder.FirstLastName() { First = splitted[0], Last = splitted[1] });
                        break;

                    case 3:
                        firstLastNameSeperated.Add(new Seeder.FirstLastName() { First = splitted[0], Last = splitted[1] + " " + splitted[2] });
                        break;

                    case 4:
                        firstLastNameSeperated.Add(new Seeder.FirstLastName() { First = splitted[0], Last = splitted[1] + " " + splitted[2] + " " + splitted[3] });
                        break;
                }
            }

            var birthdateFinal = new List<DateTime>();
            foreach (var year in actorsBirthDate)
            {
                DateTime dateOut;
                DateTime.TryParse(year, out dateOut);
                var dateFinal = dateOut.ToShortDateString();

                birthdateFinal.Add(dateOut);
            }
            
            using (var context = new MovieDbContext())
            {
                for (int i = 0; i < forCount; i++)
                {
                    var actorKnownForMovieId = actorsKnownFor[i];
                    Movie isThereAmovie = context.MovieSet.FirstOrDefault(t => t.ImdbLink == actorKnownForMovieId);

                    if (isThereAmovie != null) // Film var mı? : Film var.
                    {
                        // Peki aktör var mı?
                        string actorName = actorsName[i];
                        Actor isThereAnActor = context.ActorSet.FirstOrDefault(t => t.FullName == actorName);

                        if (isThereAnActor == null) // böyle bir aktör yok (map da olamaz). ama bu aktörün (known for) movie'si var.
                        {
                            var actorToDb = new Actor()
                            {
                                Biography = "",
                                BirthDate = birthdateFinal[i],
                                CreatedAt = isThereAmovie.CreatedAt,
                                DeathDate = null,
                                FirstName = firstLastNameSeperated[i].First,
                                LastName = firstLastNameSeperated[i].Last,
                                FullName = actorsName[i],
                                Gender = Convert.ToBoolean(actorsIsMale[i]),
                                CountryId = 1,
                                CountyId = 1,
                                StateId = 1,
                                ImdbLink = actorsConst[i],
                                Photo = "null"
                            };
                            context.ActorSet.Add(actorToDb);
                            context.SaveChanges();

                            context.MovieActorMappingSet.Add(new MovieActorMapping()
                            {
                                MovieId = isThereAmovie.Id,
                                ActorId = actorToDb.Id
                            });
                            context.SaveChanges();
                        }
                        else
                        {
                            // böyle bir aktör var. Film de vardı. O zaman map yapma zamanı. 
                            // Tabii hali hazırda map yoksa...

                            // önce gender ayarı
                            

                            MovieActorMapping mp = context.MovieActorMappingSet
                                .FirstOrDefault(t => t.MovieId == isThereAmovie.Id && t.ActorId == isThereAnActor.Id);

                            if (mp == null) // map yok
                            {
                                context.MovieActorMappingSet.Add(new MovieActorMapping()
                                {
                                    MovieId = isThereAmovie.Id,
                                    ActorId = isThereAnActor.Id
                                });
                                context.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        // Actor için KnownFor FİLM YOK. Peki böyle bir Actor var mı?
                        string actorName = actorsName[i];
                        Actor isThereAnActor = context.ActorSet.FirstOrDefault(t => t.FullName == actorName);

                        if (isThereAnActor == null) // böyle bir aktör de YOK: ekle o zaman.
                        {
                            var actorToDb = new Actor()
                            {
                                Biography = "",
                                BirthDate = birthdateFinal[i],
                                CreatedAt = DateTime.Now,
                                DeathDate = null,
                                FirstName = firstLastNameSeperated[i].First,
                                LastName = firstLastNameSeperated[i].Last,
                                FullName = actorsName[i],
                                Gender = Convert.ToBoolean(actorsIsMale[i]),
                                CountryId = 1,
                                CountyId = 1,
                                StateId = 1,
                                ImdbLink = actorsConst[i],
                                Photo = "null"
                            };
                            context.ActorSet.Add(actorToDb);
                            context.SaveChanges();
                        }
                    }
                }
            }
        }

        public static List<Movie> Movies()
        {
            var random = new Random();

            string[] movies = File.ReadAllLines(@"C:\seed\movies.txt", Encoding.Default);
            string[] years = File.ReadAllLines(@"C:\seed\movies_years.txt");
            string[] imdbLink = File.ReadAllLines(@"C:\seed\imdblink.txt");
            string[] moviesRuntime = File.ReadAllLines(@"C:\seed\movies_runtime.txt");
            string[] myratingDates = File.ReadAllLines(@"C:\seed\movie_rate_date.txt");
            var directors219 = File.ReadAllLines(@"C:\seed\directors.txt", Encoding.Default).ToList();

            var directors219Edit = new List<string>();
            foreach (var director in directors219)
            {
                if (director.Contains(","))
                {
                    directors219Edit.Add(director.Split(',')[0]);
                }
                else
                {
                    directors219Edit.Add(director);
                }
            }

            var imdbLinks = new List<string>();
            if (imdbLink[0].Contains("http://www.imdb.com/title/"))
            {
                foreach (var link in imdbLink)
                {
                    if (link.Contains("http://www.imdb.com/title/"))
                    {
                        imdbLinks.Add(link.Replace("http://www.imdb.com/title/", "").Replace("/", ""));
                    }
                }
            }
            else
            {
                imdbLinks.AddRange(imdbLink);
            }

            var directors = Directors();
            var filmler = new List<Movie>();
            string[] ratings = File.ReadAllLines(@"C:\seed\myratings.txt", Encoding.Default);
            var fixedGenresWithId = Seeder.RatedMoviesFixedGenres();
            for (int i = 0; i < movies.Count(); i++)
            {
                filmler.Add(new Movie()
                {
                    Name = movies[i],
                    Year = int.Parse(years[i]),
                    PlotOutline = LoremText[random.Next(0, LoremCount)],
                    CreatedAt = ReturnProperDate(myratingDates[i]), //randomDateTime.Next(),
                    ImdbLink = imdbLinks[i],
                    LanguageId = 1,
                    DirectorId = directors.First(t => t.FullName.Contains(directors219Edit[i])).Id,
                    RunningTime = int.Parse(moviesRuntime[i]),
                    Rating = float.Parse(ratings[i]),
                    VoteCount = 1,
                    CountryId = 1,
                    CoverImage = movies[i]
                                .Replace(" ", "-")
                                .Replace(":", "")
                                .Replace("?", "")
                                .Replace("*", "")
                                .Replace("/", "")
                                .ToLowerInvariant() + ".jpg"
                });
            }
            return filmler;
        }

        public static List<Director> Directors()
        {
            var directors = File.ReadAllLines(@"C:\seed\directors.txt", Encoding.Default);
            string[] myratingDates = File.ReadAllLines(@"C:\seed\movie_rate_date.txt");

            var rnd = new Random();
            var randomDateTime = new RandomDateTime();

            var directorsList = new List<string>();

            foreach (var director in directors)
            {
                string found = directorsList.Find(t => t == director);
               
                if (found == null)
                {
                    if (director.Contains(","))
                    {
                        directorsList.Add(director.Split(',')[0]);
                    }
                    else
                    {
                        directorsList.Add(director);
                    }
                }
            }

            var firstLastNamesSeperated = new List<FirstLastName>();
            foreach (var actorFullName in directorsList.Distinct())
            {
                var splitted = actorFullName.Split(' ');
                switch (splitted.Count())
                {
                    case 2:
                        firstLastNamesSeperated.Add(new FirstLastName() { First = splitted[0], Last = splitted[1] });
                        break;

                    case 3:
                        firstLastNamesSeperated.Add(new FirstLastName() { First = splitted[0], Last = splitted[1] + " " + splitted[2] });
                        break;

                    case 4:
                        firstLastNamesSeperated.Add(new FirstLastName() { First = splitted[0], Last = splitted[1] + " " + splitted[2] + " " + splitted[3] });
                        break;
                }
            }

            var finalList = new List<Director>();
            int id = 1;
            foreach (var dir in firstLastNamesSeperated)
            {
                finalList.Add(new Director()
                {
                    Id = id,
                    //Name = dir.First + " " + dir.Last,
                    FirstName = dir.First,
                    LastName = dir.Last,
                    BirthDate = null,
                    DeathDate = null,
                    FullName = dir.First + " " + dir.Last,
                    Biography = null, // LoremText[(rnd.Next(0, LoremCount))],
                    CreatedAt = ReturnProperDate(myratingDates[id-1]), // randomDateTime.Next(),
                    ImdbLink = null,
                    CountryId = 1,
                    StateId = 1,
                    CountyId = 1,
                    Gender = true
                });
                id++;
            }
            return finalList;
        }

        public static List<FixedGenresWithId> RatedMoviesFixedGenresWithId()
        {
            var ratedMoviesGenres = File.ReadAllLines(@"C:\seed\myratings_genre.txt", Encoding.Default);
            var final = new List<string[]>();

            foreach (var genre in ratedMoviesGenres)
            {                
                var gnr = genre.Replace(",", "").Split(' ');
                var tempArray = new string[gnr.Length];

                var counterForGen = 0;
                foreach (var g in gnr)
                {
                    if (g == "animation")
                    {
                        const string newGen = "Animation";
                        tempArray[counterForGen] = newGen;
                        counterForGen++;
                    }
                    else if (g == "sci_fi")
                    {
                        const string newGen = "Sci-Fi";
                        tempArray[counterForGen] = newGen;
                        counterForGen++;
                    }
                    else
                    {
                        var firstCharMod = g.Replace(g[0].ToString(), g[0].ToString().ToUpperInvariant());
                        tempArray[counterForGen] = firstCharMod.Trim();
                        counterForGen++;
                    }
                }
                counterForGen = 0;
                
                final.Add(tempArray);
            }

            var genresWithId = new List<FixedGenresWithId>();
            var genreIdForMovie = 1;
            foreach (string[] array in final)
            {
                genresWithId.Add(new FixedGenresWithId() { Id = genreIdForMovie, FixedGenres = array });
                genreIdForMovie++;
            }

            return genresWithId;
        }

        public static List<string[]> RatedMoviesFixedGenres()
        {
            var ratedMoviesGenres = File.ReadAllLines(@"C:\seed\movies_genre.txt", Encoding.Default);
            var final = new List<string[]>();

            foreach (var genre in ratedMoviesGenres)
            {
                var gnr = genre.Replace(",", "").Split(' ');
                var tempArray = new string[gnr.Length];

                var counterForGen = 0;
                foreach (var g in gnr)
                {
                    if (g == "animation")
                    {
                        const string newGen = "Animation";
                        tempArray[counterForGen] = newGen;
                        counterForGen++;
                    }
                    else if (g == "sci_fi")
                    {
                        const string newGen = "Sci-Fi";
                        tempArray[counterForGen] = newGen;
                        counterForGen++;
                    }
                    else
                    {
                        var firstCharMod = g.Replace(g[0].ToString(), g[0].ToString().ToUpperInvariant());
                        tempArray[counterForGen] = firstCharMod;
                        counterForGen++;
                    }
                }
                counterForGen = 0;

                final.Add(tempArray);
            }
            return final;
        }

        public static void SeedGenreMovieMappingTable()
        {
            var context = new MovieDbContext();

            var container = RatedMoviesFixedGenresWithId();

            foreach (var genreArray in container)
            {
                var movieId = genreArray.Id;
                foreach (var genreName in genreArray.FixedGenres)
                {
                    // genreName için genreId
                    int genreId = context.GenreSet.First(t => t.Name == genreName).Id;

                    context.MovieGenreMappingSet.Add(
                        new MovieGenreMapping()
                        {
                            MovieId = movieId,
                            GenreId = genreId,
                        });
                    context.SaveChanges();
                }
            }           
        }

        public static DateTime ReturnProperDate(string imdbInput)
        {
            var year = 0;
            var month = 0;
            var day = 0;

            var dateParts = imdbInput.Split(' ');
            if (dateParts.Length == 5)
            {
                // like "Mon Nov 2 00:00:00 2015"

                year = int.Parse(dateParts[4]);
                month = MonthToInteger(dateParts[1]);
                day = int.Parse(dateParts[2]);
            }
            else
            {
                // like "Mon Nov 12 00:00:00 2015"

                year = int.Parse(dateParts[5]);
                month = MonthToInteger(dateParts[2]);
                day = int.Parse(dateParts[3]);
            }
            return new DateTime(year, month, day);
        }

        public static int MonthToInteger(string month)
        {
            var months = new Dictionary<string, int>
            {
                {"January", 1},
                {"February", 2},
                {"March", 3},
                {"April", 4},
                {"May", 5},
                {"June", 6},
                {"July", 7},
                {"August", 8},
                {"September", 9},
                {"October", 10},
                {"November", 11},
                {"December", 12}
            };

            int returnValue = 0;
            foreach (var mo in months)
            {
                if (mo.Key.Contains(month) || mo.Key == month)
                    returnValue = mo.Value;
            }
            return returnValue;
        }

        public static byte[] ImageToByteArray(Image imageIn)
        {
            using (imageIn)
            {
                MemoryStream ms = new MemoryStream();
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                Image returnImage = Image.FromStream(ms);
                return returnImage;
            }
        }

        public class FirstLastName
        {
            public string First { get; set; }
            public string Last { get; set; }
        }

        public class FixedGenresWithId
        {
            public int Id { get; set; }
            public string[] FixedGenres { get; set; }
        }
    }
    public class RandomDateTime
    {
        private DateTime start;
        private Random gen = new Random();
        private int range;

        public RandomDateTime()
        {
            start = new DateTime(2000, 1, 1);
            gen = new Random();
            range = (DateTime.Today - start).Days;
        }

        public DateTime Next()
        {
            return
                start.AddDays(gen.Next(range))
                    .AddHours(gen.Next(0, 24))
                    .AddMinutes(gen.Next(0, 60))
                    .AddSeconds(gen.Next(0, 60));
        }
    }
}