using System.Collections.Generic;
using System.Linq; // Required for OrderBy and ThenBy
using Rezervari; // Ensure this namespace is correctly referenced

namespace Rezervari.BusinessLogic
{
    // This class sorts reservations primarily by NumeClient, then by PrenumeClient.
    public class SortByNameStrategy : IRezervareSortStrategy
    {
        public string Name => "Nume Client"; // Display name for this strategy

        /// <summary>
        /// Sorts the list of reservations by NumeClient, then by PrenumeClient.
        /// </summary>
        /// <param name="rezervari">The list of reservations to sort.</param>
        /// <returns>A new list of sorted Rezervare objects.</returns>
        public List<Rezervare> Sort(List<Rezervare> rezervari)
        {
            // Sort by last name, then by first name for secondary sorting.
            return rezervari.OrderBy(r => r.NumeClient)
                             .ThenBy(r => r.PrenumeClient)
                             .ToList();
        }
    }
}