using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using MoviesDB.Business.Entities;

namespace MoviesDB.Business.Contracts
{
    [ServiceContract]
    public interface IBlogPostService
    {
        [OperationContract]
        BlogPost Get(int entityId);

        [OperationContract]
        BlogPost[] GetAll();

        [OperationContract]
        BlogPost Update(BlogPost entity);

        [OperationContract]
        void Delete(int entityId);

        [OperationContract]
        BlogPost[] GetByPage(int page, int pageSize);

        [OperationContract]
        int TotalCount();
    }
}
