using System.Collections.Generic;
using Core.Common.Contracts;
using MoviesDB.Business.Contracts;
using MoviesDB.Business.Entities;
using MoviesDB.Data.Contracts;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Web.Script.Serialization;
using MoviesDB.Business.Entities.DTOs;

namespace MoviesDB.Business.Services.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class DirectorService : ServiceBase, IDirectorService
    {
        [Import]
        IDirectorRepository _directorRepository;
      
        // WCF-Constructor
        public DirectorService()
        {
        }

        public Director Get(int directorId)
        {
            return _directorRepository.Get(directorId);
        }

        public Director[] GetAll()
        {
            Director[] directors = _directorRepository.Get().ToArray();
            
            return directors;
        }

        public DirectorDetailsData GetDetails(int directorId)
        {
            return _directorRepository.GetDetails(directorId);
        }

        public Director Update(Director director)
        {
            Director updatedDirector = null;
            updatedDirector = director.Id == 0 ? _directorRepository.Add(director) : _directorRepository.Update(director);

            return updatedDirector;
        }

        public void Delete(int directorId)
        {
            _directorRepository.Remove(directorId);
        }

        public Director[] GetByPage(int page, int pageSize)
        {

            return _directorRepository.GetByPage(page, pageSize).ToArray();
        }

        public int TotalCount()
        {
            return _directorRepository.TotalCount();
        }

        public DirectorData[] DirectorsForDropdownList()
        {
            List<DirectorData> result = new List<DirectorData>();
            IEnumerable<DirectorData> incoming = _directorRepository.DirectorsForDropdownList();

            foreach (var directorInfo in incoming)
            {
                result.Add(new DirectorData(){ Name = directorInfo.Name, Id = directorInfo.Id});
            }

            return result.ToArray();
        }

        public DirectorData[] FindByName(string directorNameOrLastName)
        {
            IEnumerable<DirectorData> incoming = _directorRepository.FindByName(directorNameOrLastName);
            return incoming.Select(directorInfo => new DirectorData() {Name = directorInfo.Name, Id = directorInfo.Id}).ToArray();
        }

        public string Statistics_TopXDirectors(int topX)
        {
            var serializer = new JavaScriptSerializer();
            var stats = _directorRepository.Statistics_TopXDirectors(topX);
            var jsonString = serializer.Serialize(stats);
            return jsonString;
        }
    }
}
