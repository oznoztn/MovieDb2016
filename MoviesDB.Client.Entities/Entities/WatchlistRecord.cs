using System.Runtime.Serialization;
using Core.Common.Core;
using FluentValidation;

namespace MoviesDB.Client.Entities
{
    public class WatchlistRecord : ObjectBase
    {
        [DataContract]
        class WatchlistRecordValidator : AbstractValidator<WatchlistRecord>
        {

        }

        protected override IValidator GetValidator()
        {
            return new WatchlistRecordValidator();
        }
    }
}
