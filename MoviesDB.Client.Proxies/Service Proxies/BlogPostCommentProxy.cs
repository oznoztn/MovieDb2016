using System.ComponentModel.Composition;
using Core.Common.Proxy;
using MoviesDB.Client.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Proxies
{
    [Export(typeof(IBlogPostCommentService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BlogPostCommentProxy : ProxyBase<IBlogPostCommentService>, IBlogPostCommentService
    {
        public BlogPostComment Get(int id)
        {
            return Channel.Get(id);
        }

        public BlogPostComment[] GetAll()
        {
            return Channel.GetAll();
        }

        public BlogPostComment Update(BlogPostComment entity)
        {
            return Channel.Update(entity);
        }

        public void Delete(int id)
        {
            Channel.Delete(id);
        }

        public BlogPostComment[] GetByPage(int page, int pageSize)
        {
            return Channel.GetByPage(page, pageSize);
        }

        public int TotalCount()
        {
            return Channel.TotalCount();
        }
    }
}
