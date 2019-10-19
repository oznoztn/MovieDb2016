using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

namespace MoviesDB.Business.Entities
{
    [DataContract]
    public class News : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Header { get; set; }

        [DataMember]
        public string Synopsis { get; set; }

        [DataMember]
        public string FullText { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

        //[IgnoreDataMember]
        //public ICollection<NewsCategory> NewsCategories { get; set; }

        [IgnoreDataMember]
        public ICollection<NewsMapping> NewsMappings { get; set; }

        public int EntityId
        {
            get { return Id; }

            set { Id = value; }
        }
    }
}
