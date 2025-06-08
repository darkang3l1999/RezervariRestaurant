using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System;
using Rezervari; // Namespace-ul clasei Rezervare
using Rezervari.Data; // Namespace-ul interfeței IRezervareRepository și implementării

namespace Rezervari.Tests
{
    [TestClass]
    public class InMemoryRezervareRepositoryTests
    {
        /// <summary>
        /// Testează metoda GetAllRezervari pentru a se asigura că returnează toate rezervările
        /// incluse inițial în repository.
        /// </summary>
        [TestMethod]
        public void GetAllRezervari_ShouldReturnAllInitialReservations()
        {
            // Arrange (Aranjare)
            var repository = new InMemoryRezervareRepository();
            int expectedInitialCount = 4; // Numărul de rezervări adăugate în constructorul InMemoryRezervareRepository

            // Act (Acțiune)
            List<Rezervare> rezervari = repository.GetAllRezervari();

            // Assert (Asertare)
            Assert.IsNotNull(rezervari, "Lista de rezervări nu ar trebui să fie null.");
            Assert.AreEqual(expectedInitialCount, rezervari.Count, $"Ar trebui să existe {expectedInitialCount} rezervări inițiale.");
        }

        /// <summary>
        /// Testează metoda AddRezervare pentru a se asigura că o nouă rezervare este adăugată corect.
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
            Assert.AreEqual(initialCount + 1, updatedRezervari.Count, "Numărul de rezervări ar trebui să crească cu 1.");
            Assert.IsTrue(updatedRezervari.Contains(newRezervare), "Noua rezervare ar trebui să fie în listă.");
        }

        /// <summary>
        /// Testează metoda DeleteRezervare pentru a se asigura că o rezervare existentă este ștearsă corect.
        /// </summary>
        [TestMethod]
        public void DeleteRezervare_ShouldRemoveExistingReservation()
        {
            // Arrange
            var repository = new InMemoryRezervareRepository();
            // Vom șterge una dintre rezervările inițiale (ex: "Popescu Ion")
            var rezervareToDelete = repository.GetAllRezervari()
                                             .FirstOrDefault(r => r.NumeClient == "Popescu" && r.PrenumeClient == "Ion");

            Assert.IsNotNull(rezervareToDelete, "Rezervarea de șters ar trebui să existe inițial.");
            int initialCount = repository.GetAllRezervari().Count;

            // Act
            repository.DeleteRezervare(rezervareToDelete);
            List<Rezervare> updatedRezervari = repository.GetAllRezervari();

            // Assert
            Assert.AreEqual(initialCount - 1, updatedRezervari.Count, "Numărul de rezervări ar trebui să scadă cu 1.");
            Assert.IsFalse(updatedRezervari.Contains(rezervareToDelete), "Rezervarea ștearsă nu ar trebui să mai fie în listă.");
        }

        /// <summary>
        /// Testează metoda DeleteRezervare când se încearcă ștergerea unei rezervări care nu există.
        /// Numărul total de rezervări nu ar trebui să se schimbe.
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
            Assert.AreEqual(initialCount, updatedRezervari.Count, "Numărul de rezervări nu ar trebui să se schimbe.");
        }
    }
}
