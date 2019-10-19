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
    public class County : EntityBase, IIdentifiableEntity
    {
        [DataMember] 
        public int Id { get; set; }

        [DataMember]
        public int StateId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [IgnoreDataMember]
        public State State { get; set; }

        public ICollection<Director> Directors { get; set; }

        public ICollection<Actor> Actors { get; set; }

        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
