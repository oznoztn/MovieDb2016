using System.ServiceModel;
using Core.Common.Exceptions;
using MoviesDB.Business.Entities;
using MoviesDB.Business.Entities.DTOs;

namespace MoviesDB.Business.Contracts
{
    [ServiceContract]
    public interface IDirectorService
    {
        [OperationContract]
        Director Get(int entityId);

        [OperationContract]
        Director[] GetAll();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
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
