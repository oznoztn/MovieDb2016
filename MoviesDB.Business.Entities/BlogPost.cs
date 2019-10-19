using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

namespace MoviesDB.Business.Entities
{
    [DataContract]
    public class BlogPost : EntityBase, IIdentifiableEntity
    {
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
