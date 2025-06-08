using System.Collections.Generic;
using System.Linq; 
using Rezervari; 

namespace Rezervari.BusinessLogic
{
   
    public class SortByDateDescendingStrategy : IRezervareSortStrategy
    {
        public string Name => "Data (Descrescator)"; 


        public List<Rezervare> Sort(List<Rezervare> rezervari)
        {
            return rezervari.OrderByDescending(r => r.DataOra).ToList();
        }
    }
}
