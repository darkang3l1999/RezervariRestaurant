using System.Collections.Generic;
using System.Linq; // Required for OrderByDescending
using Rezervari; // Ensure this namespace is correctly referenced

namespace Rezervari.BusinessLogic
{
    // This class sorts reservations by their DataOra property in descending order.
    public class SortByDateDescendingStrategy : IRezervareSortStrategy
    {
        public string Name => "Data (Descrescator)"; // Display name for this strategy

        /// <summary>
        /// Sorts the list of reservations by DataOra in descending order.
        /// </summary>
        /// <param name="rezervari">The list of reservations to sort.</param>
        /// <returns>A new list of sorted Rezervare objects.</returns>
        public List<Rezervare> Sort(List<Rezervare> rezervari)
        {
            return rezervari.OrderByDescending(r => r.DataOra).ToList();
        }
    }
}
