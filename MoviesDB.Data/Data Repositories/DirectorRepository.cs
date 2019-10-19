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
    [Export(typeof(IDirectorRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DirectorRepository : DataRepositoryBase<Director>, IDirectorRepository
    {
        protected override Director AddEntity(MovieDbContext entityContext, Director entity)
        {
            entity.CreatedAt = DateTime.Now;
            return entityContext.DirectorSet.Add(entity);
        }

        protected override Director UpdateEntity(MovieDbContext entityContext, Director entity)
        {
            entity.CreatedAt = DateTime.Now;

            return (from e in entityContext.DirectorSet
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override Director GetEntity(MovieDbContext entityContext, int id)
        {
            return (from e in entityContext.DirectorSet
                    where e.Id == id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Director> GetEntities(MovieDbContext entityContext)
        {
            return entityContext.DirectorSet.ToList();
        }

        public IEnumerable<Director> GetByPage(int page, int pageSize)
        {
            using (var entityContext = new MovieDbContext())
            {
                return entityContext.DirectorSet.OrderBy(t => t.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public DirectorDetailsData GetDetails(int directorId)
        {
            var directorDetails = new DirectorDetailsData();

            using (var entityContext = new MovieDbContext())
            {
                if (entityContext.DirectorSet.FirstOrDefault(t => t.Id == directorId) == null) 
                    return directorDetails;

                directorDetails = (from d in entityContext.DirectorSet.Where(t => t.Id == directorId)
                    join cr in entityContext.CountrySet on d.CountryId equals cr.Id
                    join cn in entityContext.CountySet on d.CountyId equals cn.Id
                    join st in entityContext.StateSet on d.StateId equals st.Id
                    select new DirectorDetailsData()
                    {
                        FirstName = d.FirstName,
                        LastName = d.LastName,
                        FullName = d.FullName,
                        Biography = d.Biography,
                        BornDate = d.BirthDate,
                        DeathDate = d.DeathDate,
                        Country = cr.Name,
                        State = st.Name,
                        County = cn.Name,
                        ImdbLink = d.ImdbLink,
                        Photo = d.Photo,
                        Id = d.Id,
                        Gender = d.Gender
                    }).First();                

                directorDetails.Movies =
                    entityContext.MovieSet.Where(t => t.DirectorId == directorId).ToArray();

                directorDetails.News =
                    (from x in entityContext.NewsSet
                        join t in entityContext.NewsMappingSet.Where(t => t.ActorId == directorId)
                            on x.Id equals t.NewsId
                        select x).Distinct().ToArray();
            }

            return directorDetails;
        }
        
        public IEnumerable<DirectorData> FindByName(string directorNameOrLastName)
        {
            string normalized = directorNameOrLastName.ToLower();

            using (var entityContext = new MovieDbContext())
            {
                var byName = entityContext.DirectorSet.Where(t => t.FirstName.ToLower().StartsWith(directorNameOrLastName))
                    .Select(dir => new DirectorData() {Id = dir.Id, Name = dir.FullName}).ToList();

                var bySurname = entityContext.DirectorSet.Where(t => t.LastName.ToLower().StartsWith(directorNameOrLastName))
                    .Select(dir => new DirectorData() { Id = dir.Id, Name = dir.FullName }).ToList();

                return byName.Union(bySurname).ToList();
            }
        }

        public IEnumerable<DirectorData> DirectorsForDropdownList()
        {
            using (var entityContext = new MovieDbContext())
            {
                return entityContext.DirectorSet.Select(dir => new DirectorData() { Id = dir.Id, Name = dir.FullName }).ToList();
            }
        }

        public StatsMovieGenre[] Statistics_TopXDirectors(int topX)
        {
            using (var entityContext = new MovieDbContext())
            {
                var top = (from mov in entityContext.MovieSet
                             join dir in entityContext.DirectorSet on mov.DirectorId equals dir.Id
                             group dir by new { dir.FullName } into grp
                             orderby grp.Count() descending
                             select new StatsMovieGenre()
                             {
                                 Count = grp.Count(),
                                 Genre = grp.Key.FullName
                             }).Take(topX).ToArray();
                return top;
            }
        }

        public int TotalCount()
        {
            using (var entityContext = new MovieDbContext())
            {
                return entityContext.DirectorSet.Count();
            }
        }
    }
}
