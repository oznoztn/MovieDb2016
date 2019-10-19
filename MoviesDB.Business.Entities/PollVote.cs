using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

namespace MoviesDB.Business.Entities
{
    public class PollVote : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int PollId { get; set; }
        
        [DataMember]
        public string VoteText { get; set; }
        
        [DataMember]
        public int DisplayOrder { get; set; }
        
        [DataMember]
        public int VoteCount { get; set; }

        [IgnoreDataMember]
        public Poll Poll { get; set; }

        [IgnoreDataMember]
        public ICollection<PollVotingRecord> PollVotingRecords { get; set; }

        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
