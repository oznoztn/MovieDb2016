using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using MoviesDB.Business.Contracts;
using MoviesDB.Business.Entities;
using MoviesDB.Data.Contracts;

namespace MoviesDB.Business.Services.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                    ConcurrencyMode = ConcurrencyMode.Multiple,
                    ReleaseServiceInstanceOnTransactionComplete = false)]
    public class StateService : ServiceBase, IStateService
    {
        [Import]
        IDataRepositoryFactory _dataRepositoryFactory;

        public State Get(int entityId)
        {
            var stateRepository = _dataRepositoryFactory.GetDataRepository<IStateRepository>();

            return stateRepository.Get(entityId);
        }

        public State[] GetAll()
        {
            var stateRepository = _dataRepositoryFactory.GetDataRepository<IStateRepository>();

            return stateRepository.Get().ToArray();
        }

        public State[] GetStatesByCountry(int countryId)
        {
            var stateRepository = _dataRepositoryFactory.GetDataRepository<IStateRepository>();

            return stateRepository.GetStatesByCountry(countryId);
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public State Update(State entity)
        {
            throw new NotImplementedException();
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }

        public State[] GetByPage(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public int TotalCount()
        {
            throw new NotImplementedException();
        }
    }
}
