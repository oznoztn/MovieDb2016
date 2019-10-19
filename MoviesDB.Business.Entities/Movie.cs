using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

namespace MoviesDB.Business.Entities
{
    [DataContract]
    public class Movie : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int DirectorId { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public int CountryId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Aka { get; set; }
        
        [DataMember]
        public int Year { get; set; }
        
        [DataMember]
        public int RunningTime { get; set; }

        [DataMember]
        public float Rating { get; set; }
        
        [DataMember]
        public int VoteCount { get; set; }

        [DataMember]
        public string ImdbLink { get; set; }

        [DataMember]
        public string PlotOutline { get; set; }

        [DataMember]
        public string CoverImage { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        public Director Director { get; set; }

        [DataMember]
        public Language Language { get; set; }

        [DataMember]
        public Country Country { get; set; }

        //[DataMember]
        //public ICollection<Genre> Genres { get; set; }

        //[DataMember]
        //public ICollection<Actor> Actors { get; set; }

        [DataMember]
        public ICollection<Comment> Comments { get; set; }

        [DataMember]
        public ICollection<Review> Reviews { get; set; }

        [DataMember]
        public ICollection<NewsMapping> NewsMappings { get; set; }

        [DataMember]
        public ICollection<MovieActorMapping> MovieActorMappings { get; set; }

        [DataMember]
        public ICollection<MovieGenreMapping> MovieGenreMappings { get; set; }


        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
