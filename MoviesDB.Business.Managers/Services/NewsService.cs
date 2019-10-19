using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using MoviesDB.Business.Contracts;
using MoviesDB.Business.Entities;
using MoviesDb.Common;

namespace MoviesDB.Business.Services.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]
    public class NewsService : ServiceBase, INewsService
    {
        public News Get(int entityId)
        {
            throw new NotImplementedException();
        }

        public News[] GetAll()
        {
            throw new NotImplementedException();
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public News Update(News news)
        {
            throw new NotImplementedException();
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }

        public News[] GetByPage(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public int TotalCount()
        {
            throw new NotImplementedException();
        }
    }
}
