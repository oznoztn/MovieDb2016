using System.ServiceModel;
using MoviesDB.Business.Entities;

namespace MoviesDB.Business.Contracts
{
    [ServiceContract]
    public interface ILanguageService
    {
        [OperationContract]
        Language Get(int entityId);

        [OperationContract]
        Language[] GetAll();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Language Update(Language entity);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void Delete(int entityId);

        [OperationContract]
        Language[] GetByPage(int page, int pageSize);

        [OperationContract]
        int TotalCount();
    }
}