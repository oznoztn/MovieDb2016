using System.ServiceModel;
using MoviesDB.Business.Contracts;
using MoviesDB.Business.Entities;

namespace MoviesDB.Business.Services.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ReviewService : ServiceBase, IReviewService
    {
        public Review Get(int entityId)
        {
            throw new System.NotImplementedException();
        }

        public Review[] GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Review Update(Review entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int entityId)
        {
            throw new System.NotImplementedException();
        }

        public Review[] GetByPage(int page, int pageSize)
        {
            throw new System.NotImplementedException();
        }

        public int TotalCount()
        {
            throw new System.NotImplementedException();
        }
    }
}
