using System.ComponentModel.Composition;
using Core.Common.Proxy;
using MoviesDB.Client.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Proxies
{
    [Export(typeof(IRatingService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RatingProxy : ProxyBase<IRatingService>, IRatingService
    {
        public Rating Get(int id)
        {
            return Channel.Get(id);
        }

        public Rating[] GetAll()
        {
            return Channel.GetAll();
        }

        public Rating Update(Rating entity)
        {
            return Channel.Update(entity);
        }

        public void Delete(int id)
        {
            Channel.Delete(id);
        }

        public Rating[] GetByPage(int page, int pageSize)
        {
            return Channel.GetByPage(page, pageSize);
        }

        public int TotalCount()
        {
            return Channel.TotalCount();
        }
    }
}
