using System;
using System.ServiceModel;
using Core.Common.Exceptions;
using MoviesDb.Common;
using MoviesDB.Business.Entities;
using MoviesDB.Business.Entities.DTOs;

namespace MoviesDB.Business.Contracts
{
    [ServiceContract]
    public interface IMovieService
    {
        [OperationContract]
        Movie Get(int movieId);
        
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(UniformException))]
        [FaultContract(typeof(CommunicationException))]
        [FaultContract(typeof(ApplicationException))]
        MovieCreationData GetSec(int movieId);

        [OperationContract]
        MovieData[] SearchMovie(string name);

        [OperationContract]
        MovieData GetMovieDataByImdbId(string imdbId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MovieDetailsData GetDetails(int movieId);

        [OperationContract]        
        Movie[] GetAll();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Movie Update(Movie entity);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MovieCreationData UpdateSec(MovieCreationData entity);

        [OperationContract]
        MovieData UpdateSimple(MovieData movieData);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void Delete(int movieId);

        [OperationContract]
        MovieData[] GetByPage(int page, int pageSize);

        [OperationContract]
        int TotalCount();

        [OperationContract]
        Movie[] GetMoviesByActorId(int id);

        [OperationContract]
        Movie[] GetMoviesByDirectorId(int id);

        [OperationContract]
        string Statistics_MoviesByYear();

        [OperationContract]
        string Statistics_MoviesByGenre();

    }
}
