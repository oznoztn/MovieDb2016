using System.ComponentModel.Composition;
using System.Linq;
using MoviesDB.Business.Contracts;
using System.ServiceModel;
using System.Web.Script.Serialization;
using MoviesDB.Business.Entities;
using MoviesDB.Business.Entities.DTOs;
using MoviesDB.Data.Contracts;

namespace MoviesDB.Business.Services.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ActorService : ServiceBase, IActorService
    {
        [Import] 
        IActorRepository _actorRepository;

        public Actor Get(int entityId)
        {
            return _actorRepository.Get(entityId);
        }

        public Actor[] GetAll()
        {
            return _actorRepository.Get().ToArray();
        }

        public ActorDetailsData GetDetails(int actorId)
        {
            return _actorRepository.GetDetails(actorId);
        }

        public Actor Update(Actor entity)
        {
            return _actorRepository.Update(entity);
        }

        public void Delete(int entityId)
        {
            _actorRepository.Remove(entityId);
        }

        public Actor[] GetByPage(int page, int pageSize)
        {
            return _actorRepository.GetByPage(page, pageSize).ToArray();
        }

        public int TotalCount()
        {
            return _actorRepository.TotalCount();
        }

        public ActorData[] ActorsForDropdownList()
        {
            return _actorRepository.ActorsForDropdownList().ToArray();
        }

        public ActorData[] FindByName(string actorNameOrLastName)
        {
            return
                _actorRepository.FindByName(actorNameOrLastName).ToArray();
        }

        public ActorCreationData Add(ActorCreationData entity)
        {
            return _actorRepository.Add(entity);
        }

        public ActorCreationData UpdateSec(ActorCreationData entity)
        {
            return _actorRepository.UpdateSec(entity);
        }

        public ActorCreationData GetSec(int id)
        {
            return _actorRepository.GetSec(id);
        }

        public string Statistics_TopXActors(int topX)
        {
            var serializer = new JavaScriptSerializer();
            var stats = _actorRepository.Statistics_TopXActor(topX);
            var jsonString = serializer.Serialize(stats);
            return jsonString;
        }
    }
}
