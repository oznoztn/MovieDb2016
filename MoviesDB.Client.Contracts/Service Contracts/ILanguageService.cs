using System.ServiceModel;
using Core.Common.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Contracts
{
    [ServiceContract]
    public interface ILanguageService : IServiceContract
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