namespace Rezervari
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNumeClient = new System.Windows.Forms.TextBox();
            this.txtPrenumeClient = new System.Windows.Forms.TextBox();
            this.txtNumarTelefon = new System.Windows.Forms.TextBox();
            this.dateTimePickerDataOra = new System.Windows.Forms.DateTimePicker();
            this.numUpDownNrPersoane = new System.Windows.Forms.NumericUpDown();
            this.txtObservatii = new System.Windows.Forms.TextBox();
            this.btnAdaugaRezervare = new System.Windows.Forms.Button();
            this.dataGridViewRezervari = new System.Windows.Forms.DataGridView();
            this.btnStergeRezervare = new System.Windows.Forms.Button();
            this.dataGridViewAnaliza = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownNrPersoane)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRezervari)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAnaliza)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 450);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnStergeRezervare);
            this.tabPage1.Controls.Add(this.dataGridViewRezervari);
            this.tabPage1.Controls.Add(this.btnAdaugaRezervare);
            this.tabPage1.Controls.Add(this.txtObservatii);
            this.tabPage1.Controls.Add(this.numUpDownNrPersoane);
            this.tabPage1.Controls.Add(this.dateTimePickerDataOra);
            this.tabPage1.Controls.Add(this.txtNumarTelefon);
            this.tabPage1.Controls.Add(this.txtPrenumeClient);
            this.tabPage1.Controls.Add(this.txtNumeClient);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 424);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Gestionare Rezervari";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridViewAnaliza);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 424);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Panou de Analiza";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nume Client:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(80, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Prenume Client:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(98, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nr. Telefon:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(93, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Data și Ora:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(89, 199);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Nr. Persoane:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(99, 234);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Observatii:";
            // 
            // txtNumeClient
            // 
            this.txtNumeClient.Location = new System.Drawing.Point(172, 47);
            this.txtNumeClient.Name = "txtNumeClient";
            this.txtNumeClient.Size = new System.Drawing.Size(200, 20);
            this.txtNumeClient.TabIndex = 6;
            // 
            // txtPrenumeClient
            // 
            this.txtPrenumeClient.Location = new System.Drawing.Point(172, 92);
            this.txtPrenumeClient.Name = "txtPrenumeClient";
            this.txtPrenumeClient.Size = new System.Drawing.Size(200, 20);
            this.txtPrenumeClient.TabIndex = 7;
            // 
            // txtNumarTelefon
            // 
            this.txtNumarTelefon.Location = new System.Drawing.Point(172, 134);
            this.txtNumarTelefon.Name = "txtNumarTelefon";
            this.txtNumarTelefon.Size = new System.Drawing.Size(200, 20);
            this.txtNumarTelefon.TabIndex = 8;
            // 
            // dateTimePickerDataOra
            // 
            this.dateTimePickerDataOra.Location = new System.Drawing.Point(172, 167);
            this.dateTimePickerDataOra.Name = "dateTimePickerDataOra";
            this.dateTimePickerDataOra.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerDataOra.TabIndex = 9;
            // 
            // numUpDownNrPersoane
            // 
            this.numUpDownNrPersoane.Location = new System.Drawing.Point(172, 199);
            this.numUpDownNrPersoane.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDownNrPersoane.Name = "numUpDownNrPersoane";
            this.numUpDownNrPersoane.Size = new System.Drawing.Size(67, 20);
            this.numUpDownNrPersoane.TabIndex = 10;
            this.numUpDownNrPersoane.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtObservatii
            // 
            this.txtObservatii.Location = new System.Drawing.Point(172, 234);
            this.txtObservatii.Multiline = true;
            this.txtObservatii.Name = "txtObservatii";
            this.txtObservatii.Size = new System.Drawing.Size(200, 56);
            this.txtObservatii.TabIndex = 11;
            // 
            // btnAdaugaRezervare
            // 
            this.btnAdaugaRezervare.Location = new System.Drawing.Point(202, 296);
            this.btnAdaugaRezervare.Name = "btnAdaugaRezervare";
            this.btnAdaugaRezervare.Size = new System.Drawing.Size(75, 23);
            this.btnAdaugaRezervare.TabIndex = 12;
            this.btnAdaugaRezervare.Text = "Adauga Rezervare";
            this.btnAdaugaRezervare.UseVisualStyleBackColor = true;
            this.btnAdaugaRezervare.Click += new System.EventHandler(this.btnAdaugaRezervare_Click);
            // 
            // dataGridViewRezervari
            // 
            this.dataGridViewRezervari.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRezervari.Location = new System.Drawing.Point(426, 17);
            this.dataGridViewRezervari.Name = "dataGridViewRezervari";
            this.dataGridViewRezervari.Size = new System.Drawing.Size(357, 332);
            this.dataGridViewRezervari.TabIndex = 13;
            // 
            // btnStergeRezervare
            // 
            this.btnStergeRezervare.Location = new System.Drawing.Point(502, 366);
            this.btnStergeRezervare.Name = "btnStergeRezervare";
            this.btnStergeRezervare.Size = new System.Drawing.Size(240, 23);
            this.btnStergeRezervare.TabIndex = 14;
            this.btnStergeRezervare.Text = "Sterge Rezervare";
            this.btnStergeRezervare.UseVisualStyleBackColor = true;
            this.btnStergeRezervare.Click += new System.EventHandler(this.btnStergeRezervare_Click);
            // 
            // dataGridViewAnaliza
            // 
            this.dataGridViewAnaliza.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAnaliza.Location = new System.Drawing.Point(44, 37);
            this.dataGridViewAnaliza.Name = "dataGridViewAnaliza";
            this.dataGridViewAnaliza.Size = new System.Drawing.Size(696, 360);
            this.dataGridViewAnaliza.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownNrPersoane)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRezervari)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAnaliza)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStergeRezervare;
        private System.Windows.Forms.DataGridView dataGridViewRezervari;
        private System.Windows.Forms.Button btnAdaugaRezervare;
        private System.Windows.Forms.TextBox txtObservatii;
        private System.Windows.Forms.NumericUpDown numUpDownNrPersoane;
        private System.Windows.Forms.DateTimePicker dateTimePickerDataOra;
        private System.Windows.Forms.TextBox txtNumarTelefon;
        private System.Windows.Forms.TextBox txtPrenumeClient;
        private System.Windows.Forms.TextBox txtNumeClient;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridViewAnaliza;
    }
}

