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
    public class BlogPost : ObjectBase
    {
        class BlogPostValidator : AbstractValidator<BlogPost>
        {

        }

        protected override IValidator GetValidator()
        {
            return new BlogPost.BlogPostValidator();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Body { get; set; }

        [DataMember]
        public string MetaKeywords { get; set; }

        [DataMember]
        public string MetaDescription { get; set; }

        [DataMember]
        public string MetaTitle { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        public bool Published { get; set; }

        [DataMember]
        public bool AllowComments { get; set; }

        [DataMember]
        public ICollection<BlogPostComment> Comments { get; set; }

        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
