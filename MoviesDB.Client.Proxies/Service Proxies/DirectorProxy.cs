using System.ComponentModel.Composition;
using Core.Common.Proxy;
using MoviesDB.Client.Contracts;
using MoviesDB.Client.Entities;
using MoviesDB.Client.Entities.DTOs;

namespace MoviesDB.Client.Proxies
{
    [Export(typeof(IDirectorService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DirectorProxy : ProxyBase<IDirectorService>, IDirectorService
    {
        public Director Get(int id)
        {
            return Channel.Get(id);
        }

        public Director[] GetAll()
        {
            return Channel.GetAll();
        }

        public DirectorDetailsData GetDetails(int directorId)
        {
            return Channel.GetDetails(directorId);
        }

        public Director Update(Director entity)
        {
            return Channel.Update(entity);
        }

        public void Delete(int id)
        {
            Channel.Delete(id);
        }

        public Director[] GetByPage(int page, int pageSize)
        {
            return Channel.GetByPage(page, pageSize);
        }

        public int TotalCount()
        {
            return Channel.TotalCount();
        }

        public DirectorData[] DirectorsForDropdownList()
        {
            return Channel.DirectorsForDropdownList();
        }

        public DirectorData[] FindByName(string directorNameOrLastName)
        {
            return Channel.FindByName(directorNameOrLastName);
        }

        public string Statistics_TopXDirectors(int topX)
        {
            return Channel.Statistics_TopXDirectors(topX);
        }
    }
}
