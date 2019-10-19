using Core.Common.Contracts;
using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MoviesDB.Business.Entities
{
    [DataContract]
    public class Actor : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string FullName { get; set; }

        [DataMember]
        public string Biography { get; set; }

        [DataMember]
        public string ImdbLink { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        public DateTime? BirthDate { get; set; }

        [DataMember]
        public DateTime? DeathDate { get; set; }

        [DataMember]
        public int CountryId { get; set; }

        [DataMember]
        public int StateId { get; set; }

        [DataMember]
        public int CountyId { get; set; }

        [DataMember]
        public bool Gender { get; set; }

        [DataMember]
        public string Photo { get; set; }

        //[IgnoreDataMember]
        //public ICollection<Movie> Movies { get; set; }

        [IgnoreDataMember]
        public ICollection<NewsMapping> NewsMappings { get; set; }

        [IgnoreDataMember]
        public ICollection<MovieActorMapping> MovieActorMappings { get; set; }

        [IgnoreDataMember]
        public Country Country { get; set; }
        
        [IgnoreDataMember]
        public State State { get; set; }
        
        [IgnoreDataMember]
        public County County { get; set; }

        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
