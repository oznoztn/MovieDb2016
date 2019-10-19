using System.ComponentModel.Composition;
using Core.Common.Proxy;
using MoviesDB.Client.Contracts;
using MoviesDB.Client.Entities;
using MoviesDB.Client.Entities.DTOs;

namespace MoviesDB.Client.Proxies
{
    [Export(typeof(IActorService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ActorProxy : ProxyBase<IActorService>, IActorService
    {
        public Actor Get(int entityId)
        {
            return Channel.Get(entityId);
        }

        public Actor[] GetAll()
        {
            return Channel.GetAll();
        }

        public ActorDetailsData GetDetails(int actorId)
        {
            return Channel.GetDetails(actorId);
        }

        public Actor Update(Actor entity)
        {
            return Channel.Update(entity);
        }

        public void Delete(int movieId)
        {
            Channel.Delete(movieId);
        }

        public Actor[] GetByPage(int page, int pageSize)
        {
            return Channel.GetByPage(page, pageSize);
        }

        public int TotalCount()
        {
            return Channel.TotalCount();
        }

        public ActorData[] ActorsForDropdownList()
        {
            return Channel.ActorsForDropdownList();
        }

        public ActorData[] FindByName(string actorNameOrLastName)
        {
            return Channel.FindByName(actorNameOrLastName);
        }

        public ActorCreationData Add(ActorCreationData entity)
        {
            return Channel.Add(entity);
        }

        public ActorCreationData UpdateSec(ActorCreationData entity)
        {
            return Channel.UpdateSec(entity);
        }

        public ActorCreationData GetSec(int id)
        {
            return Channel.GetSec(id);
        }

        public string Statistics_TopXActors(int topX)
        {
            return Channel.Statistics_TopXActors(topX);
        }
    }
}
