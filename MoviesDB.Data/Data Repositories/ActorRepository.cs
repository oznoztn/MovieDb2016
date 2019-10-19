using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Core.Common.Utils;
using MoviesDB.Business.Entities;
using MoviesDB.Business.Entities.DTOs;
using MoviesDB.Data.Contracts;

namespace MoviesDB.Data
{
    [Export(typeof(IActorRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ActorRepository : DataRepositoryBase<Actor>, IActorRepository
    {
        protected override Actor AddEntity(MovieDbContext entityContext, Actor entity)
        {
            return entityContext.ActorSet.Add(entity);
        }

        protected override Actor UpdateEntity(MovieDbContext entityContext, Actor entity)
        {
            return (from e in entityContext.ActorSet
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override Actor GetEntity(MovieDbContext entityContext, int id)
        {
            return (from e in entityContext.ActorSet
                    where e.Id == id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Actor> GetEntities(MovieDbContext entityContext)
        {
            return entityContext.ActorSet.ToList();
        }

        public IEnumerable<Actor> GetByPage(int page, int pageSize)
        {
            using (var entityContext = new MovieDbContext())
            {
                return entityContext.ActorSet.OrderBy(t => t.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public IEnumerable<ActorData> FindByName(string actorNameOrLastName)
        {
            string normalized = actorNameOrLastName.ToLower();

            using (var entityContext = new MovieDbContext())
            {
                var byName =
                    entityContext.ActorSet.Where(t => t.FirstName.ToLower().StartsWith(actorNameOrLastName))
                    .Select(act => new ActorData(){Id = act.Id, Name = act.FullName}).ToList();

                var bySurname = 
                    entityContext.ActorSet.Where(t => t.LastName.ToLower().StartsWith(actorNameOrLastName))
                    .Select(act => new ActorData() { Id = act.Id, Name = act.FullName }).ToList();

                return byName.Union(bySurname).ToList();
            }
        }

        public ActorCreationData Add(ActorCreationData actorCreationData)
        {
            using (var entityContext = new MovieDbContext())
            {
                Actor actorToDb = new Actor();

                SimpleMapper.PropertyMap(actorCreationData, actorToDb);

                entityContext.ActorSet.Add(actorToDb);
                entityContext.SaveChanges();

                if (actorCreationData.MovieIds != null)
                {
                    foreach (var movieId in actorCreationData.MovieIds)
                    {
                        entityContext.MovieActorMappingSet.Add(new MovieActorMapping()
                        {
                            ActorId = actorToDb.Id,
                            MovieId = movieId
                        });
                    }
                }
                entityContext.SaveChanges();
            }
            return actorCreationData;
        }

        public ActorCreationData UpdateSec(ActorCreationData actorCreationData)
        {
            using (var entityContext = new MovieDbContext())
            {
                // peki existing null ise? 
                var existingActor = (from x in entityContext.ActorSet where x.Id == actorCreationData.Id select x).FirstOrDefault();
                SimpleMapper.PropertyMap(actorCreationData, existingActor);
                
                var currentMovieActorMappings = entityContext.MovieActorMappingSet.Where(t => t.ActorId == actorCreationData.Id).ToList();

                if (actorCreationData.MovieIds == null)
                    actorCreationData.MovieIds = new int[] {};

                if (currentMovieActorMappings.Count > actorCreationData.MovieIds.Count())
                {
                    for (int i = 0; i < actorCreationData.MovieIds.Count(); i++)
                    {
                        currentMovieActorMappings[i].MovieId = actorCreationData.MovieIds[i];
                    }
                    //2
                    for (int i = actorCreationData.MovieIds.Count(); i < currentMovieActorMappings.Count; i++)
                    {
                        entityContext.MovieActorMappingSet.Remove(currentMovieActorMappings[i]);
                    }
                    entityContext.SaveChanges();
                }
                else
                {
                    int index = 0;
                    foreach (var currentGenreMap in currentMovieActorMappings)
                    {
                        currentGenreMap.MovieId = actorCreationData.MovieIds[index];
                        index++;
                    }

                    if (actorCreationData.MovieIds.Count() != currentMovieActorMappings.Count)
                    {
                        for (int i = index; i < actorCreationData.MovieIds.Count(); i++)
                        {
                            var newMovieActorMapping = new MovieActorMapping(){ ActorId = actorCreationData.Id, MovieId = actorCreationData.MovieIds[i] };
                            entityContext.MovieActorMappingSet.Add(newMovieActorMapping);
                        }
                    }
                    entityContext.SaveChanges();
                }
            }

            return null;
        }

        public ActorCreationData GetSec(int actorId)
        {
            var actorCreationData = new ActorCreationData();

            using (var entityContext = new MovieDbContext())
            {
                var actor = entityContext.ActorSet.Find(actorId);
                SimpleMapper.PropertyMap(actor, actorCreationData);

                actorCreationData.MovieIds = entityContext.MovieActorMappingSet.Where(t => t.ActorId == actorId).Select(t => t.MovieId).ToArray();
            }

            return actorCreationData;
        }

        public StatsMovieGenre[] Statistics_TopXActor(int topX)
        {
            using (var entityContext = new MovieDbContext())
            {
                var top10 = (from map in entityContext.MovieActorMappingSet
                             join act in entityContext.ActorSet on map.ActorId equals act.Id
                             group act by new { act.FullName } into grp
                             orderby grp.Count() descending
                             select new StatsMovieGenre()
                             {
                                 Genre = grp.Key.FullName,
                                 Count = grp.Count()
                             }).Take(topX).ToArray();

                return top10;
            }
        }

        public ActorDetailsData GetDetails(int actorId)
        {
            var actorDetails = new ActorDetailsData();

            using (var entityContext = new MovieDbContext())
            {
                if (entityContext.ActorSet.FirstOrDefault(t => t.Id == actorId) == null) 
                    return actorDetails;

                actorDetails =
                    (from a in entityContext.ActorSet.Where(t => t.Id == actorId)
                        join cr in entityContext.CountrySet on a.CountryId equals cr.Id
                        join cn in entityContext.CountySet on a.CountyId equals cn.Id
                        join st in entityContext.StateSet on a.StateId equals st.Id
                        select new ActorDetailsData()
                        {
                            FirstName = a.FirstName,
                            LastName = a.LastName,
                            FullName = a.FullName,
                            Biography = a.Biography,
                            BornDate = a.BirthDate,
                            DeathDate = a.DeathDate,
                            Country = cr.Name,
                            State = st.Name,
                            County = cn.Name,
                            ImdbLink = a.ImdbLink,
                            Photo = a.Photo,
                            Id = a.Id,
                            Gender = a.Gender
                        }).First();

                actorDetails.Movies =
                    (from mo in entityContext.MovieSet
                        join mp in entityContext.MovieActorMappingSet.Where(t => t.ActorId == actorId)
                            on mo.Id equals mp.MovieId
                        select mo).ToArray();

                actorDetails.News =
                    (from n in entityContext.NewsSet 
                     join mp in entityContext.NewsMappingSet.Where(t => t.ActorId == actorId)
                     on n.Id equals mp.NewsId
                     select n).Distinct().ToArray();
            }

            return actorDetails;
        }

        public IEnumerable<ActorData> ActorsForDropdownList()
        {
            using (var entityContext = new MovieDbContext())
            {
                return entityContext.ActorSet.Select(t => new ActorData() { Id = t.Id, Name = t.FullName }).ToList();
            }
        }

        public int TotalCount()
        {
            var totalCount = 0;
            using (var entityContext = new MovieDbContext())
            {
                totalCount = entityContext.ActorSet.Count();
            }

            return totalCount;
        }
    }
}
