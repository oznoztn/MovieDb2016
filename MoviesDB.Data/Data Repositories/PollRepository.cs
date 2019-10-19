using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Core.Common.Contracts;
using MoviesDB.Business.Entities;
using MoviesDB.Data.Contracts;

namespace MoviesDB.Data
{
    [Export(typeof(IPollRepository))]   
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PollRepository : DataRepositoryBase<Poll>, IPollRepository
    {
        protected override Poll AddEntity(MovieDbContext entityContext, Poll entity)
        {
            return entityContext.PollSet.Add(entity);
        }

        protected override Poll UpdateEntity(MovieDbContext entityContext, Poll entity)
        {
            return (from en in entityContext.PollSet where en.Id == entity.Id select en).FirstOrDefault();
        }

        protected override Poll GetEntity(MovieDbContext entityContext, int id)
        {
            return (from en in entityContext.PollSet where en.Id == id select en).FirstOrDefault();
        }

        protected override IEnumerable<Poll> GetEntities(MovieDbContext entityContext)
        {
            return entityContext.PollSet.ToList();
        }

        public IEnumerable<Poll> GetByPage(int page, int pageSize)
        {
            using (var entityContext = new MovieDbContext())
            {
                return entityContext.PollSet.OrderBy(t => t.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }
    }
}
