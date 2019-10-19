using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using MoviesDB.Business.Entities;
using MoviesDB.Business.Entities.DTOs;
using MoviesDB.Data.Contracts;

namespace MoviesDB.Data
{
    [Export(typeof(IUserRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserRepository : DataRepositoryBase<User>, IUserRepository
    {
        protected override User AddEntity(MovieDbContext entityContext, User entity)
        {
            return entityContext.UserSet.Add(entity);
        }

        protected override User UpdateEntity(MovieDbContext entityContext, User entity)
        {
            return (from e in entityContext.UserSet
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override User GetEntity(MovieDbContext entityContext, int id)
        {
            return (from e in entityContext.UserSet
                    where e.Id == id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<User> GetEntities(MovieDbContext entityContext)
        {
            return entityContext.UserSet.ToList();
        }

        public StatsMovieGenre[] Statistics_RatedMoviesByGenre(int userId)
        {
            // Hangi genre'deki filmlere oy verilmiş? Action: 8, SciFi: 10
            using (var entityContext = new MovieDbContext())
            {
                var rtngGenreDist = (from u in entityContext.UserSet where u.Id == userId
                                     join r in entityContext.RatingSet on u.Id equals r.UserId
                                     join gm in entityContext.MovieGenreMappingSet on r.MovieId equals gm.MovieId
                                     join g in entityContext.GenreSet on gm.GenreId equals g.Id
                                     group g by new {g.Name} into grp orderby grp.Count() descending
                                     select new StatsMovieGenre()
                                     {
                                         Genre = grp.Key.Name,
                                         Count = grp.Count()
                                     }).ToArray();

                return rtngGenreDist;
            }
        }

        public StatsMovieYear[] Statistics_RatedMoviesByYear(int userId)
        {
            // Kaç tane filme kaç puan verilmiş? 10 puan: 8, 9 puan: 7
            using (var entityContext = new MovieDbContext())
            {
                var rtndstbyYear = (from u in entityContext.UserSet
                                    where u.Id == userId
                                    join r in entityContext.RatingSet on u.Id equals r.UserId
                                    join m in entityContext.MovieSet on r.MovieId equals m.Id
                                    group m by new { m.Year } into grp 
                                    select new StatsMovieYear()
                                        {
                                            Count = grp.Count(),
                                            Year = grp.Key.Year
                                        }).ToArray();

                return rtndstbyYear;
            }            
        }

        public StatsRatingDistribution[] Statistics_RatingDistrubition(int userId)
        {
            using (var entityContext = new MovieDbContext())
            {
                var movies = (from u in entityContext.UserSet
                              where u.Id == userId
                              join r in entityContext.RatingSet on u.Id equals r.UserId
                              group r by new { r.Rate } into grp
                              select new StatsRatingDistribution
                                  {
                                      Rate = grp.Key.Rate,
                                      Count = grp.Count()
                                  }).ToArray();

                return movies;
            }
        }

        public StatsMovieGenre[] Statistics_Top10Directors(int userId)
        {
            using (var entityContext = new MovieDbContext())
            {
                var top10 = (from rating in entityContext.RatingSet
                             where rating.UserId == userId
                             join movie in entityContext.MovieSet on rating.MovieId equals movie.Id
                             join director in entityContext.DirectorSet on movie.DirectorId equals director.Id
                             group director by new { director.FullName } into grp
                             orderby grp.Count() descending
                             select new StatsMovieGenre()
                             {
                                 Genre = grp.Key.FullName,
                                 Count = grp.Count()
                             }).Take(25).ToArray();
                return top10;
            }
        }

        public StatsMovieGenre[] Statistics_Top10Actor(int userId)
        {
            using (var entityContext = new MovieDbContext())
            {
                var top10 = (from rating in entityContext.RatingSet
                             where rating.UserId == userId
                             join map in entityContext.MovieActorMappingSet on rating.MovieId equals map.MovieId
                             join actor in entityContext.ActorSet on map.ActorId equals actor.Id
                             group actor by new { actor.FullName } into grp
                             orderby grp.Count() descending
                             select new StatsMovieGenre()
                             {
                                 Count = grp.Count(),
                                 Genre = grp.Key.FullName
                             }).Take(25).ToArray();
                return top10;
            }
        }

        public User GetByEmail(string email)
        {
            using (var entityContext = new MovieDbContext())
            {
                return entityContext.UserSet.FirstOrDefault(t => t.Mail == email);
            }
        }

        public User GetByUsername(string username)
        {
            using (var entityContext = new MovieDbContext())
            {
                return entityContext.UserSet.FirstOrDefault(t => t.Username == username);
            }
        }

        public User GetByTwitterId(string twitterId)
        {
            using (var entityContext = new MovieDbContext())
            {
                return entityContext.UserSet.FirstOrDefault(t => t.Twitter == twitterId);
            }
        }

        public User GetByFacebookId(string facebookId)
        {
            using (var entityContext = new MovieDbContext())
            {
                return entityContext.UserSet.FirstOrDefault(t => t.Facebook == facebookId);
            }
        }

        public User GetByInstagramId(string instagramId)
        {
            using (var entityContext = new MovieDbContext())
            {
                return entityContext.UserSet.FirstOrDefault(t => t.Instagram == instagramId);
            }
        }

        public User GetByGoogleId(string googleId)
        {
            using (var entityContext = new MovieDbContext())
            {
                return entityContext.UserSet.FirstOrDefault(t => t.Google == googleId);
            }
        }

        public User[] GetByPage(int page, int pageSize)
        {
            using (var entityContext = new MovieDbContext())
            {
                return entityContext.UserSet.OrderBy(t => t.Id).Skip((page - 1) * pageSize).Take(pageSize).ToArray();
            }
        }

        public int TotalCount()
        {
            using (var entityContext = new MovieDbContext())
            {
                return entityContext.UserSet.Count();
            }
        }
    }
}