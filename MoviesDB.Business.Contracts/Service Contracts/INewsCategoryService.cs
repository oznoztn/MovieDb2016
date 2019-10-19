using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using MoviesDB.Business.Entities;

namespace MoviesDB.Business.Contracts
{
    [ServiceContract]
    public interface INewsCategoryService
    {
        [OperationContract]
        NewsCategory Get(int entityId);

        [OperationContract]
        NewsCategory[] GetAll();

        [OperationContract]
        NewsCategory Update(NewsCategory entity);

        [OperationContract]
        void Delete(int entityId);

        [OperationContract]
        NewsCategory[] GetByPage(int page, int pageSize);

        [OperationContract]
        int TotalCount();
    }
}
