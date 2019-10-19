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
    [Export(typeof(IWatchlistRecordRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class WatchlistRecordRepository : DataRepositoryBase<WatchlistRecord>, IWatchlistRecordRepository
    {
        protected override WatchlistRecord AddEntity(MovieDbContext entityContext, WatchlistRecord entity)
        {
            return entityContext.WatchlistRecordSet.Add(entity);
        }

        protected override WatchlistRecord UpdateEntity(MovieDbContext entityContext, WatchlistRecord entity)
        {
            return (from e in entityContext.WatchlistRecordSet where e.Id == entity.Id select e).FirstOrDefault();
        }

        protected override WatchlistRecord GetEntity(MovieDbContext entityContext, int id)
        {
            return (from e in entityContext.WatchlistRecordSet where e.Id == id select e).FirstOrDefault();
        }

        protected override IEnumerable<WatchlistRecord> GetEntities(MovieDbContext entityContext)
        {
            return entityContext.WatchlistRecordSet.ToList();
        }

        public IEnumerable<WatchlistRecord> GetByPage(int page, int pageSize)
        {
            using (var entityContext = new MovieDbContext())
            {
                return entityContext.WatchlistRecordSet.OrderBy(t => t.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }
    }
}
