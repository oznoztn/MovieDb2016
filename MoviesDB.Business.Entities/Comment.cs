using System;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

namespace MoviesDB.Business.Entities
{
    [DataContract]
    public class Comment : EntityBase, IIdentifiableEntity, ILoggedUser
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public int MovieId { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public bool IsConfirmed { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        public User User { get; set; }

        [DataMember]
        public Movie Movie { get; set; }

        public int EntityId
        {
            get { return Id; }

            set { Id = value; }
        }

        int ILoggedUser.LoggedAccountId
        {
            get { return Id; }
        }
    }
}
