using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Core;
using FluentValidation;

namespace MoviesDB.Client.Entities
{
    [DataContract]
    public class BlogPostComment : ObjectBase
    {
        class BlogPostCommentValidator : AbstractValidator<BlogPostComment>
        {

        }

        protected override IValidator GetValidator()
        {
            return new BlogPostComment.BlogPostCommentValidator();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public int BlogPostId { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public bool Published { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        public User User { get; set; }

        [DataMember]
        public BlogPost BlogPost { get; set; }

        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
