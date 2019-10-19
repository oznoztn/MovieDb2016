using System.ComponentModel.Composition;
using Core.Common.Proxy;
using MoviesDB.Client.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Proxies
{
    [Export(typeof(IWatchlistRecordService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class WatchlistEntryProxy : ProxyBase<IWatchlistRecordService>, IWatchlistRecordService
    {
        public WatchlistRecord Get(int id)
        {
            return Channel.Get(id);
        }

        public WatchlistRecord[] GetAll()
        {
            return Channel.GetAll();
        }

        public WatchlistRecord Update(WatchlistRecord entity)
        {
            return Channel.Update(entity);
        }

        public void Delete(int id)
        {
            Channel.Delete(id);
        }

        public WatchlistRecord[] GetByPage(int page, int pageSize)
        {
            return Channel.GetByPage(page, pageSize);
        }

        public int TotalCount()
        {
            return Channel.TotalCount();
        }
    }
}
