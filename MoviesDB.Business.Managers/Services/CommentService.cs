using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using MoviesDB.Business.Contracts;
using MoviesDB.Business.Entities;

namespace MoviesDB.Business.Services.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CommentService : ServiceBase, ICommentService
    {
        public Comment Get(int entityId)
        {
            throw new NotImplementedException();
        }

        public Comment[] GetAll()
        {
            throw new NotImplementedException();
        }

        public Comment Update(Comment entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }

        public Comment[] GetByPage(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public int TotalCount()
        {
            throw new NotImplementedException();
        }
    }
}
