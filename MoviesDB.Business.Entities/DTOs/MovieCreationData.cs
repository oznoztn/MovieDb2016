using System.Runtime.Serialization;
using Core.Common.Contracts;

namespace MoviesDB.Business.Entities.DTOs
{
    [DataContract]
    public class MovieCreationData
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
        public string PlotOutline { get; set; }

        [DataMember]
        public string CoverImage { get; set; }

        [DataMember]
        public string ImdbLink { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public int RunningTime { get; set; }

        [DataMember]
        public int[] ActorIds { get; set; }

        [DataMember]
        public int[] GenreIds { get; set; }

        [DataMember]
        public int[] SubGenreIds { get; set; }
    }
}
