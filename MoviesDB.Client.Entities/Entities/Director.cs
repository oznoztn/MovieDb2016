using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Common.Core;
using FluentValidation;

namespace MoviesDB.Client.Entities
{
    [DataContract]
    public class Director : ObjectBase
    {
        class DirectorValidator : AbstractValidator<Director>
        {
            public DirectorValidator()
            {
                RuleFor(t => t.FirstName).NotNull().Length(1,25);
                RuleFor(t => t.LastName).NotNull().Length(1, 25);
                RuleFor(t => t.BirthDate).NotNull();
                RuleFor(t => t.Gender).NotNull();
            }
        }

        protected override IValidator GetValidator()
        {
            return new DirectorValidator();
        }
        

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string FullName { get; set; }

        [DataMember]
        public bool Gender { get; set; }

        [DataMember]
        public string Biography { get; set; }

        [DataMember]
        public string ImdbLink { get; set; }

        [DataMember]
        public string Photo { get; set; }

        [DataMember]
        public int CountryId { get; set; }

        [DataMember]
        public int StateId { get; set; }

        [DataMember]
        public int CountyId { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        public DateTime? BirthDate { get; set; }

        [DataMember]
        public DateTime? DeathDate { get; set; }

        [IgnoreDataMember]
        public ICollection<Movie> Movies { get; set; }
    }
}
