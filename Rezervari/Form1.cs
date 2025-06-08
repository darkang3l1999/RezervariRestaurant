using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq; // Necesit pentru ToList() pentru a crea o copie a listei pentru DataGridView
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

            // Setează DataGridView-ul să genereze automat coloane bazat pe proprietățile clasei Rezervare.
            // Acest lucru simplifică afișarea datelor din lista de Rezervare.
            dataGridViewRezervari.AutoGenerateColumns = true;
            dataGridViewAnaliza.AutoGenerateColumns = true; // Și pentru cel de analiză

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
        /// </summary>
        private void IncarcaRezervariInDataGridView()
        {
            // Pentru a forța reîmprospătarea DataGridView-ului, setăm DataSource la null și apoi la lista actualizată.
            // Utilizăm ToList() pentru a crea o copie a listei, ceea ce ajută la data binding și previne anumite erori.
            dataGridViewRezervari.DataSource = null;
            dataGridViewRezervari.DataSource = rezervari.ToList();

            // Momentan, populăm și DataGridView-ul pentru analiză cu aceleași date.
            // Ulterior, aici vom adăuga logică de filtrare/sortare specifică pentru analiza datelor.
            dataGridViewAnaliza.DataSource = null;
            dataGridViewAnaliza.DataSource = rezervari.ToList();
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
