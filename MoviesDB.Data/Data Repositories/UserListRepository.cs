using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesDB.Business.Entities;
using MoviesDB.Data.Contracts;

namespace MoviesDB.Data
{
    [Export(typeof(IUserListRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserListRepository : DataRepositoryBase<UserList>, IUserListRepository
    {
        protected override UserList AddEntity(MovieDbContext entityContext, UserList entity)
        {
            return entityContext.UserListSet.Add(entity);
        }

        protected override UserList UpdateEntity(MovieDbContext entityContext, UserList entity)
        {
            return (from e in entityContext.UserListSet
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override UserList GetEntity(MovieDbContext entityContext, int id)
        {
            return (from e in entityContext.UserListSet
                    where e.Id == id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<UserList> GetEntities(MovieDbContext entityContext)
        {
            return entityContext.UserListSet.ToList();
        }

        public IEnumerable<UserList> GetByPage(int page, int pageSize)
        {
            using (var entityContext = new MovieDbContext())
            {
                return entityContext.UserListSet.OrderBy(t => t.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public IEnumerable<UserList> GetByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public UserList GetByListId(int listId)
        {
            throw new NotImplementedException();
        }
    }
}
