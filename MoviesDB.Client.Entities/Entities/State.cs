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
    public class State : ObjectBase
    {
        class StateValidator : AbstractValidator<State>
        {

        }

        protected override IValidator GetValidator()
        {
            return new State.StateValidator();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int CountryId { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
