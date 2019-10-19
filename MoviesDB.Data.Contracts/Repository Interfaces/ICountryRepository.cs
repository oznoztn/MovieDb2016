using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using MoviesDB.Business.Entities;
using MoviesDB.Business.Entities.DTOs;

namespace MoviesDB.Data.Contracts
{    
    public interface ICountryRepository : IDataRepository<Country>
    {
        IEnumerable<Country> SearchCountry(string searchTerm);
        IEnumerable<Country> GetByPage(int page, int pageSize);
        int TotalCount();
        StatsMovieGenre[] Statistics();
    }
}
