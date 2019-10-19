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
    public class Review : ObjectBase
    {
        class ReviewValidator : AbstractValidator<Review>
        {

        }

        protected override IValidator GetValidator()
        {
            return new Review.ReviewValidator();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int MovieId { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Body { get; set; }

        [DataMember]
        public string MetaKeywords { get; set; }

        [DataMember]
        public string MetaDescription { get; set; }

        [DataMember]
        public string MetaTitle { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        public Movie Movie { get; set; }

        [DataMember]
        public User User { get; set; } 
    }
}
