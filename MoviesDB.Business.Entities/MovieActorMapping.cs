using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using Core.Common.Core;

namespace MoviesDB.Business.Entities
{
    [DataContract]
    public class MovieActorMapping : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int MovieId { get; set; }

        [DataMember]
        public int ActorId { get; set; }

        [DataMember]
        public Movie Movie { get; set; }

        [DataMember]
        public Actor Actor { get; set; }
        
        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
