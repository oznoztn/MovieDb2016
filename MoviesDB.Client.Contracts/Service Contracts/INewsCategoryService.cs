using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Contracts
{
    [ServiceContract]
    public interface INewsCategoryService : IServiceContract
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
        News[] GetByPage(int page, int pageSize);

        [OperationContract]
        int TotalCount();
    }
}
