using System.ComponentModel.Composition;
using Core.Common.Proxy;
using MoviesDB.Client.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Proxies
{
    [Export(typeof(IRoleService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RoleProxy : ProxyBase<IRoleService>, IRoleService
    {
        public Role Get(int id)
        {
            return Channel.Get(id);
        }

        public Role[] GetAll()
        {
            return Channel.GetAll();
        }

        public Role Update(Role entity)
        {
            return Channel.Update(entity);
        }

        public void Delete(int id)
        {
            Channel.Delete(id);
        }
    }
}
