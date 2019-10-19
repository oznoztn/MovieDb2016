using System.ComponentModel.Composition;
using Core.Common.Proxy;
using MoviesDB.Client.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Proxies
{
    [Export(typeof(ICommentService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CommentProxy : ProxyBase<ICommentService>, ICommentService
    {
        public Comment Get(int id)
        {
            return Channel.Get(id);
        }

        public Comment[] GetAll()
        {
            return Channel.GetAll();
        }

        public Comment Update(Comment entity)
        {
            return Channel.Update(entity);
        }

        public void Delete(int id)
        {
            Channel.Delete(id);
        }

        public Comment[] GetByPage(int page, int pageSize)
        {
            return Channel.GetByPage(page, pageSize);
        }

        public int TotalCount()
        {
            return Channel.TotalCount();
        }
    }
}
