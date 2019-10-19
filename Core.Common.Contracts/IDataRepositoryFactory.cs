using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Contracts
{
    public interface IDataRepositoryFactory
    {
        T GetDataRepository<T>() where T : IDataRepository;
    }
}

// Factory class will retrieve a concrete class when given an abstraction. 
// AbstractFactory does the same thing but actually abstracts the factory out in the it's own interface. 
// So you can have more than one factory.
//
// Bu class'a verilecek olan her bir I...Repository, aynı interface'i implement ediyor: (IDataRepository) 
// İşin püf noktası bu.

