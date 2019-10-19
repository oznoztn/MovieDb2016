using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDB.Client.Entities.DTOs
{
    [DataContract]
    public class ActorDetailsData
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
        public DateTime? BirthDate { get; set; }

        [DataMember]
        public DateTime? DeathDate { get; set; }

        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string County { get; set; }

        [DataMember]
        public bool Gender { get; set; }

        [DataMember]
        public string Photo { get; set; }

        [DataMember]
        public Movie[] Movies { get; set; }

        [DataMember]
        public News[] News { get; set; }
    }
}
