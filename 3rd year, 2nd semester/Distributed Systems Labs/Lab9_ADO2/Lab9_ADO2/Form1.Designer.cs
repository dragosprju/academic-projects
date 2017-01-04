namespace Lab9_ADO2
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
            this.components = new System.ComponentModel.Container();
            this.tabel = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.iDAutorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDCarteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numeCarteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numeAutorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.autorCarteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.database1DataSet = new Lab9_ADO2.Database1DataSet();
            this.autorCarteTableAdapter = new Lab9_ADO2.Database1DataSetTableAdapters.AutorCarteTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.tabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.autorCarteBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.database1DataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // tabel
            // 
            this.tabel.AutoGenerateColumns = false;
            this.tabel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tabel.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDAutorDataGridViewTextBoxColumn,
            this.iDCarteDataGridViewTextBoxColumn,
            this.numeCarteDataGridViewTextBoxColumn,
            this.numeAutorDataGridViewTextBoxColumn});
            this.tabel.DataSource = this.autorCarteBindingSource;
            this.tabel.Location = new System.Drawing.Point(12, 12);
            this.tabel.Name = "tabel";
            this.tabel.Size = new System.Drawing.Size(556, 237);
            this.tabel.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(493, 255);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Salveaza";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // iDAutorDataGridViewTextBoxColumn
            // 
            this.iDAutorDataGridViewTextBoxColumn.DataPropertyName = "ID_Autor";
            this.iDAutorDataGridViewTextBoxColumn.HeaderText = "ID_Autor";
            this.iDAutorDataGridViewTextBoxColumn.Name = "iDAutorDataGridViewTextBoxColumn";
            // 
            // iDCarteDataGridViewTextBoxColumn
            // 
            this.iDCarteDataGridViewTextBoxColumn.DataPropertyName = "ID_Carte";
            this.iDCarteDataGridViewTextBoxColumn.HeaderText = "ID_Carte";
            this.iDCarteDataGridViewTextBoxColumn.Name = "iDCarteDataGridViewTextBoxColumn";
            // 
            // numeCarteDataGridViewTextBoxColumn
            // 
            this.numeCarteDataGridViewTextBoxColumn.DataPropertyName = "Nume_Carte";
            this.numeCarteDataGridViewTextBoxColumn.HeaderText = "Nume_Carte";
            this.numeCarteDataGridViewTextBoxColumn.Name = "numeCarteDataGridViewTextBoxColumn";
            // 
            // numeAutorDataGridViewTextBoxColumn
            // 
            this.numeAutorDataGridViewTextBoxColumn.DataPropertyName = "Nume_Autor";
            this.numeAutorDataGridViewTextBoxColumn.HeaderText = "Nume_Autor";
            this.numeAutorDataGridViewTextBoxColumn.Name = "numeAutorDataGridViewTextBoxColumn";
            // 
            // autorCarteBindingSource
            // 
            this.autorCarteBindingSource.DataMember = "AutorCarte";
            this.autorCarteBindingSource.DataSource = this.database1DataSet;
            // 
            // database1DataSet
            // 
            this.database1DataSet.DataSetName = "Database1DataSet";
            this.database1DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // autorCarteTableAdapter
            // 
            this.autorCarteTableAdapter.ClearBeforeFill = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 285);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabel);
            this.Name = "Form1";
            this.Text = "Carti si Autori";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.autorCarteBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.database1DataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView tabel;
        private Database1DataSet database1DataSet;
        private System.Windows.Forms.BindingSource autorCarteBindingSource;
        private Database1DataSetTableAdapters.AutorCarteTableAdapter autorCarteTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDAutorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDCarteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numeCarteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numeAutorDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button button1;
    }
}

