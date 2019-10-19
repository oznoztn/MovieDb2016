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
    public class NewsMapping : ObjectBase
    {
        class NewsMappingValidator : AbstractValidator<NewsMapping>
        {

        }

        protected override IValidator GetValidator()
        {
            return new NewsMapping.NewsMappingValidator();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int NewsId { get; set; }

        [DataMember]
        public int? MovieId { get; set; }

        [DataMember]
        public int? ActorId { get; set; }

        [DataMember]
        public int? DirectorId { get; set; }

        [DataMember]
        public int NewsCategoryId { get; set; }

    }
}
