using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rezervari.Data; // Importă namespace-ul pentru Repository

namespace Rezervari
{
    public partial class Form1 : Form
    {
        // Declaram o variabila de tipul interfetei IRezervareRepository.
        // Acum Form1 nu va mai sti detaliile de implementare ale stocarii.
        private IRezervareRepository _rezervareRepository;

        /// <summary>
        /// Constructorul principal al formei.
        /// Inițializează componentele UI și logica de bază a aplicației.
        /// </summary>
        public Form1()
        {
            InitializeComponent(); // Inițializează controalele generate de designer

            // Instanțiem implementarea in-memory a repository-ului.
            // În aplicații mai complexe, am folosi Inversion of Control/Dependency Injection aici.
            _rezervareRepository = new InMemoryRezervareRepository();

            // Setează DataGridView-urile să genereze automat coloane bazat pe proprietățile clasei Rezervare.
            dataGridViewRezervari.AutoGenerateColumns = true;
            dataGridViewAnaliza.AutoGenerateColumns = true;

            // Inițializează DataPicker-ul de filtrare la data curentă.
            dateTimePickerFiltruData.Value = DateTime.Today;

            // Încarcă inițial rezervările folosind repository-ul.
            IncarcaRezervariInDataGridView();
        }

        /// <summary>
        /// Gestionează evenimentul de click pentru butonul "Adaugă Rezervare".
        /// Colectează datele introduse de utilizator, efectuează validări simple
        /// și adaugă o nouă rezervare la lista internă folosind repository-ul.
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
                    return;
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

                // Adaugă noua rezervare folosind repository-ul.
                _rezervareRepository.AddRezervare(nouaRezervare);

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
        /// Șterge rezervarea selectată din DataGridView-ul principal folosind repository-ul.
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
                    Rezervare rezervareDeSters = dataGridViewRezervari.SelectedRows[0].DataBoundItem as Rezervare;

                    if (rezervareDeSters != null)
                    {
                        // Elimină rezervarea folosind repository-ul.
                        _rezervareRepository.DeleteRezervare(rezervareDeSters);

                        // Reîmprospătează ambele DataGridView-uri.
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
        /// Reîmprospătează datele afișate în ambele DataGridView-uri.
        /// Acum preia datele din repository.
        /// </summary>
        private void IncarcaRezervariInDataGridView()
        {
            // Preluăm toate rezervările de la repository.
            List<Rezervare> toateRezervarile = _rezervareRepository.GetAllRezervari();

            // Reîmprospătează DataGridView-ul pentru Gestionare Rezervări
            dataGridViewRezervari.DataSource = null;
            dataGridViewRezervari.DataSource = toateRezervarile.ToList(); // Folosim ToList() pentru a asigura o copie legată la UI

            // Logica de filtrare pentru Panoul de Analiză
            List<Rezervare> rezervariFiltrate = new List<Rezervare>(toateRezervarile); // Creează o copie a listei complete

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
            IncarcaRezervariInDataGridView(); // Va reîncărca rezervările pentru data curentă
        }

        /// <summary>
        /// Curăță toate câmpurile de input de pe formularul de adăugare rezervări.
        /// </summary>
        private void GolesteCampuri()
        {
            txtNumeClient.Clear();
            txtPrenumeClient.Clear();
            txtNumarTelefon.Clear();
            dateTimePickerDataOra.Value = DateTime.Now;
            numUpDownNrPersoane.Value = 1;
            txtObservatii.Clear();
        }
    }
}
