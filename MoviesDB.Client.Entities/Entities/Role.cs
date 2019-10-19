using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Common.Core;
using FluentValidation;

namespace MoviesDB.Client.Entities
{
    [DataContract]
    public class Role : ObjectBase
    {
        class RoleValidator : AbstractValidator<Role>
        {
                
        }

        protected override IValidator GetValidator()
        {
            return new RoleValidator();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public ICollection<User> Users { get; set; }
    }
}
