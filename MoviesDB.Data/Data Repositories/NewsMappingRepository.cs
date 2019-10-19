using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using MoviesDB.Business.Entities;
using MoviesDB.Data.Contracts;

namespace MoviesDB.Data
{
    [Export(typeof(INewsMappingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class NewsMappingRepository : DataRepositoryBase<NewsMapping>, INewsMappingRepository
    {
        protected override NewsMapping AddEntity(MovieDbContext entityContext, NewsMapping entity)
        {
            return entityContext.NewsMappingSet.Add(entity);
        }

        protected override NewsMapping UpdateEntity(MovieDbContext entityContext, NewsMapping entity)
        {
            return (from en in entityContext.NewsMappingSet where en.Id == entity.Id select en).FirstOrDefault();
        }

        protected override NewsMapping GetEntity(MovieDbContext entityContext, int id)
        {
            return (from en in entityContext.NewsMappingSet where en.Id == id select en).FirstOrDefault();
        }

        protected override IEnumerable<NewsMapping> GetEntities(MovieDbContext entityContext)
        {
            return entityContext.NewsMappingSet.ToList();
        }

        public IEnumerable<NewsMapping> GetByPage(int page, int pageSize)
        {
            using (var entityContext = new MovieDbContext())
            {
                return entityContext.NewsMappingSet.OrderBy(t => t.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }
    }
}
