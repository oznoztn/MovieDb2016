using System.ComponentModel.Composition;
using Core.Common.Proxy;
using MoviesDB.Client.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Proxies
{
    [Export(typeof(IPollService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PollProxy : ProxyBase<IPollService>, IPollService
    {
        public Poll Get(int id)
        {
            return Channel.Get(id);
        }

        public Poll[] GetAll()
        {
            return Channel.GetAll();
        }

        public Poll Update(Poll entity)
        {
            return Channel.Update(entity);
        }

        public void Delete(int id)
        {
            Channel.Delete(id);
        }

        public Poll[] GetByPage(int page, int pageSize)
        {
            return Channel.GetByPage(page, pageSize);
        }

        public int TotalCount()
        {
            return Channel.TotalCount();
        }
    }
}
