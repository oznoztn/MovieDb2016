using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Contracts
{
    [ServiceContract]
    public interface ICountyService
    {
        [OperationContract]
        County Get(int entityId);

        [OperationContract]
        County[] GetAll();

        [OperationContract]
        County[] GetCountiesByState(int stateId);

        [OperationContract]
        County Update(County entity);

        [OperationContract]
        void Delete(int entityId);

        [OperationContract]
        County[] GetByPage(int page, int pageSize);

        [OperationContract]
        int TotalCount();
    }
}
