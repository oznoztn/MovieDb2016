using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

namespace MoviesDB.Business.Entities
{
    [DataContract]
    public class Genre : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int Id { get; set; }
        
        [DataMember]
        public string Name { get; set; }
        
        [DataMember]
        public bool IsSubGenre { get; set; }

        //[IgnoreDataMember]
        //public ICollection<Movie> Movies { get; set; }

        [DataMember]
        public ICollection<MovieGenreMapping> MovieGenreMappings { get; set; }

        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }        
    }
}
