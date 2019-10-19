using System;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

namespace MoviesDB.Business.Entities
{
    [DataContract]
    public class UserListRecord : EntityBase, IIdentifiableEntity, ILoggedUser
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ListId { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        public UserList UserList { get; set; }

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
