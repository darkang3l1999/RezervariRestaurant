using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Rezervare
{
    
    public string NumeClient { get; set; }
    public string PrenumeClient { get; set; }
    public string NumarTelefon { get; set; }
    public DateTime DataOra { get; set; }
    public int NumarPersoane { get; set; }
    public string Observatii { get; set; } 

    public Rezervare(string numeClient, string prenumeClient, string numarTelefon, DateTime dataOra, int numarPersoane, string observatii = "")
    {
        NumeClient = numeClient;
        PrenumeClient = prenumeClient;
        NumarTelefon = numarTelefon;
        DataOra = dataOra;
        NumarPersoane = numarPersoane;
        Observatii = observatii;
    }


    public override string ToString()
    {
        return $"{DataOra:dd/MM/yyyy HH:mm} - {NumeClient} {PrenumeClient} ({NumarPersoane} pers.)";
    }
}