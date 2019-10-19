using Core.Common.Contracts;
using MoviesDB.Client.Proxies;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using MoviesDB.Data;

namespace MoviesDB.Client.IoC
{
    public static class MEFLoader
    {
        public static CompositionContainer Init()
        {
            return Init(null);
        }

        public static CompositionContainer Init(ICollection<ComposablePartCatalog> catalogParts)
        {
            AggregateCatalog catalog = new AggregateCatalog();
            
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(MovieProxy).Assembly));
            
            if (catalogParts != null)
                foreach (var part in catalogParts)
                    catalog.Catalogs.Add(part);

            CompositionContainer container = new CompositionContainer(catalog);

            return container;
        }
    }
}

/*
 * Burası client tarafındaki dependency'lerin resolve yapıldığı class.  
 * 
 *      catalog.Catalogs.Add(new AssemblyCatalog(typeof(MovieProxy).Assembly));  
 * Client tarafındaki dependency'leri keşfetmek için ilk adım.
 * MovieProxy ve diğer proxy'ler aynı assembly içerisinde olduğundan typeof(MovieProxy) dedik.
 * Assembly'yi tespit ettikten sonra, MEF tarafından taranacak ve dependency'ler keşfedilecek.
 */