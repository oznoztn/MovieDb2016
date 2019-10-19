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
    [Export(typeof(IStateRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StateRepository : DataRepositoryBase<State>, IStateRepository
    {
        protected override State AddEntity(MovieDbContext entityContext, State entity)
        {
            return entityContext.StateSet.Add(entity);
        }

        protected override State UpdateEntity(MovieDbContext entityContext, State entity)
        {
            return (from s in entityContext.StateSet where s.Id == entity.Id select s).FirstOrDefault();    
        }

        protected override State GetEntity(MovieDbContext entityContext, int id)
        {
            return (from s in entityContext.StateSet where s.Id == id select s).FirstOrDefault();            
        }

        protected override IEnumerable<State> GetEntities(MovieDbContext entityContext)
        {
            return entityContext.StateSet.ToList();
        }

        public State[] GetStatesByCountry(int countryId)
        {
            using (var entityContext = new MovieDbContext())
            {
                return (from state in entityContext.StateSet where state.CountryId == countryId select state).ToArray();
            }
        }
    }
}
