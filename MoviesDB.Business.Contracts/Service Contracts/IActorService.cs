using System.ServiceModel;
using Core.Common.Exceptions;
using MoviesDB.Business.Entities;
using MoviesDB.Business.Entities.DTOs;

namespace MoviesDB.Business.Contracts
{
    [ServiceContract]
    public interface IActorService
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Actor Get(int entityId);

        [OperationContract]
        Actor[] GetAll();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ActorDetailsData GetDetails(int actorId);

        [OperationContract]
        Actor Update(Actor entity);

        [OperationContract]
        void Delete(int entityId);

        [OperationContract]
        Actor[] GetByPage(int page, int pageSize);

        [OperationContract]
        int TotalCount();

        [OperationContract]
        ActorData[] ActorsForDropdownList();

        [OperationContract]
        ActorData[] FindByName(string actorNameOrLastName);

        [OperationContract]
        ActorCreationData Add(ActorCreationData entity);

        [OperationContract]
        ActorCreationData UpdateSec(ActorCreationData entity);

        [OperationContract]
        ActorCreationData GetSec(int id);

        [OperationContract]
        string Statistics_TopXActors(int topX);

    }
}
