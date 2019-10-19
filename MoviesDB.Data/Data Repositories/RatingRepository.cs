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
    [Export(typeof(IRatingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RatingRepository : DataRepositoryBase<Rating>, IRatingRepository
    {
        protected override Rating AddEntity(MovieDbContext entityContext, Rating entity)
        {
            return entityContext.RatingSet.Add(entity);
        }

        protected override Rating UpdateEntity(MovieDbContext entityContext, Rating entity)
        {
            return (from e in entityContext.RatingSet
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override Rating GetEntity(MovieDbContext entityContext, int id)
        {
            return (from e in entityContext.RatingSet
                    where e.Id == id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Rating> GetEntities(MovieDbContext entityContext)
        {
            return entityContext.RatingSet.ToList();
        }
    }
}
