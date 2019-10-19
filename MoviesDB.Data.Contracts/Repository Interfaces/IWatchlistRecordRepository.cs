using System.Collections.Generic;
using Core.Common.Contracts;
using MoviesDB.Business.Entities;

namespace MoviesDB.Data.Contracts
{
    public interface IWatchlistRecordRepository : IDataRepository<WatchlistRecord>
    {
        IEnumerable<WatchlistRecord> GetByPage(int page, int pageSize);

    }
}