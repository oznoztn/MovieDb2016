using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Core;
using MoviesDB.Business.Entities;

namespace MoviesDB.Business.Contracts
{
    [ServiceContract]
    public interface IPollService 
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
