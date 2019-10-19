using System.ComponentModel.Composition;
using Core.Common.Proxy;
using MoviesDB.Client.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Proxies
{
    [Export(typeof(INewsMappingService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class NewsMappingProxy : ProxyBase<INewsMappingService>, INewsMappingService
    {
        public NewsMapping Get(int id)
        {
            return Channel.Get(id);
        }

        public NewsMapping[] GetAll()
        {
            return Channel.GetAll();
        }

        public NewsMapping Update(NewsMapping entity)
        {
            return Channel.Update(entity);
        }

        public void Delete(int id)
        {
            Channel.Delete(id);
        }

        public News[] GetByPage(int page, int pageSize)
        {
            return Channel.GetByPage(page, pageSize);
        }

        public int TotalCount()
        {
            return Channel.TotalCount();
        }
    }
}
