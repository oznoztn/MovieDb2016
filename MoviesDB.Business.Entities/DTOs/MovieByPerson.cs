using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDB.Business.Entities.DTOs
{
    [DataContract]
    public class MovieByPerson
    {
        public string PersonName { get; set; }
        public string MovieName { get; set; }
        public string MoviePoster { get; set; }
    }
}
