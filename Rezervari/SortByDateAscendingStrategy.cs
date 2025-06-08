using System.Collections.Generic;
using System.Linq; // Required for OrderBy
using Rezervari; // Ensure this namespace is correctly referenced

namespace Rezervari.BusinessLogic
{
    // This class sorts reservations by their DataOra property in ascending order.
    public class SortByDateAscendingStrategy : IRezervareSortStrategy
    {
        public string Name => "Data (Crescator)"; // Display name for this strategy

        /// <summary>
        /// Sorts the list of reservations by DataOra in ascending order.
        /// </summary>
        /// <param name="rezervari">The list of reservations to sort.</param>
        /// <returns>A new list of sorted Rezervare objects.</returns>
        public List<Rezervare> Sort(List<Rezervare> rezervari)
        {
            return rezervari.OrderBy(r => r.DataOra).ToList();
        }
    }
}
