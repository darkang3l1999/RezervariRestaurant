using Microsoft.VisualStudio.TestTools.UnitTesting; 
using System;
using Rezervari; 

namespace Rezervari.Tests
{
    [TestClass] 
    public class RezervareTests
    {
   
        /// Testeaza constructorul clasei Rezervare pentru a asigura ca proprietatile sunt inițializate corect

        [TestMethod]
        public void RezervareConstructor_ShouldSetPropertiesCorrectly()
        {
     
            string numeClient = "Popescu";
            string prenumeClient = "Ion";
            string numarTelefon = "0722123456";
            DateTime dataOra = new DateTime(2025, 12, 25, 19, 0, 0); 
            int numarPersoane = 4;
            string observatii = "Masa la fereastra";

       
            Rezervare rezervare = new Rezervare(numeClient, prenumeClient, numarTelefon, dataOra, numarPersoane, observatii);

           
            Assert.AreEqual(numeClient, rezervare.NumeClient, "NumeClient nu a fost setat corect.");
            Assert.AreEqual(prenumeClient, rezervare.PrenumeClient, "PrenumeClient nu a fost setat corect.");
            Assert.AreEqual(numarTelefon, rezervare.NumarTelefon, "NumarTelefon nu a fost setat corect.");
            Assert.AreEqual(dataOra, rezervare.DataOra, "DataOra nu a fost setata corect.");
            Assert.AreEqual(numarPersoane, rezervare.NumarPersoane, "NumarPersoane nu a fost setat corect.");
            Assert.AreEqual(observatii, rezervare.Observatii, "Observatii nu a fost setat corect.");
        }

       
        /// Testeaza constructorul clasei Rezervare atunci cand campul Observatii este omis (valoare implicita).
        [TestMethod]
        public void RezervareConstructor_ShouldHandleDefaultObservations()
        {
            // Arrange
            string numeClient = "Georgescu";
            string prenumeClient = "Maria";
            string numarTelefon = "0711223344";
            DateTime dataOra = new DateTime(2025, 7, 10, 18, 0, 0);
            int numarPersoane = 2;
         

           
            Rezervare rezervare = new Rezervare(numeClient, prenumeClient, numarTelefon, dataOra, numarPersoane);

      
            Assert.AreEqual(numeClient, rezervare.NumeClient, "NumeClient nu a fost setat corect.");
            Assert.AreEqual(prenumeClient, rezervare.PrenumeClient, "PrenumeClient nu a fost setat corect.");
            Assert.AreEqual(numarTelefon, rezervare.NumarTelefon, "NumarTelefon nu a fost setat corect.");
            Assert.AreEqual(dataOra, rezervare.DataOra, "DataOra nu a fost setata corect.");
            Assert.AreEqual(numarPersoane, rezervare.NumarPersoane, "NumarPersoane nu a fost setat corect.");
            Assert.AreEqual("", rezervare.Observatii, "Observatii ar trebui sa fie un string gol.");
        }

 
        /// Testează metoda ToString() a clasei Rezervare.
     
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
