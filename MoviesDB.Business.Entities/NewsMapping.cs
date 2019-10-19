using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

namespace MoviesDB.Business.Entities
{
    public class NewsMapping : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int NewsId { get; set; }

        [DataMember]
        public int? MovieId { get; set; }

        [DataMember]
        public int? ActorId { get; set; }

        [DataMember]
        public int? DirectorId { get; set; }

        [DataMember]
        public int NewsCategoryId { get; set; }

        [IgnoreDataMember]
        public Movie Movie { get; set; }

        [IgnoreDataMember]
        public Actor Actor { get; set; }

        [IgnoreDataMember]
        public News News { get; set; }

        [IgnoreDataMember]
        public Director Director { get; set; }

        [IgnoreDataMember]
        public NewsCategory NewsCategory { get; set; }

        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
