using Microsoft.VisualStudio.TestTools.UnitTesting; // Asigură-te că ai această referință
using System;
using Rezervari; // Asigură-te că ai adăugat referința la proiectul Rezervari

namespace Rezervari.Tests
{
    [TestClass] // Marchează această clasă ca o clasă de testare
    public class RezervareTests
    {
        /// <summary>
        /// Testează constructorul clasei Rezervare pentru a asigura că proprietățile
        /// sunt inițializate corect cu valorile furnizate.
        /// </summary>
        [TestMethod] // Marchează această metodă ca o metodă de test
        public void RezervareConstructor_ShouldSetPropertiesCorrectly()
        {
            // Arrange (Aranjare): Pregătește datele de intrare pentru test.
            string numeClient = "Popescu";
            string prenumeClient = "Ion";
            string numarTelefon = "0722123456";
            DateTime dataOra = new DateTime(2025, 12, 25, 19, 0, 0); // O dată și oră specifică
            int numarPersoane = 4;
            string observatii = "Masa la fereastra";

            // Act (Acțiune): Execută codul pe care vrei să-l testezi.
            // Aici, apelăm constructorul clasei Rezervare.
            Rezervare rezervare = new Rezervare(numeClient, prenumeClient, numarTelefon, dataOra, numarPersoane, observatii);

            // Assert (Asertare): Verifică dacă rezultatul acțiunii este cel așteptat.
            // Folosim Assert.AreEqual pentru a compara valorile așteptate cu cele reale.
            Assert.AreEqual(numeClient, rezervare.NumeClient, "NumeClient nu a fost setat corect.");
            Assert.AreEqual(prenumeClient, rezervare.PrenumeClient, "PrenumeClient nu a fost setat corect.");
            Assert.AreEqual(numarTelefon, rezervare.NumarTelefon, "NumarTelefon nu a fost setat corect.");
            Assert.AreEqual(dataOra, rezervare.DataOra, "DataOra nu a fost setată corect.");
            Assert.AreEqual(numarPersoane, rezervare.NumarPersoane, "NumarPersoane nu a fost setat corect.");
            Assert.AreEqual(observatii, rezervare.Observatii, "Observatii nu a fost setat corect.");
        }

        /// <summary>
        /// Testează constructorul clasei Rezervare atunci când câmpul Observatii este omis (valoare implicită).
        /// </summary>
        [TestMethod]
        public void RezervareConstructor_ShouldHandleDefaultObservations()
        {
            // Arrange
            string numeClient = "Georgescu";
            string prenumeClient = "Maria";
            string numarTelefon = "0711223344";
            DateTime dataOra = new DateTime(2025, 7, 10, 18, 0, 0);
            int numarPersoane = 2;
            // Observatii este omis, deci va folosi valoarea implicită ""

            // Act
            Rezervare rezervare = new Rezervare(numeClient, prenumeClient, numarTelefon, dataOra, numarPersoane);

            // Assert
            Assert.AreEqual(numeClient, rezervare.NumeClient, "NumeClient nu a fost setat corect.");
            Assert.AreEqual(prenumeClient, rezervare.PrenumeClient, "PrenumeClient nu a fost setat corect.");
            Assert.AreEqual(numarTelefon, rezervare.NumarTelefon, "NumarTelefon nu a fost setat corect.");
            Assert.AreEqual(dataOra, rezervare.DataOra, "DataOra nu a fost setată corect.");
            Assert.AreEqual(numarPersoane, rezervare.NumarPersoane, "NumarPersoane nu a fost setat corect.");
            Assert.AreEqual("", rezervare.Observatii, "Observatii ar trebui să fie un string gol."); // Verificăm că e "" implicit
        }

        /// <summary>
        /// Testează metoda ToString() a clasei Rezervare.
        /// </summary>
        [TestMethod]
        public void Rezervare_ToString_ShouldReturnFormattedString()
        {
            // Arrange
            string numeClient = "Popescu";
            string prenumeClient = "Ion";
            string numarTelefon = "0722123456";
            DateTime dataOra = new DateTime(2025, 6, 15, 18, 0, 0);
            int numarPersoane = 4;
            string observatii = "Masa la fereastra";
            Rezervare rezervare = new Rezervare(numeClient, prenumeClient, numarTelefon, dataOra, numarPersoane, observatii);

            string expectedString = "15/06/2025 18:00 - Popescu Ion (4 pers.)";

            // Act
            string actualString = rezervare.ToString();

            // Assert
            Assert.AreEqual(expectedString, actualString, "Metoda ToString() nu returnează string-ul formatat corect.");
        }
    }
}
