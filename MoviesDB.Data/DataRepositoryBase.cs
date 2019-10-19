using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using Core.Common.Data;

namespace MoviesDB.Data
{
    public abstract class DataRepositoryBase<T> : DataRepositoryBase<T, MovieDbContext>
        where T : class, IIdentifiableEntity, new()
    {
    }
}

/*
 *  new()    
 * DbContext class'ının bir örneğini oluşturur (instantiation)
 * 
 * Bu class'ın amacı, oluşturulan her bir repository için DbContext class'ını oluşturmak.
 * Böylece repository class'larında sürekli MovieDbContext'i belirtmekten kurtuluyoruz.
 *
 * Örnek olarak AccountRepository ...
 *   ÖNCE : public class AccountRepository : DataRepositoryBase<Account, MovieDbContext>
 *   ŞMDİ : public class AccountRepository : DataRepositoryBase<Account> 
 */