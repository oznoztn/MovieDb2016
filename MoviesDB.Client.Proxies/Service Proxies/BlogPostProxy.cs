using System.ComponentModel.Composition;
using Core.Common.Proxy;
using MoviesDB.Client.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Proxies
{
    [Export(typeof(IBlogPostService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BlogPostProxy : ProxyBase<IBlogPostService>, IBlogPostService
    {
        public BlogPost Get(int id)
        {
            return Channel.Get(id);
        }

        public BlogPost[] GetAll()
        {
            return Channel.GetAll();
        }

        public BlogPost Update(BlogPost entity)
        {
            return Channel.Update(entity);
        }

        public void Delete(int id)
        {
            Channel.Delete(id);
        }

        public BlogPost[] GetByPage(int page, int pageSize)
        {
            return Channel.GetByPage(page, pageSize);
        }

        public int TotalCount()
        {
            return Channel.TotalCount();
        }
    }
}
