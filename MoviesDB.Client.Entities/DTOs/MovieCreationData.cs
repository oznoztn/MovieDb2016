using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Core;
using FluentValidation;

namespace MoviesDB.Client.Entities.DTOs
{
    [DataContract]
    public class MovieCreationData : ObjectBase
    {
        class MovieCreationDataValidator : AbstractValidator<MovieCreationData>
        {
            public MovieCreationDataValidator()
            {
                RuleFor(t => t.Name).NotNull().Length(2, 75);
                RuleFor(t => t.CountryId).NotEmpty();
                RuleFor(t => t.ImdbLink).NotEmpty();
                RuleFor(t => t.LanguageId).NotEmpty();
                RuleFor(t => t.RunningTime).NotEmpty().LessThanOrEqualTo(400);
                RuleFor(t => t.Year).NotEmpty().GreaterThanOrEqualTo(1900).LessThanOrEqualTo(2100);
                RuleFor(t => t.ActorIds).NotEmpty().WithMessage("'Actor(s)' should not be empty.");
                RuleFor(t => t.GenreIds).NotEmpty().WithMessage("'Genre(s)' should not be empty.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new MovieCreationDataValidator();
        }

        [DataMember]
        public int Id { get; set; }
        
        [DataMember]
        public int DirectorId { get; set; }
        
        [DataMember]
        public int LanguageId { get; set; }
        
        [DataMember]
        public int CountryId { get; set; }
        
        [DataMember]
        public string Name { get; set; }
        
        [DataMember]
        public string Aka { get; set; }
        
        [DataMember]
        public string PlotOutline { get; set; }

        [DataMember]
        public string CoverImage { get; set; }
        
        [DataMember]
        public string ImdbLink { get; set; }
        
        [DataMember]
        public int Year { get; set; }
        
        [DataMember]
        public int RunningTime { get; set; }

        [DataMember]
        public int[] ActorIds { get; set; }
        
        [DataMember]
        public int[] GenreIds { get; set; }
        
        [DataMember]
        public int[] SubGenreIds { get; set; }
    }
}
