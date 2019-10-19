using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using MoviesDB.Business.Entities;
using MoviesDB.Data.Contracts;

namespace MoviesDB.Data
{
    [Export(typeof(ILanguageRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LanguageRepository : DataRepositoryBase<Language>, ILanguageRepository
    {
        protected override Language AddEntity(MovieDbContext entityContext, Language entity)
        {
            return entityContext.LanguageSet.Add(entity);
        }

        protected override Language UpdateEntity(MovieDbContext entityContext, Language entity)
        {
            return (from e in entityContext.LanguageSet
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override Language GetEntity(MovieDbContext entityContext, int id)
        {
            return (from e in entityContext.LanguageSet
                    where e.Id == id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Language> GetEntities(MovieDbContext entityContext)
        {
            return entityContext.LanguageSet.ToList();
        }

        public IEnumerable<Language> GetByPage(int page, int pageSize)
        {
            using (var entityContext = new MovieDbContext())
            {
                return entityContext.LanguageSet.OrderBy(t => t.Id).Skip((page - 1)*pageSize).Take(pageSize).ToList();
            }
        }

        public int TotalCount()
        {
            using (var context = new MovieDbContext())
            {
                return context.LanguageSet.Count();
            }
        }
    }
}
