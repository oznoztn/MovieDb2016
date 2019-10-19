using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using Core.Common.Core;
using FluentValidation;

namespace MoviesDB.Client.Entities
{
    [DataContract]
    public class Language : ObjectBase, IIdentifiableEntity
    {
        class CountryValidator : AbstractValidator<Language>
        {
            public CountryValidator()
            {
                RuleFor(t => t.Name).NotNull().Length(1,15);
            }
        }

        protected override IValidator GetValidator()
        {
            return new Language.CountryValidator();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [IgnoreDataMember]
        public ICollection<Movie> Movies { get; set; }

        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
