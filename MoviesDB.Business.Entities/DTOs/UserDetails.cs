﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDB.Business.Entities.DTOs
{
    [DataContract]
    public class UserDetails
    {
        [DataMember]
        public int Id { get; set; }
    }
}
