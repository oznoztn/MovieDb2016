using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using MoviesDB.Business.Entities;
using MoviesDB.Business.Entities.DTOs;
using MoviesDB.Data.Contracts;

namespace MoviesDB.Data
{
    [Export(typeof(ICountryRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CountryRepository : DataRepositoryBase<Country>, ICountryRepository
    {
        protected override Country AddEntity(MovieDbContext entityContext, Country entity)
        {
            return entityContext.CountrySet.Add(entity);
        }

        protected override Country UpdateEntity(MovieDbContext entityContext, Country entity)
        {
            return (from c in entityContext.CountrySet where c.Id == entity.Id select c).FirstOrDefault();
        }

        protected override Country GetEntity(MovieDbContext entityContext, int id)
        {
            return (from c in entityContext.CountrySet where c.Id == id select c).FirstOrDefault();
        }

        protected override IEnumerable<Country> GetEntities(MovieDbContext entityContext)
        {
            return entityContext.CountrySet.ToList();
        }

        public IEnumerable<Country> SearchCountry(string searchTerm)
        {
            using (var entityContext = new MovieDbContext())
            {
                var movies = entityContext.CountrySet.Where(t => t.Name.ToUpper().Contains(searchTerm.ToUpper())).ToList();

                return movies;
            }
        }

        public IEnumerable<Country> GetByPage(int page, int pageSize)
        {
            using (var entityContext = new MovieDbContext())
            {
                return entityContext.CountrySet.OrderBy(t => t.Id).Skip((page - 1)*pageSize).Take(pageSize).ToList();
            }
        }

        public int TotalCount()
        {
            using (var entityContext = new MovieDbContext())
            {
                return entityContext.CountrySet.Count();
            }
        }

        public StatsMovieGenre[] Statistics()
        {
            using (var entityContext = new MovieDbContext())
            {
                var stats = (from c in entityContext.CountrySet 
                             join m in entityContext.MovieSet on c.Id equals m.CountryId     
                             group c by new {c.Name} into grp orderby grp.Count() descending                              
                             where grp.Count() >= 3 
                             select new StatsMovieGenre()
                             {
                                 Genre = grp.Key.Name,
                                 Count = grp.Count()
                             }).ToArray();

                return stats;
            }
        }
    }
}
