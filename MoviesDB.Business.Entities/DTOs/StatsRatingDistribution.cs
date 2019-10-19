using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDB.Business.Entities.DTOs
{
    [DataContract]
    public class StatsRatingDistribution
    {
        [DataMember]
        public int Rate { get; set; }

        [DataMember]
        public int Count { get; set; }
    }
}
