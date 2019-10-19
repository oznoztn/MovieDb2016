using System.Collections.Generic;
using System.ComponentModel.Composition;
using Core.Common.Proxy;
using MoviesDB.Client.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Proxies
{
    [Export(typeof(IUserListService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserListProxy : ProxyBase<IUserListService>, IUserListService
    {
        public UserList Get(int id)
        {
            return Channel.Get(id);
        }

        public UserList[] GetAll()
        {
            return Channel.GetAll();
        }

        public UserList Update(UserList entity)
        {
            return Channel.Update(entity);
        }

        public void Delete(int id)
        {
            Channel.Delete(id);
        }

        public IEnumerable<UserList> GetUserListsById(int userId)
        {
            return Channel.GetUserListsById(userId);
        }

        public IEnumerable<UserList> GetUserListsByEmail(string email)
        {
            return Channel.GetUserListsByEmail(email);
        }

        public UserList GetUserList(int listId)
        {
            return Channel.GetUserList(listId);
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
