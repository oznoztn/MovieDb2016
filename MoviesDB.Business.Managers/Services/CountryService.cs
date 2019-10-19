using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Core.Common.Contracts;
using MoviesDB.Business.Contracts;
using MoviesDB.Business.Entities;
using MoviesDB.Business.Entities.DTOs;
using MoviesDB.Data.Contracts;

namespace MoviesDB.Business.Services.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                    ConcurrencyMode = ConcurrencyMode.Multiple,
                    ReleaseServiceInstanceOnTransactionComplete = false)]
    public class CountryService : ServiceBase, ICountryService
    {
        [Import]
        IDataRepositoryFactory _dataRepositoryFactory;        

        public Country Get(int entityId)
        {
            var countryRepository = _dataRepositoryFactory.GetDataRepository<ICountryRepository>();

            return countryRepository.Get(entityId);
        }

        public Country[] GetAll()
        {
            var countryRepository = _dataRepositoryFactory.GetDataRepository<ICountryRepository>();

            return countryRepository.Get().ToArray();
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public Country Update(Country entity)
        {
            return _dataRepositoryFactory.GetDataRepository<ICountryRepository>().Update(entity);
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void Delete(int entityId)
        {
            _dataRepositoryFactory.GetDataRepository<ICountryRepository>().Remove(entityId);
        }

        public Country[] GetByPage(int page, int pageSize)
        {
            return _dataRepositoryFactory.GetDataRepository<ICountryRepository>().GetByPage(page, pageSize).ToArray();
        }

        public int TotalCount()
        {
            return _dataRepositoryFactory.GetDataRepository<ICountryRepository>().TotalCount();
        }

        public Country[] SearchCountry(string searchTerm)
        {
            return _dataRepositoryFactory.GetDataRepository<ICountryRepository>()
                .SearchCountry(searchTerm).ToArray();
        }

        public string Statistics()
        {
            var rep = _dataRepositoryFactory.GetDataRepository<ICountryRepository>();
            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(rep.Statistics().Select(t => new {title = t.Genre, value = t.Count}));
            return json;
        }
    }
}
