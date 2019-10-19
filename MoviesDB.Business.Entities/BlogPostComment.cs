using System;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

namespace MoviesDB.Business.Entities
{
    [DataContract]
    public class BlogPostComment : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int BlogPostId { get; set; }
        
        [DataMember]
        public int UserId { get; set; }

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
