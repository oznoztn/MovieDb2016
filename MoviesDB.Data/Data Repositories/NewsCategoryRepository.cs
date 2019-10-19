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
    [Export(typeof(INewsCategoryRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class NewsCategoryRepository : DataRepositoryBase<NewsCategory>, INewsCategoryRepository
    {
        protected override NewsCategory AddEntity(MovieDbContext entityContext, NewsCategory entity)
        {
            return entityContext.NewsCategorySet.Add(entity);
        }

        protected override NewsCategory UpdateEntity(MovieDbContext entityContext, NewsCategory entity)
        {
            return (from e in entityContext.NewsCategorySet
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override NewsCategory GetEntity(MovieDbContext entityContext, int id)
        {
            return (from e in entityContext.NewsCategorySet
                    where e.Id == id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<NewsCategory> GetEntities(MovieDbContext entityContext)
        {
            return entityContext.NewsCategorySet.ToList();
        }

        public IEnumerable<NewsCategory> GetByPage(int page, int pageSize)
        {
            using (var entityContext = new MovieDbContext())
            {
                return entityContext.NewsCategorySet.OrderBy(t => t.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }
    }
}
