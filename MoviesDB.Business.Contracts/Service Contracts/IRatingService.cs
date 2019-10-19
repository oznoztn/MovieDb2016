using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using MoviesDB.Business.Entities;

namespace MoviesDB.Business.Contracts
{
    [ServiceContract]
    public interface IRatingService
    {
        [OperationContract]
        Rating Get(int entityId);

        [OperationContract]
        Rating[] GetAll();

        [OperationContract]
        Rating Update(Rating entity);

        [OperationContract]
        void Delete(int entityId);

        [OperationContract]
        Rating[] GetByPage(int page, int pageSize);

        [OperationContract]
        int TotalCount();
    }
}
