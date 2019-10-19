using System.ServiceModel;
using MoviesDB.Business.Entities;

namespace MoviesDB.Business.Contracts
{    
    [ServiceContract]
    public interface IRoleService
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
