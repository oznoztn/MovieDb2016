using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;

namespace MoviesDB.Business.Entities.DTOs
{
    [DataContract]
    public class ActorCreationData
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
        public bool Gender { get; set; }

        [DataMember]
        public string Biography { get; set; }

        [DataMember]
        public string ImdbLink { get; set; }

        [DataMember]
        public int CountryId { get; set; }

        [DataMember]
        public int StateId { get; set; }

        [DataMember]
        public int CountyId { get; set; }

        [DataMember]
        public int[] MovieIds { get; set; }

        [DataMember]
        public string Photo { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        public DateTime? BornDate { get; set; }

        [DataMember]
        public DateTime? DeathDate { get; set; }
    }
}
