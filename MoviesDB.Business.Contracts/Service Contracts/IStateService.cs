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
    public interface IStateService
    {
        [OperationContract]
        State Get(int entityId);

        [OperationContract]
        State[] GetAll();

        [OperationContract]
        State[] GetStatesByCountry(int countryId);

        [OperationContract]
        State Update(State entity);

        [OperationContract]
        void Delete(int entityId);

        [OperationContract]
        State[] GetByPage(int page, int pageSize);

        [OperationContract]
        int TotalCount();
    }
}
