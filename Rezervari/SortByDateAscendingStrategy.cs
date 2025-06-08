using System.Collections.Generic;
using System.Linq; 
using Rezervari; 

namespace Rezervari.BusinessLogic
{
 
    public class SortByDateAscendingStrategy : IRezervareSortStrategy
    {
        public string Name => "Data (Crescator)";


        public List<Rezervare> Sort(List<Rezervare> rezervari)
        {
            return rezervari.OrderBy(r => r.DataOra).ToList();
        }
    }
}
