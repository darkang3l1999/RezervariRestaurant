
using System.Collections.Generic;
using Rezervari; 

namespace Rezervari.Data
{

    public interface IRezervareRepository
    {

        List<Rezervare> GetAllRezervari();

        void AddRezervare(Rezervare rezervare);

        void DeleteRezervare(Rezervare rezervare);

    }
}