using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesDb.Models
{
    public class SampleRep : ISampleRep
    {
        public string Get()
        {
            return "Hello";
        }
    }

    public interface ISampleRep
    {
        string Get();
    }
}