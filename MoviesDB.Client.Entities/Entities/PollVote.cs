using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Core;
using FluentValidation;

namespace MoviesDB.Client.Entities
{
    [DataContract]
    public class PollVote : ObjectBase
    {
        class PollVoteValidator : AbstractValidator<PollVote>
        {

        }

        protected override IValidator GetValidator()
        {
            return new PollVoteValidator();
        }
    }
}
