using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Contracts
{
    public interface IBusinessEngineFactory
    {
        T GetBusinessEngine<T>() where T : IBusinessEngine;
    }
}

// Factory class will retrieve a concrete class when given an abstraction. 
// AbstractFactory does the same thing but actually abstracts the factory out in the it's own interface. 
// So you can have more than one factory.
//
// Bu class'a verilecek olan her bir I...Engine, aynı interface'i implement ediyor: (IBusinessEngine) 
// İşin püf noktası bu.
