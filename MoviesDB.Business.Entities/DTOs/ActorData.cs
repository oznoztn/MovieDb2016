using System.Runtime.Serialization;

namespace MoviesDB.Business.Entities.DTOs
{
    [DataContract]
    public class ActorData
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
