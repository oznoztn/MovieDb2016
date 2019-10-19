using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
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
    public class CountyService : ServiceBase, ICountyService
    {
        [Import]
        IDataRepositoryFactory _dataRepositoryFactory;

        public County Get(int entityId)
        {
            var countyRepository = _dataRepositoryFactory.GetDataRepository<ICountyRepository>();

            return countyRepository.Get(entityId);
        }

        public County[] GetAll()
        {
            var countyRepository = _dataRepositoryFactory.GetDataRepository<ICountyRepository>();

            return countyRepository.Get().ToArray();
        }

        public County[] GetCountiesByState(int stateId)
        {
            var countyRepository = _dataRepositoryFactory.GetDataRepository<ICountyRepository>();

            return countyRepository.GetCountiesByState(stateId).ToArray();
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public County Update(County entity)
        {
            throw new NotImplementedException();
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }

        public County[] GetByPage(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public int TotalCount()
        {
            throw new NotImplementedException();
        }
    }
}
