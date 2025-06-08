using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rezervari.Data; 
using Rezervari.BusinessLogic; 

namespace Rezervari
{
    public partial class Form1 : Form
    {
        private IRezervareRepository _rezervareRepository;
        private IRezervareSortStrategy _currentSortStrategy;
        private List<IRezervareSortStrategy> _sortStrategies;

        public Form1()
        {
            InitializeComponent();

            _rezervareRepository = new InMemoryRezervareRepository();

   
            _sortStrategies = new List<IRezervareSortStrategy>
            {
                new SortByDateAscendingStrategy(),
                new SortByDateDescendingStrategy(),
                new SortByNameStrategy()
            };

    
            comboBoxSortStrategy.DisplayMember = "Name"; 
            comboBoxSortStrategy.ValueMember = null; 
            comboBoxSortStrategy.DataSource = _sortStrategies;

          
            _currentSortStrategy = _sortStrategies.FirstOrDefault();
            if (_currentSortStrategy != null)
            {
                comboBoxSortStrategy.SelectedItem = _currentSortStrategy; 
            }


            dataGridViewRezervari.AutoGenerateColumns = true;
            dataGridViewAnaliza.AutoGenerateColumns = true;

            dateTimePickerFiltruData.Value = DateTime.Today;

            IncarcaRezervariInDataGridView();
        }

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
                MessageBox.Show("Rezervare adaugata cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A aparut o eroare la adaugarea rezervarii: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

  

        private void btnStergeRezervare_Click(object sender, EventArgs e)
        {
            if (dataGridViewRezervari.SelectedRows.Count > 0)
            {
                var confirmResult = MessageBox.Show("Esti sigur ca vrei sa stergi aceasta rezervare?", "Confirmare Stergere", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmResult == DialogResult.Yes)
                {
                    Rezervare rezervareDeSters = dataGridViewRezervari.SelectedRows[0].DataBoundItem as Rezervare;

                    if (rezervareDeSters != null)
                    {
                        _rezervareRepository.DeleteRezervare(rezervareDeSters);
                        IncarcaRezervariInDataGridView();
                        MessageBox.Show("Rezervare stearsa cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecteaza o rezervare pentru a o sterge.", "Avertisment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void IncarcaRezervariInDataGridView()
        {
            List<Rezervare> toateRezervarile = _rezervareRepository.GetAllRezervari();

      
            dataGridViewRezervari.DataSource = null;
            dataGridViewRezervari.DataSource = toateRezervarile.ToList();

    
            List<Rezervare> rezervariPentruAnaliza = new List<Rezervare>(toateRezervarile);

            DateTime dataSelectata = dateTimePickerFiltruData.Value.Date;
            rezervariPentruAnaliza = rezervariPentruAnaliza
                .Where(r => r.DataOra.Date == dataSelectata)
                .ToList();

    
            if (_currentSortStrategy != null)
            {
                rezervariPentruAnaliza = _currentSortStrategy.Sort(rezervariPentruAnaliza);
            }

 
            dataGridViewAnaliza.DataSource = null;
            dataGridViewAnaliza.DataSource = rezervariPentruAnaliza;
        }


        private void btnAplicaFiltru_Click(object sender, EventArgs e)
        {
            IncarcaRezervariInDataGridView(); 
        }

  
        private void btnClearFiltru_Click(object sender, EventArgs e)
        {
            dateTimePickerFiltruData.Value = DateTime.Today;
            _currentSortStrategy = _sortStrategies.FirstOrDefault();
            if (_currentSortStrategy != null)
            {
                comboBoxSortStrategy.SelectedItem = _currentSortStrategy;
            }
            IncarcaRezervariInDataGridView(); 
        }

        private void comboBoxSortStrategy_SelectedIndexChanged(object sender, EventArgs e)
        {
  
            if (comboBoxSortStrategy.SelectedItem is IRezervareSortStrategy selectedStrategy)
            {
                _currentSortStrategy = selectedStrategy;
                IncarcaRezervariInDataGridView();
            }
        }

    
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
