namespace Lab9_ADO_MiniExcel
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
            this.tabel = new System.Windows.Forms.DataGridView();
            this.addColumnButton = new System.Windows.Forms.Button();
            this.deleteColumnButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tabel)).BeginInit();
            this.SuspendLayout();
            // 
            // tabel
            // 
            this.tabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tabel.Location = new System.Drawing.Point(12, 12);
            this.tabel.Name = "tabel";
            this.tabel.Size = new System.Drawing.Size(446, 278);
            this.tabel.TabIndex = 0;
            this.tabel.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.tabel_CellValueChanged);
            // 
            // addColumnButton
            // 
            this.addColumnButton.Location = new System.Drawing.Point(265, 296);
            this.addColumnButton.Name = "addColumnButton";
            this.addColumnButton.Size = new System.Drawing.Size(99, 23);
            this.addColumnButton.TabIndex = 2;
            this.addColumnButton.Text = "Adaugă coloană";
            this.addColumnButton.UseVisualStyleBackColor = true;
            this.addColumnButton.Click += new System.EventHandler(this.addColumnButton_Click);
            // 
            // deleteColumnButton
            // 
            this.deleteColumnButton.Location = new System.Drawing.Point(370, 296);
            this.deleteColumnButton.Name = "deleteColumnButton";
            this.deleteColumnButton.Size = new System.Drawing.Size(88, 23);
            this.deleteColumnButton.TabIndex = 3;
            this.deleteColumnButton.Text = "Șterge coloană";
            this.deleteColumnButton.UseVisualStyleBackColor = true;
            this.deleteColumnButton.Click += new System.EventHandler(this.deleteColumnButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 331);
            this.Controls.Add(this.deleteColumnButton);
            this.Controls.Add(this.addColumnButton);
            this.Controls.Add(this.tabel);
            this.Name = "Form1";
            this.Text = "MiniExcel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.tabel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView tabel;
        private System.Windows.Forms.Button addColumnButton;
        private System.Windows.Forms.Button deleteColumnButton;
    }
}

