using System.ServiceModel;
using MoviesDB.Business.Contracts;
using MoviesDB.Business.Entities;

namespace MoviesDB.Business.Services.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class WatchlistRecordService : ServiceBase, IWatchlistRecordService
    {
        public WatchlistRecord Get(int entityId)
        {
            throw new System.NotImplementedException();
        }

        public WatchlistRecord[] GetAll()
        {
            throw new System.NotImplementedException();
        }

        public WatchlistRecord Update(Language entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int entityId)
        {
            throw new System.NotImplementedException();
        }

        public WatchlistRecord[] GetByPage(int page, int pageSize)
        {
            throw new System.NotImplementedException();
        }

        public int TotalCount()
        {
            throw new System.NotImplementedException();
        }
    }
}
