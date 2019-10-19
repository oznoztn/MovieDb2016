using System.ServiceModel;
using Core.Common.Contracts;
using MoviesDB.Client.Entities;
using MoviesDB.Client.Entities.DTOs;

namespace MoviesDB.Client.Contracts
{
    [ServiceContract]
    public interface IDirectorService : IServiceContract
    {
        [OperationContract]
        Director Get(int entityId);

        [OperationContract]
        Director[] GetAll();

        [OperationContract]
        DirectorDetailsData GetDetails(int directorId);

        [OperationContract]
        Director Update(Director entity);

        [OperationContract]
        void Delete(int entityId);

        [OperationContract]
        Director[] GetByPage(int page, int pageSize);

        [OperationContract]
        int TotalCount();

        [OperationContract]
        DirectorData[] DirectorsForDropdownList();

        [OperationContract]
        DirectorData[] FindByName(string directorNameOrLastName);

        [OperationContract]
        string Statistics_TopXDirectors(int topX);
    }
}
