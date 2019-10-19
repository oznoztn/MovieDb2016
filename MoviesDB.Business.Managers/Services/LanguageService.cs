using Core.Common.Contracts;
using MoviesDB.Business.Contracts;
using MoviesDB.Business.Entities;
using MoviesDB.Data.Contracts;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;

namespace MoviesDB.Business.Services.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]
    public class LanguageService : ServiceBase, ILanguageService
    {
        [Import]
        ILanguageRepository _languageRepository;

        public Language Get(int entityId)
        {
            return _languageRepository.Get(entityId);
        }

        public Language[] GetAll()
        {
            return _languageRepository.Get().ToArray();
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public Language Update(Language language)
        {
            Language updatedLanguage = null;
            updatedLanguage = language.Id == 0 ? _languageRepository.Add(language) : _languageRepository.Update(language);

            return updatedLanguage;
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void Delete(int entityId)
        {
            _languageRepository.Remove(entityId);
        }

        public Language[] GetByPage(int page, int pageSize)
        {
            return _languageRepository.GetByPage(page, pageSize).ToArray();
        }

        public int TotalCount()
        {
            return _languageRepository.TotalCount();
        }
    }
}
