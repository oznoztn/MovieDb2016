using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDB.Client.Entities.DTOs
{
    [DataContract]
    public class StatsRatingDistribution
    {
        public int Rate { get; set; }
        public int Count { get; set; }
    }
}
