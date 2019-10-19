using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Common.Core;
using FluentValidation;

namespace MoviesDB.Client.Entities
{
    [DataContract]
    public class NewsCategory : ObjectBase
    {
        class NewsCategoryValidator : AbstractValidator<NewsCategory>
        {
            
        }

        protected override IValidator GetValidator()
        {
            return new NewsCategoryValidator();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
