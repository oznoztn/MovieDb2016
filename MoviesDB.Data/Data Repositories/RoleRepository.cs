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
    [Export(typeof(IRoleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RoleRepository : DataRepositoryBase<Role>, IRoleRepository
    {
        protected override Role AddEntity(MovieDbContext entityContext, Role entity)
        {
            return entityContext.RoleSet.Add(entity);
        }

        protected override Role UpdateEntity(MovieDbContext entityContext, Role entity)
        {
            return (from e in entityContext.RoleSet
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override Role GetEntity(MovieDbContext entityContext, int id)
        {
            return (from e in entityContext.RoleSet
                    where e.Id == id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Role> GetEntities(MovieDbContext entityContext)
        {
            return entityContext.RoleSet.ToList();
        }
    }
}
