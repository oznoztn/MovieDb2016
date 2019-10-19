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
    [Export(typeof(IUserListRecordRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserListRecordRepository : DataRepositoryBase<UserListRecord>, IUserListRecordRepository
    {
        protected override UserListRecord AddEntity(MovieDbContext entityContext, UserListRecord entity)
        {
            return entityContext.UserListRecordSet.Add(entity);
        }

        protected override UserListRecord UpdateEntity(MovieDbContext entityContext, UserListRecord entity)
        {
            return (from e in entityContext.UserListRecordSet
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override UserListRecord GetEntity(MovieDbContext entityContext, int id)
        {
            return (from e in entityContext.UserListRecordSet
                    where e.Id == id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<UserListRecord> GetEntities(MovieDbContext entityContext)
        {
            return entityContext.UserListRecordSet.ToList();
        }
    }
}
