using System.Collections.Generic;
using System.Linq; // Required for ToList() and other LINQ operations
using Rezervari; // Ensure this namespace is correctly referenced

namespace Rezervari.Data
{
    // This class implements the IRezervareRepository interface,
    // managing reservations using a simple List<Rezervare> in memory.
    public class InMemoryRezervareRepository : IRezervareRepository
    {
        // The private list to store reservations. This is the "in-memory database".
        private List<Rezervare> _rezervari;

        /// <summary>
        /// Constructor for InMemoryRezervareRepository.
        /// Initializes the internal list and adds some sample data.
        /// </summary>
        public InMemoryRezervareRepository()
        {
            _rezervari = new List<Rezervare>();
            // Add some initial reservations for testing the repository.
            _rezervari.Add(new Rezervare("Popescu", "Ion", "0722111222", new System.DateTime(2025, 6, 15, 18, 0, 0), 4, "Window seat"));
            _rezervari.Add(new Rezervare("Ionescu", "Ana", "0744333444", new System.DateTime(2025, 6, 15, 20, 30, 0), 2));
            _rezervari.Add(new Rezervare("Georgescu", "Dan", "0766555666", new System.DateTime(2025, 6, 16, 19, 0, 0), 3, "Baby chair needed"));
            _rezervari.Add(new Rezervare("Vasile", "Maria", "0755888999", new System.DateTime(2025, 6, 15, 21, 0, 0), 5)); // Second reservation on 15.06
        }

        /// <summary>
        /// Retrieves all reservations from the in-memory list.
        /// Returns a new list to prevent external modification of the internal list.
        /// </summary>
        /// <returns>A list of all Rezervare objects.</returns>
        public List<Rezervare> GetAllRezervari()
        {
            // Return a copy to ensure encapsulation and prevent direct modification of the internal list.
            return _rezervari.ToList();
        }

        /// <summary>
        /// Adds a new reservation to the in-memory list.
        /// </summary>
        /// <param name="rezervare">The Rezervare object to add.</param>
        public void AddRezervare(Rezervare rezervare)
        {
            _rezervari.Add(rezervare);
        }

        /// <summary>
        /// Deletes a specific reservation from the in-memory list.
        /// </summary>
        /// <param name="rezervare">The Rezervare object to delete.</param>
        public void DeleteRezervare(Rezervare rezervare)
        {
            // Find and remove the reservation.
            // Using RemoveAll with a predicate is efficient for objects.
            _rezervari.RemoveAll(r => r.NumeClient == rezervare.NumeClient &&
                                       r.PrenumeClient == rezervare.PrenumeClient &&
                                       r.DataOra == rezervare.DataOra);
            // Note: For robust deletion, especially in a real app, you'd use a unique ID.
            // For now, matching by name, surname, and datetime is sufficient.
        }
    }
}
