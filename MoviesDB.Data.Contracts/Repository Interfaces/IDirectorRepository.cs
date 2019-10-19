using System.Collections.Generic;
using Core.Common.Contracts;
using MoviesDB.Business.Entities;
using MoviesDB.Business.Entities.DTOs;

namespace MoviesDB.Data.Contracts
{
    public interface IDirectorRepository : IDataRepository<Director>
    {
        int TotalCount();
        IEnumerable<Director> GetByPage(int page, int pageSize);
        DirectorDetailsData GetDetails(int directorId);
        IEnumerable<DirectorData> FindByName(string directorNameOrLastName);
        IEnumerable<DirectorData> DirectorsForDropdownList();

        StatsMovieGenre[] Statistics_TopXDirectors(int topX);

    }
}
