using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using Core.Common.Core;

namespace MoviesDB.Data
{
    [Export(typeof(IDataRepositoryFactory))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DataRepositoryFactory : IDataRepositoryFactory
    {
        T IDataRepositoryFactory.GetDataRepository<T>()
        {
            return ObjectBase.Container.GetExportedValue<T>();
        }
    }
}


// DataRepositoryFactory'nin görevi, T jenerik argümanı için bir DataRepository bulup döndermek
/*
 * MEF ile export ediliyor (T için dönecek olan DataRepositorty<T>)
 * Manuel olarak T için dönecek şeyin örneğini metot içinde yaratmak yerine MEF aracılığıyla DataRepository<T> yi elde ediliyoruz.
 * Bunun için ObjectBase sınıfı içindeki static property olan Container'a başvuruyoruz ve GetExportedValue<T> metodunu çağırıyoruz.
 * 
 * Sonuç olarak bir şey GetDataRepository<T> metodunu çağırdığında;
 *  T: IAccountRepository olsun.
 *      IAccountRepository ile ilişkili, Dep. Injection Container'ında hangi class var ise onu dönderecek
 *          Dönecek olan class ise AccountRepository olacaktır.
 * 
 * DataRepositoryFactory sınıfı, kendisine verdiğim interface için, 
 *  o interface ile ilişkili olan repository sınıfını (concrete class) bana sunacaktır.
 *  
 * Ayrıca DataRepositoryFactory sınıfının bir interface'i (IDataRepositoryFactory) olduğu için 
 * Artık bu sınıfı da Dependency Injection işlemlerinde kullanabilirim: *  
 *
 * DI için attribute'lar eklendi.
 *   [Export(typeof(IDataRepositoryFactory))]
 *   [PartCreationPolicy(CreationPolicy.NonShared)] 
 *      
 */
