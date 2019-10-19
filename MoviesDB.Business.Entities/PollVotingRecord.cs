using System;
using Core.Common.Contracts;
using Core.Common.Core;

namespace MoviesDB.Business.Entities
{
    public class PollVotingRecord : EntityBase, IIdentifiableEntity
    {
        public int Id { get; set; }
        public int VoteId { get; set; }
        public int UserId { get; set; }
        public DateTime VotingDate { get; set; }

        public PollVote PollVote { get; set; }
        public User User { get; set; }

        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
        
        //public int PollId { get; set; }
        //public Poll Poll { get; set; }

    }
}
