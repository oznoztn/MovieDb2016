using System.ServiceModel;
using MoviesDB.Business.Contracts;
using MoviesDB.Business.Entities;

namespace MoviesDB.Business.Services.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class UserListRecordService : ServiceBase, IUserListRecordService
    {
        public UserListRecord Get(int entityId)
        {
            throw new System.NotImplementedException();
        }

        public UserListRecord[] GetAll()
        {
            throw new System.NotImplementedException();
        }

        public UserListRecord Update(UserListRecord entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int entityId)
        {
            throw new System.NotImplementedException();
        }

        public UserListRecord[] GetByPage(int page, int pageSize)
        {
            throw new System.NotImplementedException();
        }

        public int TotalCount()
        {
            throw new System.NotImplementedException();
        }
    }
}
