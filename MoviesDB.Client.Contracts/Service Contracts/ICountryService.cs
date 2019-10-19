using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Contracts
{
    [ServiceContract]
    public interface ICountryService : IServiceContract
    {
        [OperationContract]
        Country Get(int entityId);

        [OperationContract]
        Country[] GetAll();

        [OperationContract]
        Country Update(Country entity);

        [OperationContract]
        void Delete(int entityId);

        [OperationContract]
        Country[] GetByPage(int page, int pageSize);

        [OperationContract]
        int TotalCount();

        [OperationContract]
        Country[] SearchCountry(string searchTerm);

        [OperationContract]
        string Statistics();
    }
}
