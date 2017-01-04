namespace WindowsFormsApplication1
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
            this.saluteButton = new System.Windows.Forms.Button();
            this.langBox = new System.Windows.Forms.ComboBox();
            this.langLabel = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // saluteButton
            // 
            this.saluteButton.Location = new System.Drawing.Point(142, 4);
            this.saluteButton.Name = "saluteButton";
            this.saluteButton.Size = new System.Drawing.Size(88, 23);
            this.saluteButton.TabIndex = 0;
            this.saluteButton.Text = "Salute";
            this.saluteButton.UseVisualStyleBackColor = true;
            this.saluteButton.Click += new System.EventHandler(this.saluteButton_Click);
            // 
            // langBox
            // 
            this.langBox.Enabled = false;
            this.langBox.FormattingEnabled = true;
            this.langBox.Items.AddRange(new object[] {
            "en",
            "ro",
            "it",
            "es",
            "fr",
            "de"});
            this.langBox.Location = new System.Drawing.Point(76, 6);
            this.langBox.Name = "langBox";
            this.langBox.Size = new System.Drawing.Size(47, 21);
            this.langBox.TabIndex = 1;
            this.langBox.Text = "en";
            this.langBox.SelectedIndexChanged += new System.EventHandler(this.langBox_SelectedIndexChanged);
            // 
            // langLabel
            // 
            this.langLabel.AutoSize = true;
            this.langLabel.Location = new System.Drawing.Point(12, 9);
            this.langLabel.Name = "langLabel";
            this.langLabel.Size = new System.Drawing.Size(58, 13);
            this.langLabel.TabIndex = 2;
            this.langLabel.Text = "Language:";
            // 
            // timer
            // 
            this.timer.Interval = 2000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 34);
            this.Controls.Add(this.langLabel);
            this.Controls.Add(this.langBox);
            this.Controls.Add(this.saluteButton);
            this.Name = "Form1";
            this.Text = "The Salutor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button saluteButton;
        private System.Windows.Forms.ComboBox langBox;
        private System.Windows.Forms.Label langLabel;
        private System.Windows.Forms.Timer timer;
    }
}

