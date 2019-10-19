using System;
using System.Collections.Generic;
using Core.Common.Contracts;
using Core.Common.Core;

namespace MoviesDB.Business.Entities
{
    public class Poll : EntityBase, IIdentifiableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public bool OnHomePage { get; set; }
        public bool Published { get; set; }
        public bool GuestsAllowedForVoting { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<PollVote> PollVotes { get; set; }

        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
