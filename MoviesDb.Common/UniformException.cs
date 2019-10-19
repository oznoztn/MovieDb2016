using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDb.Common
{
    [DataContract]
    public class UniformException
    {
        [DataMember]
        public string Reason { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}
