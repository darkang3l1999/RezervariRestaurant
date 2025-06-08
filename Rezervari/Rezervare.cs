using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Rezervare
{
    // Proprietăți pentru detalii client și rezervare
    public string NumeClient { get; set; }
    public string PrenumeClient { get; set; }
    public string NumarTelefon { get; set; }
    public DateTime DataOra { get; set; }
    public int NumarPersoane { get; set; }
    public string Observatii { get; set; } // Câmp opțional pentru notițe suplimentare

    /// <summary>
    /// Constructor pentru a crea o nouă instanță de Rezervare.
    /// </summary>
    /// <param name="numeClient">Numele de familie al clientului.</param>
    /// <param name="prenumeClient">Prenumele clientului.</param>
    /// <param name="numarTelefon">Numărul de telefon al clientului.</param>
    /// <param name="dataOra">Data și ora rezervării.</param>
    /// <param name="numarPersoane">Numărul de persoane pentru rezervare.</param>
    /// <param name="observatii">Observații suplimentare despre rezervare (opțional).</param>
    public Rezervare(string numeClient, string prenumeClient, string numarTelefon, DateTime dataOra, int numarPersoane, string observatii = "")
    {
        NumeClient = numeClient;
        PrenumeClient = prenumeClient;
        NumarTelefon = numarTelefon;
        DataOra = dataOra;
        NumarPersoane = numarPersoane;
        Observatii = observatii;
    }

    /// <summary>
    /// Suprascrie metoda ToString() pentru a oferi o reprezentare text prietenoasă a rezervării,
    /// utilă pentru afișarea în controalele UI precum ListBox sau pentru debugging.
    /// </summary>
    /// <returns>Un string formatat care reprezintă rezervarea.</returns>
    public override string ToString()
    {
        return $"{DataOra:dd/MM/yyyy HH:mm} - {NumeClient} {PrenumeClient} ({NumarPersoane} pers.)";
    }
}