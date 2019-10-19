using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using Core.Common.Core;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Contracts
{
    [ServiceContract]
    public interface IPollService : IServiceContract
    {
        [OperationContract]
        Poll Get(int entityId);

        [OperationContract]
        Poll[] GetAll();

        [OperationContract]
        Poll Update(Poll entity);

        [OperationContract]
        void Delete(int entityId);

        [OperationContract]
        Poll[] GetByPage(int page, int pageSize);

        [OperationContract]
        int TotalCount();
    }
}
