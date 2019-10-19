using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

namespace MoviesDB.Business.Entities
{
    [DataContract]
    public class User : EntityBase, ILoggedUser, IIdentifiableEntity
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Mail { get; set; }

        [DataMember]
        public int? Age { get; set; }

        [DataMember]
        public DateTime CreateDate { get; set; }

        [DataMember]
        public DateTime LastLoginDate { get; set; }

        [DataMember]
        public DateTime? LastPasswordChangedDate { get; set; }

        [DataMember]
        public DateTime? LastAccountSuspensedDate { get; set; }

        [DataMember]
        public DateTime? LastActivityDate { get; set; }

        [DataMember]
        public string Signature { get; set; }

        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public string Website { get; set; }

        [DataMember]
        public string Twitter { get; set; }

        [DataMember]
        public string Facebook { get; set; }

        [DataMember]
        public string Google { get; set; }

        [DataMember]
        public string Instagram { get; set; }

        [DataMember]
        public string Avatar { get; set; }
        
        [DataMember]
        public bool DisablePrivateMessages { get; set; }
        
        [DataMember]
        public bool DisableEmailNotifications { get; set; }
        
        [DataMember]
        public bool IsActivated { get; set; }

        [DataMember]
        public bool IsSuspended { get; set; }

        [DataMember]
        public bool IsSystemAccount { get; set; }

        [DataMember]
        public bool IsPremium { get; set; }

        [DataMember]
        public ICollection<WatchlistRecord> WatchlistRecords { get; set; }

        [DataMember]
        public ICollection<Rating> Ratings { get; set; }
        
        [DataMember]
        public ICollection<Review> MovieReviews { get; set; }

        [DataMember]
        public ICollection<Role> Roles { get; set; }

        [DataMember]
        public ICollection<UserList> UserLists { get; set; }

        [DataMember]
        public ICollection<PollVotingRecord> PollVotingRecords { get; set; }

        [DataMember]
        public ICollection<BlogPostComment> BlogPostComments { get; set; }

        [DataMember]
        public ICollection<Comment> Comments { get; set; }
        
        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }

        public int LoggedAccountId
        {
            get { return Id; }
        }
    }
}
