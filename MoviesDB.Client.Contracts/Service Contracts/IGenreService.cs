using System.Collections.Generic;
using System.ServiceModel;
using Core.Common.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Contracts
{
    [ServiceContract]
    public interface IGenreService : IServiceContract
    {
        [OperationContract]
        Genre Get(int genreId);

        [OperationContract]
        Genre[] GetAll();

        [OperationContract]
        Genre[] GetAllSubs();

        [OperationContract]
        Genre[] FindByName(string genreName);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Genre Update(Genre genre);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void Delete(int genreId);

        [OperationContract]
        Genre[] GetByPage(int page, int pageSize);

        [OperationContract]
        int TotalCount();
    }
}
