using System.ComponentModel.Composition;
using Core.Common.Proxy;
using MoviesDB.Client.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Proxies
{
    [Export(typeof(IReviewService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ReviewProxy : ProxyBase<IReviewService>, IReviewService
    {
        public Review Get(int id)
        {
            return Channel.Get(id);
        }

        public Review[] GetAll()
        {
            return Channel.GetAll();
        }

        public Review Update(Review entity)
        {
            return Channel.Update(entity);
        }

        public void Delete(int id)
        {
            Channel.Delete(id);
        }

        public Review[] GetByPage(int page, int pageSize)
        {
            return Channel.GetByPage(page, pageSize);
        }

        public int TotalCount()
        {
            return Channel.TotalCount();
        }
    }
}
