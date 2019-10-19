using System;
using System.ServiceModel;
using MoviesDB.Business.Contracts;
using MoviesDB.Business.Entities;

namespace MoviesDB.Business.Services.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class NewsMappingService : ServiceBase, INewsMappingService
    {
        public NewsMapping Get(int entityId)
        {
            throw new System.NotImplementedException();
        }

        public NewsMapping[] GetAll()
        {
            throw new System.NotImplementedException();
        }

        public NewsMapping Update(NewsMapping entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int entityId)
        {
            throw new System.NotImplementedException();
        }

        public NewsMapping[] GetByPage(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public int TotalCount()
        {
            throw new NotImplementedException();
        }
    }
}
