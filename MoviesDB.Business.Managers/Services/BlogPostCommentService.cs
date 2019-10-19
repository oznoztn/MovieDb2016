using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using MoviesDB.Business.Contracts;
using MoviesDB.Business.Entities;
using MoviesDB.Data.Contracts;

namespace MoviesDB.Business.Services.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class BlogPostCommentService : ServiceBase, IBlogPostCommentService
    {
        [Import]
        private IBlogPostCommentRepository _blogPostCommentRepository;

        public BlogPostComment Get(int entityId)
        {
            return _blogPostCommentRepository.Get(entityId);
        }

        public BlogPostComment[] GetAll()
        {
            return _blogPostCommentRepository.Get().ToArray();
        }

        public BlogPostComment Update(BlogPostComment entity)
        {
            return _blogPostCommentRepository.Update(entity);
        }

        public void Delete(int entityId)
        {
            _blogPostCommentRepository.Remove(entityId);
        }

        public BlogPostComment[] GetByPage(int page, int pageSize)
        {
            return _blogPostCommentRepository.GetByPage(page, pageSize).ToArray();
        }

        public int TotalCount()
        {
            throw new NotImplementedException();
        }
    }
}
