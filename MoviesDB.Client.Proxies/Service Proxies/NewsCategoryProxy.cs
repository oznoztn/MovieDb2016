using System.ComponentModel.Composition;
using Core.Common.Proxy;
using MoviesDB.Client.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Proxies
{
    [Export(typeof(INewsCategoryService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class NewsCategoryProxy : ProxyBase<INewsCategoryService>, INewsCategoryService
    {
        public NewsCategory Get(int id)
        {
            return Channel.Get(id);
        }

        public NewsCategory[] GetAll()
        {
            return Channel.GetAll();
        }

        public NewsCategory Update(NewsCategory entity)
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
