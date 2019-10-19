using System;
using System.Runtime.Serialization;
using Core.Common.Core;
using FluentValidation;

namespace MoviesDB.Client.Entities
{
    [DataContract]
    public class News : ObjectBase
    {
        class NewsValidator : AbstractValidator<News>
        {
            
        }

        protected override IValidator GetValidator()
        {
            return new NewsValidator();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Header { get; set; }

        [DataMember]
        public string Synopsis { get; set; }

        [DataMember]
        public string FullText { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

    }
}
