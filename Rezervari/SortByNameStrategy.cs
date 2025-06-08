using System.Collections.Generic;
using System.Linq; 
using Rezervari; 

namespace Rezervari.BusinessLogic
{
   
    public class SortByNameStrategy : IRezervareSortStrategy
    {
        public string Name => "Nume Client"; 

        public List<Rezervare> Sort(List<Rezervare> rezervari)
        {
       
            return rezervari.OrderBy(r => r.NumeClient)
                             .ThenBy(r => r.PrenumeClient)
                             .ToList();
        }
    }
}