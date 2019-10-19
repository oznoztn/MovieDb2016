using System.ServiceModel;
using Core.Common.Contracts;
using MoviesDB.Client.Entities;
using MoviesDB.Client.Entities.DTOs;

namespace MoviesDB.Client.Contracts
{
    [ServiceContract]
    public interface IActorService : IServiceContract
    {
        [OperationContract]
        Actor Get(int entityId);

        [OperationContract]
        Actor[] GetAll();

        [OperationContract]
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
