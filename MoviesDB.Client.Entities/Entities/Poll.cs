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
    public class Poll : ObjectBase
    {
        [DataContract]
        class PollValidator : AbstractValidator<Poll>
        {

        }

        protected override IValidator GetValidator()
        {
            return new Poll.PollValidator();
        }
    }
}
