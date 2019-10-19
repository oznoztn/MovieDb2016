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
    public class MovieData : ObjectBase
    {
        class MovieDataValidator : AbstractValidator<MovieData>
        {
            public MovieDataValidator()
            {
                
            }
        }

        protected override IValidator GetValidator()
        {
            return new MovieDataValidator();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
