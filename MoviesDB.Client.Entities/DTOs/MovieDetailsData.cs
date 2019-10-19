using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using Core.Common.Core;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Entities.DTOs
{
    [DataContract]
    public class MovieDetailsData
    {
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
        public string Language { get; set; }

        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public string DirectorName { get; set; }

        [DataMember]
        public string DirectorImdbLink { get; set; }

        [DataMember]
        public string DirectorPhoto { get; set; }

        [DataMember]
        public Actor[] Actors { get; set; }

        [DataMember]
        public Genre[] Genres { get; set; }

        [DataMember]
        public string[] SubGenres { get; set; }

        [DataMember]
        public Review[] Reviews { get; set; }

        [DataMember]
        public News[] News { get; set; }
    }
}
