using System.ComponentModel.Composition.Hosting;
using MoviesDB.Business.Engines;
using MoviesDB.Data;


namespace MoviesDB.Business.IoC
{
    public static class MEFLoader
    {
        public static CompositionContainer Init()
        {
            AggregateCatalog catalog = new AggregateCatalog();

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(MovieEngine).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(MovieRepository).Assembly));

            CompositionContainer container = new CompositionContainer(catalog);

            return container;
        }
    }
}

/*
 *  Export edilenleri keşif aşamasında MEF iki adet assembly'yi tarıyor ve Export edilenleri tespit ediyor. (discover)
 *  
 * MoviesDb.Data assembly -> MovieRepository sınıfına bakarak
 * MoviesDb.Business assembly -> MovieEngine sınıfına bakarak
 * 
 * Önceden kullandığım Simple Injector ve Ninject'de olduğu gibi tek tek belirtme yapmıyorum. 
 * Bunun yerine assembly verip assembly'deki enjekte etmek istediğim sınıfları (Export edilenleri) keşfetmesini istiyorum.
 * 
 * typeof( ... ) ifadesine dikkat et !
 * 
 * 
 */