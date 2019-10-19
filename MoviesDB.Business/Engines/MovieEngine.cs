using System.ComponentModel.Composition;
using MoviesDB.Business.Common;

namespace MoviesDB.Business.Engines
{
    [Export(typeof(IMovieEngine))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MovieEngine : IMovieEngine
    {

    }
}


/*
 * Servis class'larındaki metotlar DAL ile ilgili metotlardı
 * Engine class'ında yeniden kullanılabilecek ve DAHA KARMAŞIK İŞLEMLER için kodlar bulunacak.
 * 
 * Buradaki metotlar (sadece metot, dikkat et!, operation değil - operation servis ile ilgili!) 
 * genelde servis operasyonlarında kullanılmak için yazılacak şeyler.
 * 
 * Buranın SERVICE katmanı ile ilgisi olmadığı için (üste de belirtildiği gibi)
 * buradaki exception handling işlemlerinde, WCF deki prosedürler (FaultException<T> geçerli değil)
 * Yani burada FaultException fırlatmıyoruz.
 * Normal olarak throw new BilmemNeException() denilebilir (083)
 */


/*
 * 
 * ENGINE SINIFLARI ENJEKTE EDİLMELİ.
 * (EXPORT)
 * 
 * NOTLARIN HEPSİNİ OKU
 * 
 */