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
    public class NewsCreationData : ObjectBase
    {
        class NewsCreationDataValidator : AbstractValidator<NewsCreationData>
        {
            public NewsCreationDataValidator()
            {
                
            }
        }

        protected override IValidator GetValidator()
        {
            return new NewsCreationDataValidator();
        }
    }
}
