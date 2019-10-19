using Core.Common.Contracts;
using MoviesDB.Business.Contracts;
using MoviesDB.Business.Entities;
using MoviesDB.Data.Contracts;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;

namespace MoviesDB.Business.Services.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]
    public class GenreService : ServiceBase, IGenreService
    {
        [Import]
        IDataRepositoryFactory _repositoryFactory;

        public GenreService()
        {

        }

        public Genre Get(int genreId)
        {
            IGenreRepository genreRepository = _repositoryFactory.GetDataRepository<IGenreRepository>();
            Genre genre = genreRepository.Get(genreId);

            return genre;
        }

        public Genre[] GetAll()
        {
            IGenreRepository genreRepository = _repositoryFactory.GetDataRepository<IGenreRepository>();
            Genre[] genres = genreRepository.Get().ToArray();
            
            return genres;
        }

        public Genre[] GetAllSubs()
        {
            return _repositoryFactory.GetDataRepository<IGenreRepository>().GetAllSubs().ToArray();
        }

        public Genre[] FindByName(string genreName)
        {
            return _repositoryFactory.GetDataRepository<IGenreRepository>().FindByName(genreName);
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public Genre Update(Genre genre)
        {
            IGenreRepository genreRepository = _repositoryFactory.GetDataRepository<IGenreRepository>();

            Genre updatedGenre = null;
            updatedGenre = genre.Id == 0 ? genreRepository.Add(genre) : genreRepository.Update(genre);

            return updatedGenre;
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void Delete(int genreId)
        {
            IGenreRepository genreRepository = _repositoryFactory.GetDataRepository<IGenreRepository>();

            genreRepository.Remove(genreId);
        }

        public Genre[] GetByPage(int page, int pageSize)
        {
            IGenreRepository genreRepository = _repositoryFactory.GetDataRepository<IGenreRepository>();

            return genreRepository.GetByPage(page, pageSize).ToArray();
        }

        public int TotalCount()
        {
            return 22;
        }
    }
}

/*
 * Dikkat edersen MovieService'de olduğu gibi burada AccountService için 
 *      ReleaseServiceInstanceOnTransactionComplete = false
 * Attribute'unu vermedik. 
 * 
 * Burada bunu eklememizin şimdilik gereği yok.
 * Çünkü burada bir tane bile "transaction aware" operation yok.
 * 
 * Burada (bu not yazılırken) sadece GetGenres operasyonu var. 
 * Bu operasyon bir "fetch operation" olduğu için "transaction aware" olması gerekmiyor.
 * 
 * MovieService tarafındaki Update ve Delete birer non-fetch operasyondular.
 * Transaction aware olduklarından üstteki attribute, MovieService'e verilmişti.
 */