using Core.Common.Proxy;
using MoviesDB.Client.Contracts;
using MoviesDB.Client.Entities;
using System.ComponentModel.Composition;
using MoviesDB.Client.Entities.DTOs;

namespace MoviesDB.Client.Proxies
{
    [Export(typeof(IMovieService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MovieProxy : ProxyBase<IMovieService>, IMovieService
    {
        public Movie Get(int movieId)
        {
            return Channel.Get(movieId);
        }

        public MovieCreationData GetSec(int movieId)
        {
            return Channel.GetSec(movieId);
        }

        public MovieData[] SearchMovie(string name)
        {
            return Channel.SearchMovie(name);
        }

        public MovieData GetMovieByImdbId(string imdbId)
        {
            return Channel.GetMovieByImdbId(imdbId);
        }

        public Movie[] GetAll()
        {
            return Channel.GetAll();
        }

        public MovieDetailsData GetDetails(int movieId)
        {
            return Channel.GetDetails(movieId);
        }

        public Movie Update(Movie movie)
        {
            return Channel.Update(movie);
        }

        public MovieCreationData UpdateSec(MovieCreationData entity)
        {
            return Channel.UpdateSec(entity);
        }

        public MovieData UpdateSimple(MovieData movieData)
        {
            return Channel.UpdateSimple(movieData);
        }

        public void Delete(int movieId)
        {
            Channel.Delete(movieId);
        }

        public MovieData[] GetByPage(int page, int pageSize)
        {
            return Channel.GetByPage(page, pageSize);
        }

        public int TotalCount()
        {
            return Channel.TotalCount();
        }

        public Movie[] GetMoviesByActorId(int id)
        {
            return Channel.GetMoviesByActorId(id);
        }

        public Movie[] GetMoviesByDirectorId(int id)
        {
            return Channel.GetMoviesByDirectorId(id);
        }

        public string Statistics_MoviesByYear()
        {
            return Channel.Statistics_MoviesByYear();
        }

        public string Statistics_MoviesByGenre()
        {
            return Channel.Statistics_MoviesByGenre();
        }

        //public MovieInfoData[] GetByPage_Joined(int page, int pageSize)
        //{
        //    return Channel.GetByPage_Joined(page, pageSize);
        //}
    }
}
/* 
* Herhangi bir client herhangi bir proxy sınıfını örneklediğinde (instantiate) 
* kullanıcının user name'i otomatik olarak soap header'a insert edilecek 
* dolayısıyla servis katmanı için kullanılabilir olacak.
* Bunu ProxyBase<T> den kalıtım alarak yapıyoruz.
* ProxyBase<T>'nin constructor'ına bak.
* 
* Generic yapı oluşturmanın bir diğer önemli nedeni de
* Kullanıcının bu işi manuel olarak yapmasını önlemek
* Diğer bir deyişle client tarafındaki kimsenin SOAP ile ilgili kodlara erişmesini istemiyoruz
* Client tarafı sadece proxy sınıfını örnekleyecek ve kullanacak. Hepsi bu kadar. Elle ayarlama olmayacak.
* 
* Dolayısıyla alttaki kodları her bir proxy class için tekrarlamamak ve
* manuel ayarlamaların önüne geçmek için ProxyBase<T> generic class'ı oluşturuldu.
*/

/*
1) SOAP Header'a insert edeceğimiz bilgiyi alıyoruz. 
Bizim durumumuzda sisteme log-in olmuş kişinin kullanıcı adını alıyoruz.
For database authorization

2) Message Header class'a ekliyoruz

3) Message Header class'ındaki bilgiyi outgoing message koleksiyonu'na eklemek için
I have to send in to the constructor of the operation context scope, the channel class
that is actually used in the proxy. Actually callad InnerChannel in this case.

4) Here, I insert some values into the outgoing message's header:
 
        public MovieServiceProxy()
        {
            string userName = Thread.CurrentPrincipal.Identity.Name;
            MessageHeader<string> header = new MessageHeader<string>(userName);
            OperationContext contextScope = new OperationContext(InnerChannel);
            OperationContext.Current.OutgoingMessageHeaders.Add(header.GetUntypedHeader("String", "System"));
        }
 */

/*
 * WCF Proxy sınınfları ClientBase<T> sınıfından kalıtım alırlar.
 * Biz direk olarak ClientBase<T> sınıfından kalıtım almak yerine oluşturduğumuz ProxyBase<T> sınıfından kalıtım alıyoruz.
 * Yani proxy sınıfı ile sistem ClientBase arasında ProxyBase isminde bir bir aracı ekledik.
 *
 * MovieProxy Class -> ProxyBase<T> -> ClientBase<T>
 * 
 * ClientBase<T> ifadesindeki T
 *  TChannel: The channel to be used to connect to service
 *  Provides the base implementation used to create WCF client objects that can call services.
 
    Now in order for me to access the outgoing message header's collection, 
    in which I have to insert this information, 
    I have to spin up something called an OperationContextScope 
    and I have to send into the constructor of the OperationContextScope, 
    the Channel class that is actually used in my proxy, actually called InnerChannel in this case. (Typing)

    Now if I was letting the client take care of all this code from the outside, from outside of this proxy,
    it's the InnerChannel that's actually exposed publicly, but I want to do it all inside the class;
    I don't want the client to actually have to see or do any of this stuff.

    Now the last thing I need to do is insert the MessageHeader 
    that I just created into the outgoing MessageHeader's collection of the OperationContext. 
    Now if you recall back in the service, I actually accessed
    OperationContextScope.Current and accessed its incoming MessageHeader.
*/