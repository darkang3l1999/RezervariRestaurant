using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq; // Necesit pentru ToList() și operații de filtrare/sortare
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rezervari
{
    public partial class Form1 : Form
    {
        // Lista internă care va stoca toate rezervările curente în memorie.
        // Aceasta va servi ca sursă de date pentru DataGridView.
        private List<Rezervare> rezervari;

        /// <summary>
        /// Constructorul principal al formei.
        /// Inițializează componentele UI și logica de bază a aplicației.
        /// </summary>
        public Form1()
        {
            InitializeComponent(); // Inițializează controalele generate de designer
            rezervari = new List<Rezervare>(); // Inițializează lista de rezervări

            // Opțional: Adaugă câteva rezervări inițiale pentru testare rapidă la pornirea aplicației.
            rezervari.Add(new Rezervare("Popescu", "Ion", "0722111222", new DateTime(2025, 6, 15, 18, 0, 0), 4, "Masa la fereastra"));
            rezervari.Add(new Rezervare("Ionescu", "Ana", "0744333444", new DateTime(2025, 6, 15, 20, 30, 0), 2));
            rezervari.Add(new Rezervare("Georgescu", "Dan", "0766555666", new DateTime(2025, 6, 16, 19, 0, 0), 3, "Cu scaun de bebelus"));
            rezervari.Add(new Rezervare("Vasile", "Maria", "0755888999", new DateTime(2025, 6, 15, 21, 0, 0), 5)); // A doua rezervare pe 15.06

            // Setează DataGridView-urile să genereze automat coloane bazat pe proprietățile clasei Rezervare.
            dataGridViewRezervari.AutoGenerateColumns = true;
            dataGridViewAnaliza.AutoGenerateColumns = true;

            // Inițializează DataPicker-ul de filtrare la data curentă.
            dateTimePickerFiltruData.Value = DateTime.Today; // Setează la începutul zilei curente

            // Încarcă inițial rezervările în ambele DataGridView-uri la pornirea aplicației.
            IncarcaRezervariInDataGridView();
        }

        /// <summary>
        /// Gestionează evenimentul de click pentru butonul "Adaugă Rezervare".
        /// Colectează datele introduse de utilizator, efectuează validări simple
        /// și adaugă o nouă rezervare la lista internă.
        /// </summary>
        private void btnAdaugaRezervare_Click(object sender, EventArgs e)
        {
            try
            {
                // Validare minimă a câmpurilor esențiale pentru o rezervare.
                if (string.IsNullOrWhiteSpace(txtNumeClient.Text) ||
                    string.IsNullOrWhiteSpace(txtPrenumeClient.Text) ||
                    numUpDownNrPersoane.Value <= 0)
                {
                    MessageBox.Show("Nume, prenume client și număr persoane sunt obligatorii!", "Eroare Validare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Oprește execuția dacă validarea eșuează
                }

                // Creează o nouă instanță de Rezervare cu datele introduse în controalele UI.
                Rezervare nouaRezervare = new Rezervare(
                    txtNumeClient.Text,
                    txtPrenumeClient.Text,
                    txtNumarTelefon.Text,
                    dateTimePickerDataOra.Value,
                    (int)numUpDownNrPersoane.Value,
                    txtObservatii.Text
                );

                // Adaugă noua rezervare la lista internă.
                rezervari.Add(nouaRezervare);

                // Reîmprospătează conținutul ambelor DataGridView-uri pentru a reflecta modificarea.
                IncarcaRezervariInDataGridView();

                // Curăță câmpurile de input pentru a pregăti pentru o nouă adăugare.
                GolesteCampuri();

                MessageBox.Show("Rezervare adăugată cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Afișează un mesaj de eroare prietenos în cazul unei excepții neașteptate.
                MessageBox.Show($"A apărut o eroare la adăugarea rezervării: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Gestionează evenimentul de click pentru butonul "Șterge Rezervare".
        /// Șterge rezervarea selectată din DataGridView-ul principal.
        /// </summary>
        private void btnStergeRezervare_Click(object sender, EventArgs e)
        {
            // Verifică dacă există cel puțin un rând selectat în DataGridView.
            if (dataGridViewRezervari.SelectedRows.Count > 0)
            {
                // Solicită confirmare utilizatorului înainte de a șterge definitiv o rezervare.
                var confirmResult = MessageBox.Show("Ești sigur că vrei să ștergi această rezervare?", "Confirmare Ștergere", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmResult == DialogResult.Yes)
                {
                    // Obține obiectul Rezervare care este legat de rândul selectat în DataGridView.
                    // DataBoundItem returnează obiectul sursă al rândului.
                    Rezervare rezervareDeSters = dataGridViewRezervari.SelectedRows[0].DataBoundItem as Rezervare;

                    if (rezervareDeSters != null)
                    {
                        // Elimină rezervarea din lista internă.
                        rezervari.Remove(rezervareDeSters);

                        // Reîmprospătează ambele DataGridView-uri pentru a arăta că rezervarea a fost ștearsă.
                        IncarcaRezervariInDataGridView();

                        MessageBox.Show("Rezervare ștearsă cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                // Anunță utilizatorul dacă nu a fost selectată nicio rezervare pentru ștergere.
                MessageBox.Show("Selectează o rezervare pentru a o șterge.", "Avertisment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Reîmprospătează datele afișate în ambele DataGridView-uri (Gestionare Rezervări și Panou de Analiză).
        /// Această metodă este apelată după fiecare operație de adăugare sau ștergere.
        /// Include logica de filtrare pentru Panoul de Analiză.
        /// </summary>
        private void IncarcaRezervariInDataGridView()
        {
            // Reîmprospătează DataGridView-ul pentru Gestionare Rezervări
            dataGridViewRezervari.DataSource = null;
            dataGridViewRezervari.DataSource = rezervari.ToList();

            // Logica de filtrare pentru Panoul de Analiză
            List<Rezervare> rezervariFiltrate = new List<Rezervare>(rezervari); // Creează o copie a listei complete

            // Verifică dacă checkbox-ul de filtrare este bifat (dacă vom adăuga unul)
            // Momentan, filtram doar pe baza valorii din dateTimePickerFiltruData
            DateTime dataSelectata = dateTimePickerFiltruData.Value.Date; // Doar data, fără ora

            // Filtrează rezervările care se potrivesc cu data selectată
            rezervariFiltrate = rezervariFiltrate
                .Where(r => r.DataOra.Date == dataSelectata)
                .ToList();

            // Reîmprospătează DataGridView-ul pentru Panoul de Analiză cu rezervările filtrate
            dataGridViewAnaliza.DataSource = null;
            dataGridViewAnaliza.DataSource = rezervariFiltrate;
        }

        /// <summary>
        /// Gestionează evenimentul de click pentru butonul "Aplică Filtru" din Panoul de Analiză.
        /// Reîncarcă rezervările în DataGridView-ul de analiză, aplicând filtrul de dată.
        /// </summary>
        private void btnAplicaFiltru_Click(object sender, EventArgs e)
        {
            IncarcaRezervariInDataGridView(); // Metoda va aplica automat filtrul
        }

        /// <summary>
        /// Gestionează evenimentul de click pentru butonul "Anulează Filtru" din Panoul de Analiză.
        /// Resetează DataPicker-ul la data curentă și reîncarcă toate rezervările.
        /// </summary>
        private void btnClearFiltru_Click(object sender, EventArgs e)
        {
            dateTimePickerFiltruData.Value = DateTime.Today; // Resetează la data curentă
            IncarcaRezervariInDataGridView(); // Va reîncărca toate rezervările (sau cele de azi, dacă e implicit)
        }

        /// <summary>
        /// Curăță toate câmpurile de input de pe formularul de adăugare rezervări,
        /// pregătindu-le pentru introducerea unei noi rezervări.
        /// </summary>
        private void GolesteCampuri()
        {
            txtNumeClient.Clear();
            txtPrenumeClient.Clear();
            txtNumarTelefon.Clear();
            dateTimePickerDataOra.Value = DateTime.Now; // Resetează data/ora la cea curentă
            numUpDownNrPersoane.Value = 1; // Resetează numărul de persoane la valoarea minimă (1)
            txtObservatii.Clear();
        }


    }
}
