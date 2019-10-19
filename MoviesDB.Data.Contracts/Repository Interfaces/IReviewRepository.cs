using System.Collections.Generic;
using Core.Common.Contracts;
using MoviesDB.Business.Entities;

namespace MoviesDB.Data.Contracts
{
    public interface IReviewRepository : IDataRepository<Review>
    {
        IEnumerable<Review> GetByPage(int page, int pageSize);
    }
}