using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesDB.Business.Entities;
using MoviesDB.Data.Contracts;

namespace MoviesDB.Data
{
    [Export(typeof(INewsRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class NewsRepository : DataRepositoryBase<News>, INewsRepository
    {
        protected override News AddEntity(MovieDbContext entityContext, News entity)
        {
            return entityContext.NewsSet.Add(entity);
        }

        protected override News UpdateEntity(MovieDbContext entityContext, News entity)
        {
            return (from e in entityContext.NewsSet
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override News GetEntity(MovieDbContext entityContext, int id)
        {
            return (from e in entityContext.NewsSet
                    where e.Id == id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<News> GetEntities(MovieDbContext entityContext)
        {
            return entityContext.NewsSet.ToList();
        }

        public IEnumerable<News> GetByPage(int page, int pageSize)
        {
            using (var entityContext = new MovieDbContext())
            {
                return entityContext.NewsSet.OrderBy(t => t.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }
    }
}
