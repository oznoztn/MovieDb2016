using System.ComponentModel.Composition;
using Core.Common.Proxy;
using MoviesDB.Client.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Proxies
{
    [Export(typeof(IUserListRecordService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserListEntryProxy : ProxyBase<IUserListRecordService>, IUserListRecordService
    {
        public UserListRecord Get(int id)
        {
            return Channel.Get(id);
        }

        public UserListRecord[] GetAll()
        {
            return Channel.GetAll();
        }

        public UserListRecord Update(UserListRecord entity)
        {
            return Channel.Update(entity);
        }

        public void Delete(int id)
        {
            Channel.Delete(id);
        }

        public Language[] GetByPage(int page, int pageSize)
        {
            return Channel.GetByPage(page, pageSize);
        }

        public int TotalCount()
        {
            return Channel.TotalCount();
        }
    }
}
