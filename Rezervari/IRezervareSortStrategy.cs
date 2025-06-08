using System.Collections.Generic;
using System.Linq; 
using Rezervari; 

namespace Rezervari.BusinessLogic
{

    public interface IRezervareSortStrategy
    {
        List<Rezervare> Sort(List<Rezervare> rezervari);
        string Name { get; }
    }
}
