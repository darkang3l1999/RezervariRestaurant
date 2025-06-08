using System.Collections.Generic;
using System.Linq; 
using Rezervari; 

namespace Rezervari.Data
{

    public class InMemoryRezervareRepository : IRezervareRepository
    {
        
        private List<Rezervare> _rezervari;

     
 
      
        public InMemoryRezervareRepository()
        {
            _rezervari = new List<Rezervare>();
           
            _rezervari.Add(new Rezervare("Popescu", "Ion", "0722111222", new System.DateTime(2025, 6, 15, 18, 0, 0), 4, "Window seat"));
            _rezervari.Add(new Rezervare("Ionescu", "Ana", "0744333444", new System.DateTime(2025, 6, 15, 20, 30, 0), 2));
            _rezervari.Add(new Rezervare("Georgescu", "Dan", "0766555666", new System.DateTime(2025, 6, 16, 19, 0, 0), 3, "Baby chair needed"));
            _rezervari.Add(new Rezervare("Vasile", "Maria", "0755888999", new System.DateTime(2025, 6, 15, 21, 0, 0), 5)); 
        }

        public List<Rezervare> GetAllRezervari()
        {
            return _rezervari.ToList();
        }


        public void AddRezervare(Rezervare rezervare)
        {
            _rezervari.Add(rezervare);
        }

        public void DeleteRezervare(Rezervare rezervare)
        {

            _rezervari.RemoveAll(r => r.NumeClient == rezervare.NumeClient &&
                                       r.PrenumeClient == rezervare.PrenumeClient &&
                                       r.DataOra == rezervare.DataOra);

        }
    }
}
