using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDB.Business.Entities.DTOs
{
    [DataContract]
    public class StatsMovieGenre
    {
        [DataMember]
        public string Genre { get; set; }

        [DataMember]
        public int Count { get; set; }
    }
}
