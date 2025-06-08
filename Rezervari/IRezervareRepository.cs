
using System.Collections.Generic;
using Rezervari; // Ensure this namespace is correctly referenced

namespace Rezervari.Data
{
    // This interface specifies the methods that any reservation repository
    // (e.g., in-memory, database, file-based) must implement.
    public interface IRezervareRepository
    {
        /// <summary>
        /// Retrieves all reservations from the repository.
        /// </summary>
        /// <returns>A list of all Rezervare objects.</returns>
        List<Rezervare> GetAllRezervari();

        /// <summary>
        /// Adds a new reservation to the repository.
        /// </summary>
        /// <param name="rezervare">The Rezervare object to add.</param>
        void AddRezervare(Rezervare rezervare);

        /// <summary>
        /// Deletes a specific reservation from the repository.
        /// </summary>
        /// <param name="rezervare">The Rezervare object to delete.</param>
        void DeleteRezervare(Rezervare rezervare);

        // Note: For a real-world scenario, you might add methods like:
        // Rezervare GetRezervareById(int id);
        // void UpdateRezervare(Rezervare rezervare);
    }
}