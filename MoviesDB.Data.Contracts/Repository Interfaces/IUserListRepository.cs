using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using MoviesDB.Business.Entities;

namespace MoviesDB.Data.Contracts
{
    public interface IUserListRepository : IDataRepository<UserList>
    {
        IEnumerable<UserList> GetByPage(int page, int pageSize);
        IEnumerable<UserList> GetByUserId(int userId);
        UserList GetByListId(int listId);
    }
}
