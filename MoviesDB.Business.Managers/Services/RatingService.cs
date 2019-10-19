
using System.ServiceModel;
using MoviesDB.Business.Contracts;
using MoviesDB.Business.Entities;

namespace MoviesDB.Business.Services.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class RatingService : ServiceBase, IRatingService
    {
        public Rating Get(int entityId)
        {
            throw new System.NotImplementedException();
        }

        public Rating[] GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Rating Update(Rating entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int entityId)
        {
            throw new System.NotImplementedException();
        }

        public Rating[] GetByPage(int page, int pageSize)
        {
            throw new System.NotImplementedException();
        }

        public int TotalCount()
        {
            throw new System.NotImplementedException();
        }
    }
}
