using System;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

namespace MoviesDB.Business.Entities
{
    [DataContract]
    public class Review : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int MovieId { get; set; }

        [DataMember]
        public int UserId { get; set; }

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
        public Movie Movie { get; set; }

        [DataMember]
        public User User { get; set; }

        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }    
    }
}
