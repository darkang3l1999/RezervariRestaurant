using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System;
using Rezervari; // Namespace-ul clasei Rezervare
using Rezervari.BusinessLogic; // Namespace-ul strategiilor de sortare

namespace Rezervari.Tests
{
    [TestClass]
    public class RezervareSortStrategyTests
    {
        // Helper method to create a sample list of reservations for testing.
        private List<Rezervare> GetSampleReservations()
        {
            return new List<Rezervare>
            {
                new Rezervare("Popescu", "Ion", "0722111222", new DateTime(2025, 6, 17, 18, 0, 0), 4),
                new Rezervare("Ionescu", "Ana", "0744333444", new DateTime(2025, 6, 15, 20, 30, 0), 2),
                new Rezervare("Georgescu", "Dan", "0766555666", new DateTime(2025, 6, 16, 19, 0, 0), 3),
                new Rezervare("Andrei", "Bogdan", "0755888999", new DateTime(2025, 6, 15, 17, 0, 0), 5), // Same date as Ionescu
                new Rezervare("Popescu", "Vlad", "0700111222", new DateTime(2025, 6, 17, 10, 0, 0), 1) // Same date as Popescu Ion
            };
        }

        /// <summary>
        /// Testează strategia de sortare după dată în ordine crescătoare.
        /// </summary>
        [TestMethod]
        public void SortByDateAscendingStrategy_ShouldSortByDateAscending()
        {
            // Arrange
            var strategy = new SortByDateAscendingStrategy();
            List<Rezervare> reservations = GetSampleReservations();

            // Act
            List<Rezervare> sortedReservations = strategy.Sort(reservations);

            // Assert
            // Verificăm dacă lista sortată este în ordine crescătoare după DataOra.
            // Putem itera și compara fiecare element cu următorul.
            for (int i = 0; i < sortedReservations.Count - 1; i++)
            {
                Assert.IsTrue(sortedReservations[i].DataOra <= sortedReservations[i + 1].DataOra,
                              $"Rezervarea la index {i} ({sortedReservations[i].DataOra}) nu este înainte sau la fel cu index {i + 1} ({sortedReservations[i + 1].DataOra})");
            }
        }

        /// <summary>
        /// Testează strategia de sortare după dată în ordine descrescătoare.
        /// </summary>
        [TestMethod]
        public void SortByDateDescendingStrategy_ShouldSortByDateDescending()
        {
            // Arrange
            var strategy = new SortByDateDescendingStrategy();
            List<Rezervare> reservations = GetSampleReservations();

            // Act
            List<Rezervare> sortedReservations = strategy.Sort(reservations);

            // Assert
            // Verificăm dacă lista sortată este în ordine descrescătoare după DataOra.
            for (int i = 0; i < sortedReservations.Count - 1; i++)
            {
                Assert.IsTrue(sortedReservations[i].DataOra >= sortedReservations[i + 1].DataOra,
                              $"Rezervarea la index {i} ({sortedReservations[i].DataOra}) nu este după sau la fel cu index {i + 1} ({sortedReservations[i + 1].DataOra})");
            }
        }

        /// <summary>
        /// Testează strategia de sortare după nume client (alfabetic, apoi prenume).
        /// </summary>
        [TestMethod]
        public void SortByNameStrategy_ShouldSortByNameThenFirstName()
        {
            // Arrange
            var strategy = new SortByNameStrategy();
            List<Rezervare> reservations = GetSampleReservations();

            // Act
            List<Rezervare> sortedReservations = strategy.Sort(reservations);

            // Assert
            // Verificăm ordinea alfabetică după NumeClient, apoi după PrenumeClient.
            Assert.AreEqual("Andrei", sortedReservations[0].NumeClient);
            Assert.AreEqual("Bogdan", sortedReservations[0].PrenumeClient);

            Assert.AreEqual("Georgescu", sortedReservations[1].NumeClient);
            Assert.AreEqual("Dan", sortedReservations[1].PrenumeClient);

            Assert.AreEqual("Ionescu", sortedReservations[2].NumeClient);
            Assert.AreEqual("Ana", sortedReservations[2].PrenumeClient);

            Assert.AreEqual("Popescu", sortedReservations[3].NumeClient);
            Assert.AreEqual("Ion", sortedReservations[3].PrenumeClient); // Ion înainte de Vlad

            Assert.AreEqual("Popescu", sortedReservations[4].NumeClient);
            Assert.AreEqual("Vlad", sortedReservations[4].PrenumeClient);
        }
    }
}
