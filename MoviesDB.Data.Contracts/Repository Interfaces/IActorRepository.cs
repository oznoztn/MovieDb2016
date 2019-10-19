using System.Collections.Generic;
using Core.Common.Contracts;
using MoviesDB.Business.Entities;
using MoviesDB.Business.Entities.DTOs;

namespace MoviesDB.Data.Contracts
{
    public interface IActorRepository : IDataRepository<Actor>
    {
        IEnumerable<Actor> GetByPage(int page, int pageSize);
        int TotalCount();
        
        ActorDetailsData GetDetails(int actorId);

        IEnumerable<ActorData> ActorsForDropdownList();
        IEnumerable<ActorData> FindByName(string actorNameOrLastName);
        
        ActorCreationData Add(ActorCreationData entity);
        ActorCreationData UpdateSec(ActorCreationData entity);
        ActorCreationData GetSec(int id);

        StatsMovieGenre[] Statistics_TopXActor(int topX);
    }
}
