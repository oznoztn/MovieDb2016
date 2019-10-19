using System.Collections.Generic;
using Core.Common.Contracts;
using MoviesDB.Business.Entities;
using MoviesDB.Business.Entities.DTOs;

namespace MoviesDB.Data.Contracts
{
    public interface IMovieRepository : IDataRepository<Movie>
    {
        MovieCreationData Add(MovieCreationData movieCreationData);
        MovieCreationData UpdateSec(MovieCreationData movieCreationData);
        MovieDetailsData GetDetails(int movieId);
        MovieCreationData GetSec(int movieId);

        IEnumerable<MovieData> GetByPage(int page, int pageSize);
        IEnumerable<Movie> GetMoviesByActorId(int actorId);
        IEnumerable<Movie> GetMoviesByDirectorId(int directorId);
        IEnumerable<MovieData> GetMoviesByActorId_Simplified(int actorId);
        IEnumerable<MovieData> GetMoviesByDirectorId_Simplified(int directorId);

        StatsMovieYear[] Statistics_MoviesByYear();
        StatsMovieGenre[] Statistics_MoviesByGenre();

        MovieData[] SearchMovie(string name);
        MovieData GetMovieDataByImdbId(string imdbId);
        MovieData UpdateSimple(MovieData movieData);

        int TotalCount();
    }
}
