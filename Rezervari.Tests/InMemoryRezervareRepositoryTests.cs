using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System;
using Rezervari; 
using Rezervari.Data; 

namespace Rezervari.Tests
{
    [TestClass]
    public class InMemoryRezervareRepositoryTests
    {
        /// <summary>
        /// Testeaza metoda GetAllRezervari pentru a se asigura ca returneaza toate rezervarile
        /// incluse inițial în repository.
        /// </summary>
        [TestMethod]
        public void GetAllRezervari_ShouldReturnAllInitialReservations()
        {
  
            var repository = new InMemoryRezervareRepository();
            int expectedInitialCount = 4; // Numarul de rezervari adaugate în constructorul InMemoryRezervareRepository
        
            List<Rezervare> rezervari = repository.GetAllRezervari();

            Assert.IsNotNull(rezervari, "Lista de rezervari nu ar trebui sa fie null.");
            Assert.AreEqual(expectedInitialCount, rezervari.Count, $"Ar trebui sa existe {expectedInitialCount} rezervari inițiale.");
        }

        /// <summary>
        /// Testeaza metoda AddRezervare pentru a se asigura ca o noua rezervare este adaugata corect.
        /// </summary>
        [TestMethod]
        public void AddRezervare_ShouldAddNewReservation()
        {
            // Arrange
            var repository = new InMemoryRezervareRepository();
            int initialCount = repository.GetAllRezervari().Count;
            var newRezervare = new Rezervare("Test", "Nou", "0123456789", DateTime.Now.AddDays(1), 2, "Test add");

            // Act
            repository.AddRezervare(newRezervare);
            List<Rezervare> updatedRezervari = repository.GetAllRezervari();

            // Assert
            Assert.AreEqual(initialCount + 1, updatedRezervari.Count, "Numarul de rezervari ar trebui sa creasca cu 1.");
            Assert.IsTrue(updatedRezervari.Contains(newRezervare), "Noua rezervare ar trebui sa fie în lista.");
        }

        /// <summary>
        /// Testeaza metoda DeleteRezervare pentru a se asigura ca o rezervare existenta este ștearsa corect.
        /// </summary>
        [TestMethod]
        public void DeleteRezervare_ShouldRemoveExistingReservation()
        {
            var repository = new InMemoryRezervareRepository();
            // Vom sterge una dintre rezervari
            var rezervareToDelete = repository.GetAllRezervari()
                                             .FirstOrDefault(r => r.NumeClient == "Popescu" && r.PrenumeClient == "Ion");

            Assert.IsNotNull(rezervareToDelete, "Rezervarea de sters ar trebui sa existe initial.");
            int initialCount = repository.GetAllRezervari().Count;

            repository.DeleteRezervare(rezervareToDelete);
            List<Rezervare> updatedRezervari = repository.GetAllRezervari();

            Assert.AreEqual(initialCount - 1, updatedRezervari.Count, "Numarul de rezervari ar trebui sa scada cu 1.");
            Assert.IsFalse(updatedRezervari.Contains(rezervareToDelete), "Rezervarea stearsa nu ar trebui sa mai fie în lista.");
        }

        /// <summary>
        /// Testeaza metoda DeleteRezervare cand se incearca stergerea unei rezervari care nu exista.
        /// Numarul total de rezervari nu ar trebui sa se schimbe.
        /// </summary>
        [TestMethod]
        public void DeleteRezervare_ShouldNotChangeCount_WhenReservationDoesNotExist()
        {
            // Arrange
            var repository = new InMemoryRezervareRepository();
            int initialCount = repository.GetAllRezervari().Count;
            var nonExistingRezervare = new Rezervare("Non", "Existent", "0000000000", DateTime.Now, 1);

            // Act
            repository.DeleteRezervare(nonExistingRezervare);
            List<Rezervare> updatedRezervari = repository.GetAllRezervari();

            // Assert
            Assert.AreEqual(initialCount, updatedRezervari.Count, "Numarul de rezervari nu ar trebui sa se schimbe.");
        }
    }
}
