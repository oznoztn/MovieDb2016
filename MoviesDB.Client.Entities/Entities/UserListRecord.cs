using System.Runtime.Serialization;
using Core.Common.Core;
using FluentValidation;

namespace MoviesDB.Client.Entities
{
    public class UserListRecord : ObjectBase
    {
        [DataContract]
        class UserListRecordValidator : AbstractValidator<UserListRecord>
        {

        }

        protected override IValidator GetValidator()
        {
            return new UserListRecordValidator();
        }
    }
}
