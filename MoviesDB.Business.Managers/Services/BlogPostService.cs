using System.ServiceModel;
using MoviesDB.Business.Contracts;
using MoviesDB.Business.Entities;

namespace MoviesDB.Business.Services.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class BlogPostService : ServiceBase, IBlogPostService
    {
        public BlogPost Get(int entityId)
        {
            throw new System.NotImplementedException();
        }

        public BlogPost[] GetAll()
        {
            throw new System.NotImplementedException();
        }

        public BlogPost Update(BlogPost entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int entityId)
        {
            throw new System.NotImplementedException();
        }

        public BlogPost[] GetByPage(int page, int pageSize)
        {
            throw new System.NotImplementedException();
        }

        public int TotalCount()
        {
            throw new System.NotImplementedException();
        }
    }
}
