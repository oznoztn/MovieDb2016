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
    public class Country : ObjectBase
    {
        class CountryValidator : AbstractValidator<Country>
        {
            
        }

        protected override IValidator GetValidator()
        {
            return new Country.CountryValidator();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
