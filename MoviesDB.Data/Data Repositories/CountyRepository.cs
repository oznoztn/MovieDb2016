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
    [Export(typeof(ICountyRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CountyRepository : DataRepositoryBase<County>, ICountyRepository
    {
        protected override County AddEntity(MovieDbContext entityContext, County entity)
        {
            return entityContext.CountySet.Add(entity);
        }

        protected override County UpdateEntity(MovieDbContext entityContext, County entity)
        {
            return (from c in entityContext.CountySet where c.Id == entity.Id select c).FirstOrDefault();
        }

        protected override County GetEntity(MovieDbContext entityContext, int id)
        {
            return (from c in entityContext.CountySet where c.Id == id select c).FirstOrDefault();
        }

        protected override IEnumerable<County> GetEntities(MovieDbContext entityContext)
        {
            return entityContext.CountySet.ToList();
        }

        public IEnumerable<County> GetCountiesByState(int stateId)
        {
            using (var entityContext = new MovieDbContext())
            {
                return (from county in entityContext.CountySet where county.StateId == stateId select county).ToList();
            }
        }
    }
}
