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
using Rezervari.BusinessLogic; // Importă namespace-ul pentru strategiile de sortare

namespace Rezervari
{
    public partial class Form1 : Form
    {
        private IRezervareRepository _rezervareRepository;
        // Adăugăm o referință la strategia de sortare curentă.
        private IRezervareSortStrategy _currentSortStrategy;
        // O listă cu toate strategiile de sortare disponibile.
        private List<IRezervareSortStrategy> _sortStrategies;

        /// <summary>
        /// Constructorul principal al formei.
        /// Inițializează componentele UI și logica de bază a aplicației.
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            _rezervareRepository = new InMemoryRezervareRepository();

            // Inițializează strategiile de sortare disponibile.
            _sortStrategies = new List<IRezervareSortStrategy>
            {
                new SortByDateAscendingStrategy(),
                new SortByDateDescendingStrategy(),
                new SortByNameStrategy()
            };

            // Populează ComboBox-ul cu numele strategiilor de sortare.
            // Fiecare element din ComboBox va fi obiectul strategiei, iar Text-ul afișat va fi proprietatea Name.
            comboBoxSortStrategy.DisplayMember = "Name"; // Afișează proprietatea Name a obiectului strategiei
            comboBoxSortStrategy.ValueMember = null; // Nu avem o valoare specifică, vom folosi obiectul întreg
            comboBoxSortStrategy.DataSource = _sortStrategies;

            // Setează strategia de sortare implicită (ex: Data Crescător).
            _currentSortStrategy = _sortStrategies.FirstOrDefault(); // Setează prima strategie ca implicită
            if (_currentSortStrategy != null)
            {
                comboBoxSortStrategy.SelectedItem = _currentSortStrategy; // Selectează elementul în ComboBox
            }


            dataGridViewRezervari.AutoGenerateColumns = true;
            dataGridViewAnaliza.AutoGenerateColumns = true;

            dateTimePickerFiltruData.Value = DateTime.Today;

            IncarcaRezervariInDataGridView();
        }

        /// <summary>
        /// Gestionează evenimentul de click pentru butonul "Adaugă Rezervare".
        /// </summary>
        private void btnAdaugaRezervare_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNumeClient.Text) ||
                    string.IsNullOrWhiteSpace(txtPrenumeClient.Text) ||
                    numUpDownNrPersoane.Value <= 0)
                {
                    MessageBox.Show("Nume, prenume client și număr persoane sunt obligatorii!", "Eroare Validare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Rezervare nouaRezervare = new Rezervare(
                    txtNumeClient.Text,
                    txtPrenumeClient.Text,
                    txtNumarTelefon.Text,
                    dateTimePickerDataOra.Value,
                    (int)numUpDownNrPersoane.Value,
                    txtObservatii.Text
                );

                _rezervareRepository.AddRezervare(nouaRezervare);
                IncarcaRezervariInDataGridView();
                GolesteCampuri();
                MessageBox.Show("Rezervare adăugată cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A apărut o eroare la adăugarea rezervării: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Gestionează evenimentul de click pentru butonul "Șterge Rezervare".
        /// </summary>
        private void btnStergeRezervare_Click(object sender, EventArgs e)
        {
            if (dataGridViewRezervari.SelectedRows.Count > 0)
            {
                var confirmResult = MessageBox.Show("Ești sigur că vrei să ștergi această rezervare?", "Confirmare Ștergere", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmResult == DialogResult.Yes)
                {
                    Rezervare rezervareDeSters = dataGridViewRezervari.SelectedRows[0].DataBoundItem as Rezervare;

                    if (rezervareDeSters != null)
                    {
                        _rezervareRepository.DeleteRezervare(rezervareDeSters);
                        IncarcaRezervariInDataGridView();
                        MessageBox.Show("Rezervare ștearsă cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selectează o rezervare pentru a o șterge.", "Avertisment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Reîmprospătează datele afișate în ambele DataGridView-uri.
        /// Acum preia datele din repository și aplică strategia de sortare și filtrarea de dată.
        /// </summary>
        private void IncarcaRezervariInDataGridView()
        {
            List<Rezervare> toateRezervarile = _rezervareRepository.GetAllRezervari();

            // Reîmprospătează DataGridView-ul pentru Gestionare Rezervări (întotdeauna nesortat/nefiltrat aici)
            dataGridViewRezervari.DataSource = null;
            dataGridViewRezervari.DataSource = toateRezervarile.ToList();

            // Logica de filtrare și sortare pentru Panoul de Analiză
            List<Rezervare> rezervariPentruAnaliza = new List<Rezervare>(toateRezervarile);

            // Pasul 1: Aplică filtrarea după dată
            DateTime dataSelectata = dateTimePickerFiltruData.Value.Date;
            rezervariPentruAnaliza = rezervariPentruAnaliza
                .Where(r => r.DataOra.Date == dataSelectata)
                .ToList();

            // Pasul 2: Aplică strategia de sortare selectată, dacă există.
            if (_currentSortStrategy != null)
            {
                rezervariPentruAnaliza = _currentSortStrategy.Sort(rezervariPentruAnaliza);
            }

            // Reîmprospătează DataGridView-ul pentru Panoul de Analiză
            dataGridViewAnaliza.DataSource = null;
            dataGridViewAnaliza.DataSource = rezervariPentruAnaliza;
        }

        /// <summary>
        /// Gestionează evenimentul de click pentru butonul "Aplică Filtru" din Panoul de Analiză.
        /// </summary>
        private void btnAplicaFiltru_Click(object sender, EventArgs e)
        {
            IncarcaRezervariInDataGridView(); // Metoda va aplica automat filtrul
        }

        /// <summary>
        /// Gestionează evenimentul de click pentru butonul "Anulează Filtru" din Panoul de Analiză.
        /// </summary>
        private void btnClearFiltru_Click(object sender, EventArgs e)
        {
            dateTimePickerFiltruData.Value = DateTime.Today; // Resetează la data curentă
            // Resetează și strategia de sortare la cea implicită (prima din listă).
            _currentSortStrategy = _sortStrategies.FirstOrDefault();
            if (_currentSortStrategy != null)
            {
                comboBoxSortStrategy.SelectedItem = _currentSortStrategy;
            }
            IncarcaRezervariInDataGridView(); // Va reîncărca rezervările pentru data curentă
        }

        /// <summary>
        /// Gestionează evenimentul de selecție modificată în ComboBox-ul de sortare.
        /// Setează noua strategie de sortare și reîncarcă DataGridView-ul.
        /// </summary>
        private void comboBoxSortStrategy_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Când utilizatorul schimbă selecția, actualizăm strategia curentă.
            if (comboBoxSortStrategy.SelectedItem is IRezervareSortStrategy selectedStrategy)
            {
                _currentSortStrategy = selectedStrategy;
                IncarcaRezervariInDataGridView(); // Reîncarcă pentru a aplica noua sortare
            }
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
