using Core.Common.Contracts;
using Core.Common.Exceptions;
using MoviesDB.Business.Entities;
using MoviesDb.Common;
using MoviesDB.Business.Contracts;
using MoviesDB.Data.Contracts;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel;
using System.Threading;
using System.Web.Script.Serialization;
using MoviesDB.Business.Entities.DTOs;

namespace MoviesDB.Business.Services.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, // her bir call için ayrı service instance'ı
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]
    public class MovieService : ServiceBase, IMovieService
    {
        [Import]
        IDataRepositoryFactory _dataRepositoryFactory;

        [Import] 
        IBusinessEngineFactory _businessEngineFactory;

        // The only constructor WCF cares about is the default parameterless constructor.
        public MovieService()
        {
            // This is the code that tells MEF to resolve the dep. of this class after the class's been constructed.
            // Moved to ManagerBase class -> Tüm servislerde ortak olduğu için.             
            // ObjectBase.Container.SatisfyImportsOnce(this);
        }

        #region Unit Test Constructors

        // WCF does not care about these constructors below. It doesn't even know that they are here.
        /// <summary>
        /// This constructor for unit-testing purposes only.
        /// </summary>
        /// <param name="dataRepositoryFactory"></param>
        public MovieService(IDataRepositoryFactory dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
        }

        /// <summary>
        /// This constructor for unit-testing purposes only.
        /// </summary>
        /// <param name="businessEngineFactory"></param>
        public MovieService(IBusinessEngineFactory businessEngineFactory)
        {
            _businessEngineFactory = businessEngineFactory;
        }

        /// <summary>
        /// This constructor for unit-testing purposes only.
        /// </summary>
        /// <param name="businessEngineFactory"></param>
        /// <param name="dataRepositoryFactory"></param>
        public MovieService(IBusinessEngineFactory businessEngineFactory, IDataRepositoryFactory dataRepositoryFactory)
        {
            _businessEngineFactory = businessEngineFactory;
            _dataRepositoryFactory = dataRepositoryFactory;
        }

        #endregion

        // BaseService class'ındaki metodu override ediyoruz.
        protected override User LoadAuthorizationValidationAccount(string loginName)
        {
            IUserRepository userRepository = _dataRepositoryFactory.GetDataRepository<IUserRepository>();
            User authAccount = userRepository.GetByEmail(loginName);

            if (authAccount == null)
            {
                NotFoundException ex = new NotFoundException(string.Format("{0} isminde kullanıcı bulunamadı!", loginName));
                throw new FaultException<NotFoundException>(ex, ex.Message);
            }
            return authAccount;
        }

        //[PrincipalPermission(SecurityAction.Demand, Role = Roles.Admin)]
        //[PrincipalPermission(SecurityAction.Demand, Name = Roles.User)]
        public Movie Get(int movieId)
        {
            try
            {
                IMovieRepository movieRepository = 
                    _dataRepositoryFactory.GetDataRepository<IMovieRepository>();             

                Movie movieEntity = movieRepository.Get(movieId);
                if (movieEntity == null)
                {
                    NotFoundException ex = new NotFoundException(
                        string.Format("There's no movie for given Id:{0}", movieId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
                return movieEntity;
            }
            catch (FaultException ex)
            {
                // i'm just gonna re-throw it.
                // Dikkat et: Bu blok yukarda FaultException<T> kullanıldığı için var.
                // FaultException<NotFoundException> kullanılmasaydı bu catch bloğu kullanılmayacaktı.
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public MovieData[] SearchMovie(string name)
        {
            IMovieRepository movieRepository = _dataRepositoryFactory.GetDataRepository<IMovieRepository>();

            var result = movieRepository.SearchMovie(name);

            return result;
        }

        public MovieData GetMovieDataByImdbId(string imdbId)
        {
            IMovieRepository movieRepository = _dataRepositoryFactory.GetDataRepository<IMovieRepository>();

            var result = movieRepository.GetMovieDataByImdbId(imdbId);

            return result;
        }

        public MovieCreationData GetSec(int movieId)
        {
            try
            {
                MovieCreationData movie =
                    _dataRepositoryFactory.GetDataRepository<IMovieRepository>().GetSec(movieId);

                if (movie.Name == null)
                {
                    var movieExcpt = new UniformException()
                    {
                        Message = "", 
                        Reason = string.Format("There's no movie for given id: {0}", movieId)
                    };
                    throw new FaultException<UniformException>(movieExcpt);
                }

                if (movie.Name == null)
                {
                    throw new FaultException("No content available for the given Id.");
                }

                return movie;
            }
            catch (FaultException<UniformException> ex)
            {
                throw;
            }
            catch (FaultException ex)
            {
                throw;
            }
            catch (CommunicationException ex)
            {
                //throw new FaultException("A communication exception has occured!" + ex.Message);
               
                throw new FaultException<CommunicationException>(new CommunicationException("ap ex"), "No reason");
            }
            catch (Exception ex)
            {
                throw new FaultException("Something went terribly wrong!" + ex.Message);
            }
        }

        public MovieDetailsData GetDetails(int movieId)
        {
            return _dataRepositoryFactory.GetDataRepository<IMovieRepository>().GetDetails(movieId);
        }

        //[PrincipalPermission(SecurityAction.Demand, Role = Roles.Admin)]
        //[PrincipalPermission(SecurityAction.Demand, Name = Roles.User)]
        public Movie[] GetAll()
        {
            try
            {
                IMovieRepository movieRepository =
                    _dataRepositoryFactory.GetDataRepository<IMovieRepository>();

                return movieRepository.Get().ToArray();
            }
            catch (Exception ex) 
            {
                throw new FaultException(ex.Message);
            }

        }

        [OperationBehavior(TransactionScopeRequired = true)]
        //[PrincipalPermission(SecurityAction.Demand, Role = Roles.Admin)]
        public Movie Update(Movie movie)
        {
            try
            {
                IMovieRepository movieRepository = _dataRepositoryFactory.GetDataRepository<IMovieRepository>();
                
                Movie updatedMovie = null;

                if (movie.Id == 0)
                    updatedMovie = movieRepository.Add(movie);
                else
                    updatedMovie = movieRepository.Update(movie);

                return updatedMovie;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public MovieCreationData UpdateSec(MovieCreationData movieCreationData)
        {
            try
            {
                IMovieRepository movieRepository = _dataRepositoryFactory.GetDataRepository<IMovieRepository>();

                MovieCreationData updatedMovie = null;

                if (movieCreationData.Id == 0)
                    updatedMovie = movieRepository.Add(movieCreationData);
                else
                    updatedMovie = movieRepository.UpdateSec(movieCreationData);

                return updatedMovie;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public MovieData UpdateSimple(MovieData movieData)
        {
            IMovieRepository movieRepository = _dataRepositoryFactory.GetDataRepository<IMovieRepository>();

            return movieRepository.UpdateSimple(movieData);
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        //[PrincipalPermission(SecurityAction.Demand, Role = Roles.Admin)]
        public void Delete(int movieId)
        {
            try
            {
                IMovieRepository movieRepository = _dataRepositoryFactory.GetDataRepository<IMovieRepository>();
                movieRepository.Remove(movieId);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public MovieData[] GetByPage(int page, int pageSize)
        {
            IMovieRepository movieRepository = _dataRepositoryFactory.GetDataRepository<IMovieRepository>();
            return movieRepository.GetByPage(page, pageSize).ToArray();
        }

        public int TotalCount()
        {
            return _dataRepositoryFactory.GetDataRepository<IMovieRepository>().TotalCount();
        }

        public Movie[] GetMoviesByActorId(int id)
        {
            IMovieRepository movieRepository = _dataRepositoryFactory.GetDataRepository<IMovieRepository>();

            return movieRepository.GetMoviesByActorId(id).ToArray();
        }

        public Movie[] GetMoviesByDirectorId(int id)
        {
            IMovieRepository movieRepository = _dataRepositoryFactory.GetDataRepository<IMovieRepository>();

            return movieRepository.GetMoviesByDirectorId(id).ToArray();
        }

        public string Statistics_MoviesByYear()
        {
            IMovieRepository movieRepository = _dataRepositoryFactory.GetDataRepository<IMovieRepository>();
            var stats = movieRepository.Statistics_MoviesByYear();

            var serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(stats);
            return json;
        }

        public string Statistics_MoviesByGenre()
        {
            IMovieRepository movieRepository = _dataRepositoryFactory.GetDataRepository<IMovieRepository>();
            var stats = movieRepository.Statistics_MoviesByGenre();

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(stats);
            return json;
        }
    }
}


/*                  Some thoughts on NotFoundException
 * NotFoundException ex = 
                    new NotFoundException(string.Format("{0} numaralı Id'ye sahip bir film girdisi bulunmamaktadır.", movieId));
 * throw new FaultException<NotFoundException>(ex, ex.Message);
 *
 * (sadece throw NotFoundException diyemeyiz, dikkat et!)
 *
 * In the world of WCF, you don't really throw an exception because remember there's a physical seperation
 * between the server and the client. So It's not like you can throw an exception that the client will automatically catch.
 * because the client is on a different machine on the other side of the world.
 * 
 * What you've got to do is wrap this in something called a SOAP fault, 
 * which will get transmitted through the SOAP message, and then the client will know how to handle that later. 
 * WCF allows us to do this with something called a FaultException of T. 
 * What we've got to do is throw a FaultException of T, 
 * T being this exception that we're defining here, the NotFoundException.
 * 
 * Böylece bir hata durumunda proxy hiçbir şeyden etkilenmeyecek. 
 * 
 * NotFoundException'ı fırlatmadan önce NotFoundException'dan WCF'i haberdar etmeliyiz. 
 * Böylece bunun nasıl düzgün bir şekilde serialize edeceğini bilecek. 
 * Bunun için servis kontrat'ı içerisinde (IInventoryManager) 
 * GetCar property'si için [FaultContract(typeof(NotFoundException))] attribute'u verildi.
 * 
 * FaultException<T> kullanılacaksa T için servis kontratında üstte bahsedilen ayarın yapılması gerekir.
 * [FaultContract(typeof(T))]
 * 
 * Sadece FaultException kullanılacaksa belirtmeye gerek yok.
 */

/*
 *              NEDEN catch (FaultException ex) DİYE BİR BLOK VAR?
 * 
 * Eğer movieEntity null olurda if bloğunun içindeki kod çalışırsa, yani:
 * NotFoundException fırlatılırsa catch (Exception ex) blokuna kod düşecektir. Bunu engellememmiz gerekiyor.
 * Bunun için catch (FaultException ex) bloku yazıldı ve bu blokta yaptığım tekrar hatayı fırlatmak.
 * 
 * Böylece fırlatılan herhangi bir FaultException<T> için hata catch (FaultException ex) bloğuna düşecek
 * diğer bir ifadeyle asla catch (Exception ex) bloğuna düşmeyecek ki baştan beri istediğimiz bu.
 */

/*
 *          MovieService ÜZERİNE NOTLAR
 * 
 * Öncelikle bu serviste (ve diğerlerinde de) DataRepository classlarına erişebilmemiz gerekir.
 * DataRepository class'ın direk olarak buraya (servise) enjekte etmiyoruz.
 * Bunun yerine önceden oluşturduğumuz data repository factory kullanıyoruz.
 * DataRepository factory ile operasyonlar için gerekli repository'yi enjekte ediyoruz.
 *
 * Bunu yapmamımızın nedeni tam olarak şudur:
 * Bu servis sınıfında tanımlanan operasyonlara göre birden fazla DataRepository'ye ihtiyacımız olabilir.
 * Buradaki TÜM operasyonların aynı anda TÜM repository'leri kullanma gibi bir durumu (çok büyük ihtimalle) olmayacağı için,
 * Bir servis operasyonu için sadece o operasyonun kullandığı DataRepository class'ını instantiate etmek için böyle bir yapıya başvurdk.
 * Aksi sadece bir repository kullanan servir operasyonu için tüm repository sınıfları instantiate edilmekteydi.
 * 
 * Sonuç olarak yapılan:
 * DI Container'ı tüm Repository'leri -kullanılmasa dahi- enjekte etmeye zorlamıyoruz. Sadece kullanılanı enjekte etmeini sağlıyoruz.
 * 
 *              [InstanceContextMode = InstanceContextMode.PerCall] attibute'unun görevi:
 * WCF, söz konusu servise özel bir davranış biçimi atanmadığını gördüğünde, 
 * servisi PerSession modunda instantiate eder.
 * Bu da şu anlama gelir: Lifetime of the servise equals the lifetime of the proxy.
 *  = So long the proxy is open every call to the service happens on the same instance of that service.
 *  
 * Proxy açık olduğu sürece servis memory'de tutulur.
 * Developer'ın client tarafında ne yapacağından emin olamayız. 
 * Proxy'yi çok uzun süre gereksiz yere açık bırakabilirler. (Kaynak israfı)
 * Client tarafındaki developerlara güvenemeyeceğimizden bu servisin davranış biçimi yukardaki attribute ile PerSession -> PerCall yapıldı.
 *  Her bir çağrı (Call) için yeni bir service örneği oluşturulacak (instantion)
 *  Çağrı bittiğinde servis dispose olacak, kaynaklar serbest bırakılacak.
 *  
 * ReleaseServiceInstanceOnTransactionComplete
 * Default true. Birinci ve ikinci attribute'ların kombinasyonları dolayısıyla false yapıldı.
 */