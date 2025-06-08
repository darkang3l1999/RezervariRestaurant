using System.Collections.Generic;
using System.Linq; // Required for IOrderedEnumerable
using Rezervari; // Ensure this namespace is correctly referenced

namespace Rezervari.BusinessLogic
{
    // This interface specifies the method that any reservation sorting strategy must implement.
    public interface IRezervareSortStrategy
    {
        /// <summary>
        /// Sorts a given list of reservations according to a specific algorithm.
        /// </summary>
        /// <param name="rezervari">The list of reservations to sort.</param>
        /// <returns>A new list of sorted Rezervare objects.</returns>
        List<Rezervare> Sort(List<Rezervare> rezervari);

        /// <summary>
        /// Gets a display name for the sorting strategy (e.g., "Sort by Date Ascending").
        /// </summary>
        string Name { get; }
    }
}
