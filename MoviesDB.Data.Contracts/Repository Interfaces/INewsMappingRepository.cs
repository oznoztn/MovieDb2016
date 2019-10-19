using System.Collections.Generic;
using Core.Common.Contracts;
using MoviesDB.Business.Entities;

namespace MoviesDB.Data.Contracts
{
    public interface INewsMappingRepository : IDataRepository<NewsMapping>
    {
        IEnumerable<NewsMapping> GetByPage(int page, int pageSize);

    }
}