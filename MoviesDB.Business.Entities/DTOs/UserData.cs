﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDB.Business.Entities.DTOs
{
    [DataContract]
    public class UserData
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Email { get; set; }        

        [DataMember]
        public DateTime LastLoginDate { get; set; }
    }
}