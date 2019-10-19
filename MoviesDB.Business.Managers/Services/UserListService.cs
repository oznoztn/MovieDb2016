using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ServiceModel;
using System.Threading;
using Core.Common.Contracts;
using Core.Common.Exceptions;
using MoviesDB.Business.Contracts;
using MoviesDB.Business.Entities;
using MoviesDb.Common;
using MoviesDB.Data.Contracts;

namespace MoviesDB.Business.Services.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class UserListService : ServiceBase, IUserListService
    {
        [Import]
        private IDataRepositoryFactory _repositoryFactory;

        public UserList Get(int entityId)
        {
            throw new System.NotImplementedException();
        }

        public UserList[] GetAll()
        {
            throw new System.NotImplementedException();
        }

        public UserList Update(UserList entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int entityId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<UserList> GetUserListsById(int userId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IUserRepository userRepository = _repositoryFactory.GetDataRepository<IUserRepository>();
                IRoleRepository roleRepository = _repositoryFactory.GetDataRepository<IRoleRepository>();
                IUserListRepository userListRepository = _repositoryFactory.GetDataRepository<IUserListRepository>();

                User userAccount = userRepository.Get(userId);
                if (userAccount == null)
                {
                    NotFoundException ex =
                        new NotFoundException(string.Format("{0} kimlik numarasına sahip herhangi bir hesap bulunamadı!", userId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
                ValidateAuthorization(userAccount);

                IEnumerable<UserList> userLists = userListRepository.GetByUserId(userAccount.Id);

                return userLists;
            });
            
        }

        public IEnumerable<UserList> GetUserListsByEmail(string loginEmail)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IUserRepository userRepository = _repositoryFactory.GetDataRepository<IUserRepository>();
                IRoleRepository roleRepository = _repositoryFactory.GetDataRepository<IRoleRepository>();
                IUserListRepository userListRepository = _repositoryFactory.GetDataRepository<IUserListRepository>();

                User userAccount = userRepository.GetByEmail(loginEmail);
                if (userAccount == null)
                {
                    NotFoundException ex =
                        new NotFoundException(string.Format("{0} için herhangi bir hesap bulunamadı!", loginEmail));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
                ValidateAuthorization(userAccount);
                
                IEnumerable<UserList> userLists = userListRepository.GetByUserId(userAccount.Id);
                return userLists;
            });
        }

        public UserList GetUserList(int listId)
        {
            throw new System.NotImplementedException();
        }

        public UserList[] GetByPage(int page, int pageSize)
        {
            throw new System.NotImplementedException();
        }

        public int TotalCount()
        {
            throw new System.NotImplementedException();
        }
    }
}
