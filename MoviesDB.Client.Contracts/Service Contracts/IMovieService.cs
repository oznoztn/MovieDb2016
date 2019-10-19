using System;
using System.ServiceModel;
using Core.Common.Contracts;
using Core.Common.Exceptions;
using MoviesDb.Common;
using MoviesDB.Client.Entities;
using MoviesDB.Client.Entities.DTOs;

namespace MoviesDB.Client.Contracts
{
    [ServiceContract]
    public interface IMovieService : IServiceContract
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
        MovieData GetMovieByImdbId(string imdbId);
        
        [OperationContract]        
        Movie[] GetAll();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MovieDetailsData GetDetails(int movieId);

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

        //[OperationContract]
        //MovieInfoData[] GetByPage_Joined(int page, int pageSize);

    }
}

// WCF datayı eninde sonunda array'a çevireceğinden array olarak tanımlandı
// Bir başka neden ise her platform list of T ifadesinin ne olduğunu bilmeyebilir.


/*
 * Because these are output operations, output in the context of I/O, input/output, they would require transaction handling. 
 * Now, to be specific, transaction handling would probably not be required in this case because 
 * I'm really only doing one database update. 
 * Transactions are important when you're doing more than one database update so that 
 * if one piece of it fails, everything is rolled back, and the system remains in a consistent state.
 * 
 * Output Operations -> Non-fetch operations (yani veri döndermeyen (?))
 */