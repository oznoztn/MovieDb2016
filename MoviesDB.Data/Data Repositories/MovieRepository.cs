using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Core.Common.Utils;
using MoviesDB.Business.Entities;
using MoviesDB.Business.Entities.DTOs;
using MoviesDB.Data.Contracts;

namespace MoviesDB.Data
{
    [Export(typeof(IMovieRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MovieRepository : DataRepositoryBase<Movie>, IMovieRepository
    {
        protected override Movie AddEntity(MovieDbContext entityContext, Movie entity)
        {
            return entityContext.MovieSet.Add(entity);
        }

        protected override Movie UpdateEntity(MovieDbContext entityContext, Movie entity)
        {
            return (from m in entityContext.MovieSet
                    where m.Id == entity.Id
                    select m).FirstOrDefault();
        }

        protected override Movie GetEntity(MovieDbContext entityContext, int id)
        {
            return (from m in entityContext.MovieSet
                    where m.Id == id
                    select m).FirstOrDefault();
        }

        protected override IEnumerable<Movie> GetEntities(MovieDbContext entityContext)
        {
            return entityContext.MovieSet.ToList();
        }

        public MovieCreationData Add(MovieCreationData movieCreationData)
        {
            using (var entityContext = new MovieDbContext())
            {
                var movie = new Movie();
                SimpleMapper.PropertyMap(movieCreationData, movie);

                movie.CreatedAt = DateTime.Now;

                entityContext.MovieSet.Add(movie);
                entityContext.SaveChanges();

                if (movieCreationData.GenreIds != null)
                {
                    foreach (var genreId in movieCreationData.GenreIds)
                    {
                        entityContext.MovieGenreMappingSet.Add(new MovieGenreMapping()
                        {
                            GenreId = genreId,
                            MovieId = movie.Id
                        });
                    }
                }

                if (movieCreationData.SubGenreIds != null)
                {
                    foreach (var subGenreId in movieCreationData.SubGenreIds)
                    {
                        entityContext.MovieGenreMappingSet.Add(new MovieGenreMapping()
                        {
                            GenreId = subGenreId,
                            MovieId = movie.Id
                        });
                    }
                }

                if (movieCreationData.ActorIds != null) 
                {
                    foreach (var actorId in movieCreationData.ActorIds)
                    {
                        entityContext.MovieActorMappingSet.Add(new MovieActorMapping()
                        {
                            ActorId = actorId,
                            MovieId = movie.Id
                        });
                    }
                }
                //entityContext.SaveChanges();
            }
            return movieCreationData;
        }

        public MovieCreationData UpdateSec(MovieCreationData movieCreationData)
        {
            if (movieCreationData.GenreIds == null)
                movieCreationData.GenreIds = new int[]{};

            if (movieCreationData.SubGenreIds != null && movieCreationData.SubGenreIds.Count() > 0)
                movieCreationData.GenreIds = movieCreationData.GenreIds.Union(movieCreationData.SubGenreIds).ToArray();

            using (var entityContext = new MovieDbContext())
            {
                var existingMovie = (from m in entityContext.MovieSet where m.Id == movieCreationData.Id select m).FirstOrDefault();
                SimpleMapper.PropertyMap(movieCreationData, existingMovie);
                entityContext.SaveChanges();

                // ADIM 1 //
                var currentGenreMappings = entityContext.MovieGenreMappingSet.Where(t => t.MovieId == movieCreationData.Id).ToList();

                // (True ise) Güncel Movie'de, veritabanındaki haline kıyasla, daha az Genre (Mapping) bulunmakta..
                if (currentGenreMappings.Count > movieCreationData.GenreIds.Count())
                {
                    for (int i = 0; i < movieCreationData.GenreIds.Count(); i++)
                    {
                        currentGenreMappings[i].GenreId = movieCreationData.GenreIds[i];
                    }
                    //2
                    for (int i = movieCreationData.GenreIds.Count(); i < currentGenreMappings.Count; i++)
                    {
                        entityContext.MovieGenreMappingSet.Remove(currentGenreMappings[i]);
                    }
                    entityContext.SaveChanges();
                }
                else
                {
                    int index = 0;
                    foreach (var currentGenreMap in currentGenreMappings)
                    {
                        currentGenreMap.GenreId = movieCreationData.GenreIds[index];
                        index++;
                    }

                    if (movieCreationData.GenreIds.Count() != currentGenreMappings.Count)
                    {
                        for (int i = index; i < movieCreationData.GenreIds.Count(); i++)
                        {
                            var newGenreMapping = new MovieGenreMapping() { MovieId = movieCreationData.Id, GenreId = movieCreationData.GenreIds[i] };
                            entityContext.MovieGenreMappingSet.Add(newGenreMapping);
                        }
                    }
                    entityContext.SaveChanges();
                }

                // ADIM 2 //
                var currentActorMappings = entityContext.MovieActorMappingSet.Where(t => t.MovieId == movieCreationData.Id).ToList();

                // (True ise) Güncel Movie'de, veritabanındaki haline kıyasla, daha az Actor (Mapping) bulunmakta..
                if (movieCreationData.ActorIds.Count() < currentActorMappings.Count)
                {
                    for (int i = 0; i < movieCreationData.ActorIds.Count(); i++)
                    {
                        currentActorMappings[i].ActorId = movieCreationData.ActorIds[i];
                    }
                    //2
                    for (int i = movieCreationData.ActorIds.Count(); i < currentActorMappings.Count; i++)
                    {
                        entityContext.MovieActorMappingSet.Remove(currentActorMappings[i]);
                    }
                    entityContext.SaveChanges();
                }
                else
                {
                    int index = 0;
                    foreach (var currentActorMap in currentActorMappings)
                    {
                        currentActorMap.ActorId = movieCreationData.ActorIds[index];
                        index++;
                    }

                    if (movieCreationData.ActorIds.Count() != currentActorMappings.Count)
                    {
                        for (int i = index; i < movieCreationData.ActorIds.Count(); i++)
                        {
                            var newActorMapping = new MovieActorMapping() { MovieId = movieCreationData.Id, ActorId = movieCreationData.ActorIds[i] };
                            entityContext.MovieActorMappingSet.Add(newActorMapping);
                        }
                    }
                    entityContext.SaveChanges();
                }
            }
            return movieCreationData;
        }

        public MovieData GetMovieDataByImdbId(string imdbId)
        {
            using (var entityContext = new MovieDbContext())
            {
                return
                    (from movie in entityContext.MovieSet 
                     where movie.ImdbLink == imdbId 
                     select new MovieData()
                     {
                         Name = movie.Name,
                         Id = movie.Id
                     }).FirstOrDefault();
            }
        }

        public MovieData UpdateSimple(MovieData movieData)
        {
            using (var entityContext = new MovieDbContext())
            {
                var entity = entityContext.MovieSet.Find(movieData.Id);
                entity.Name = movieData.Name;

                entityContext.SaveChanges();

                return movieData;
            }
        }

        public MovieDetailsData GetDetails(int movieId)
        {
            using (var entityContext = new MovieDbContext())
            {
                var movieDetails = 
                    (from m in entityContext.MovieSet.Where(t => t.Id == movieId)
                     join c in entityContext.CountrySet on m.CountryId equals c.Id
                     join d in entityContext.DirectorSet on m.DirectorId equals d.Id
                     join l in entityContext.LanguageSet on m.LanguageId equals l.Id
                              select new MovieDetailsData()
                              {
                                  Name = m.Name,
                                  ImdbLink = m.ImdbLink,
                                  CoverImage = m.CoverImage,
                                  Aka = m.Aka,
                                  Year = m.Year,
                                  VoteCount = m.VoteCount,
                                  DirectorName = d.FullName,
                                  DirectorImdbLink = d.ImdbLink,
                                  DirectorPhoto = d.Photo,
                                  Country = c.Name,
                                  Language = l.Name,
                                  PlotOutline = m.PlotOutline,
                                  RunningTime = m.RunningTime,
                                  Rating = m.Rating
                              }).Single();

                movieDetails.Genres = (from ge in entityContext.GenreSet
                                       join gm in entityContext.MovieGenreMappingSet.Where(t => t.Id == movieId) 
                                       on ge.Id equals gm.GenreId select ge).ToArray();

                movieDetails.Actors = (from ac in entityContext.ActorSet
                                       join map in entityContext.MovieActorMappingSet.Where(t => t.MovieId == movieId)
                                       on ac.Id equals map.ActorId select ac).ToArray();

                movieDetails.News = (from n in entityContext.NewsSet
                                     join mp in entityContext.NewsMappingSet on n.Id equals mp.NewsId 
                                     where mp.MovieId == movieId select n).Distinct().ToArray();

                movieDetails.Reviews = entityContext.ReviewSet.Where(t => t.MovieId == movieId).ToArray();

                return movieDetails;
            }
        }

        public MovieCreationData GetSec(int movieId)
        {
            MovieCreationData movieData = new MovieCreationData();
            Movie movieEntity = new Movie();

            //var actorMappings = new MovieActorMapping();
            //var genreMappings = new MovieGenreMapping();

            using (var entityContext = new MovieDbContext())
            {
                movieEntity = entityContext.MovieSet.Find(movieId);
                movieData.GenreIds = entityContext.MovieGenreMappingSet
                                        .Where(t => t.MovieId == movieId)
                                        .Select(t => t.GenreId).ToArray();
                
                movieData.ActorIds = entityContext.MovieActorMappingSet
                                        .Where(t => t.MovieId == movieId)
                                        .Select(t => t.ActorId).ToArray();
            }
            if(movieEntity != null)
                SimpleMapper.PropertyMap(movieEntity, movieData);
            
            return movieData;
        }

        public IEnumerable<MovieData> GetByPage(int page, int pageSize)
        {
            using (var entityContext = new MovieDbContext())
            {
                return
                    entityContext.MovieSet.OrderBy(t => t.Id)
                        .Skip((page - 1)*pageSize)
                        .Take(pageSize)
                        .Select(t => new MovieData() {Id = t.Id, Name = t.Name})
                        .ToList();
            }
        }

        public IEnumerable<Movie> GetMoviesByActorId(int actorId)
        {
            using (var entityContext = new MovieDbContext())
            {
                return null;
            }
        }

        public IEnumerable<Movie> GetMoviesByDirectorId(int directorId)
        {
            using (var entityContext = new MovieDbContext())
            {
                return null;
            }
        }

        public IEnumerable<MovieData> GetMoviesByActorId_Simplified(int actorId)
        {
            using (var entityContext = new MovieDbContext())
            {

                return null;
            }
        }

        public IEnumerable<MovieData> GetMoviesByDirectorId_Simplified(int directorId)
        {
            using (var entityContext = new MovieDbContext())
            {
                var directorsById = (from mo in entityContext.MovieSet.Where(t => t.DirectorId == directorId).ToList() 
                                     join di in entityContext.DirectorSet.Where(t => t.Id == directorId)
                                     on mo.DirectorId equals di.Id select mo);

                return null;
            }
        }

        public StatsMovieYear[] Statistics_MoviesByYear()
        {
            using (var entityContext = new MovieDbContext())
            {
                 return (from m in entityContext.MovieSet 
                         group m by new {m.Year} into grp
                         select new StatsMovieYear()
                         {
                             Count = grp.Count(),
                             Year = grp.Key.Year
                         }).ToArray();
            }
        }

        public StatsMovieGenre[] Statistics_MoviesByGenre()
        {
            using (var entityContext = new MovieDbContext())
            {
                 return (from g in entityContext.GenreSet 
                         join gm in entityContext.MovieGenreMappingSet on g.Id equals gm.GenreId                         
                         group g by new {g.Name} into grp orderby grp.Count() descending 
                         select new StatsMovieGenre()
                         {
                             Count = grp.Count(),
                             Genre = grp.Key.Name
                         }).ToArray();
            }
        }

        public MovieData[] SearchMovie(string name)
        {
            using (var entityContext = new MovieDbContext())
            {
                return entityContext.MovieSet.Where(t => t.Name.ToUpper().Contains(name.ToUpper()))
                    .Select(movie => new MovieData() {
                        Id = movie.Id,
                        Name = movie.Name
                    }).ToArray();
            }
        }

        public int TotalCount()
        {
            var totalCount = 0;
            using (var entityContext = new MovieDbContext())
            {
                totalCount = entityContext.MovieSet.Count();
            }

            return totalCount;
        }
    } 
}