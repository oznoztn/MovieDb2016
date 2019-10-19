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
    public interface IBlogPostCommentService
    {
        [OperationContract]
        BlogPostComment Get(int entityId);

        [OperationContract]
        BlogPostComment[] GetAll();

        [OperationContract]
        BlogPostComment Update(BlogPostComment entity);

        [OperationContract]
        void Delete(int entityId);

        [OperationContract]
        BlogPostComment[] GetByPage(int page, int pageSize);

        [OperationContract]
        int TotalCount();
    }
}
