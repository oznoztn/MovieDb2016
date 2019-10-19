using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Common.Core;
using FluentValidation;

namespace MoviesDB.Client.Entities
{
    [DataContract]
    public class Genre : ObjectBase
    {
        class GenreValidator : AbstractValidator<Genre>
        {

        }

        protected override IValidator GetValidator()
        {
            return new GenreValidator();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public bool IsSubGenre { get; set; }

        [IgnoreDataMember]
        public ICollection<Movie> Movies { get; set; }
    }
}
