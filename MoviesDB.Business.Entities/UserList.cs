using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

namespace MoviesDB.Business.Entities
{
    [DataContract]
    public class UserList : EntityBase, IIdentifiableEntity, ILoggedUser
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

        [IgnoreDataMember]
        public User User { get; set; }

        [IgnoreDataMember]
        public ICollection<UserListRecord> UserListRecords { get; set; }

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
