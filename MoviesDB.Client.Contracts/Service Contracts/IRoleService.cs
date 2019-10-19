using System.ServiceModel;
using Core.Common.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Contracts
{    
    [ServiceContract]
    public interface IRoleService : IServiceContract
    {
        [OperationContract]
        Role Get(int entityId);

        [OperationContract]
        Role[] GetAll();

        [OperationContract]
        Role Update(Role entity);

        [OperationContract]
        void Delete(int entityId);
    }
}
