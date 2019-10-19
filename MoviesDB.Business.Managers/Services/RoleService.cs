using MoviesDB.Business.Contracts;
using System.ServiceModel;
using MoviesDB.Business.Entities;

namespace MoviesDB.Business.Services.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class RoleService : ServiceBase, IRoleService
    {
        public Role Get(int entityId)
        {
            throw new System.NotImplementedException();
        }

        public Role[] GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Role Update(Role entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int entityId)
        {
            throw new System.NotImplementedException();
        }
    }
}
