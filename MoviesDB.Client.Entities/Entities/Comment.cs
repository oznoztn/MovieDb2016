using System;
using System.Runtime.Serialization;
using Core.Common.Core;
using FluentValidation;

namespace MoviesDB.Client.Entities
{
    [DataContract]
    public class Comment : ObjectBase
    {
        class CommentValidator : AbstractValidator<Comment>
        {
            
        }

        protected override IValidator GetValidator()
        {
            return new CommentValidator();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public int MovieId { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public bool IsConfirmed { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        public User User { get; set; }

        [DataMember]
        public Movie Movie { get; set; }
    }
}
