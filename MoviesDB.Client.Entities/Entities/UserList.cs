using System.Runtime.Serialization;
using Core.Common.Core;
using FluentValidation;

namespace MoviesDB.Client.Entities
{
    public class UserList : ObjectBase
    {
        [DataContract]
        class UserListValidator : AbstractValidator<UserList>
        {
             
        }

        protected override IValidator GetValidator()
        {
            return new UserListValidator();
        }
    }
}
