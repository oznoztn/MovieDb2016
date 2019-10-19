using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Core;
using FluentValidation;

namespace MoviesDB.Client.Entities
{
    public class PollVotingRecord : ObjectBase
    {
        class PollVotingRecordValidator : AbstractValidator<PollVotingRecord>
        {

        }

        protected override IValidator GetValidator()
        {
            return new PollVotingRecordValidator();
        }
    }
}
