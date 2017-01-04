namespace Customer_Send
{
    partial class SendForm
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
            this.custNameTextBox = new System.Windows.Forms.TextBox();
            this.customerLabel = new System.Windows.Forms.Label();
            this.custNameLabel = new System.Windows.Forms.Label();
            this.custEmailLabel = new System.Windows.Forms.Label();
            this.custEmailTextBox = new System.Windows.Forms.TextBox();
            this.custCardNoTextBox = new System.Windows.Forms.TextBox();
            this.custCardNoLabel = new System.Windows.Forms.Label();
            this.sendButton = new System.Windows.Forms.Button();
            this.xmlCheckBox = new System.Windows.Forms.CheckBox();
            this.binaryCheckBox = new System.Windows.Forms.CheckBox();
            this.cleanButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // custNameTextBox
            // 
            this.custNameTextBox.Location = new System.Drawing.Point(86, 36);
            this.custNameTextBox.Name = "custNameTextBox";
            this.custNameTextBox.Size = new System.Drawing.Size(203, 20);
            this.custNameTextBox.TabIndex = 0;
            // 
            // customerLabel
            // 
            this.customerLabel.AutoSize = true;
            this.customerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.customerLabel.Location = new System.Drawing.Point(12, 11);
            this.customerLabel.Name = "customerLabel";
            this.customerLabel.Size = new System.Drawing.Size(76, 17);
            this.customerLabel.TabIndex = 1;
            this.customerLabel.Text = "Customer";
            // 
            // custNameLabel
            // 
            this.custNameLabel.AutoSize = true;
            this.custNameLabel.Location = new System.Drawing.Point(12, 39);
            this.custNameLabel.Name = "custNameLabel";
            this.custNameLabel.Size = new System.Drawing.Size(35, 13);
            this.custNameLabel.TabIndex = 2;
            this.custNameLabel.Text = "Name";
            // 
            // custEmailLabel
            // 
            this.custEmailLabel.AutoSize = true;
            this.custEmailLabel.Location = new System.Drawing.Point(12, 64);
            this.custEmailLabel.Name = "custEmailLabel";
            this.custEmailLabel.Size = new System.Drawing.Size(32, 13);
            this.custEmailLabel.TabIndex = 3;
            this.custEmailLabel.Text = "Email";
            // 
            // custEmailTextBox
            // 
            this.custEmailTextBox.Location = new System.Drawing.Point(86, 61);
            this.custEmailTextBox.Name = "custEmailTextBox";
            this.custEmailTextBox.Size = new System.Drawing.Size(203, 20);
            this.custEmailTextBox.TabIndex = 4;
            // 
            // custCardNoTextBox
            // 
            this.custCardNoTextBox.Location = new System.Drawing.Point(86, 87);
            this.custCardNoTextBox.Name = "custCardNoTextBox";
            this.custCardNoTextBox.Size = new System.Drawing.Size(203, 20);
            this.custCardNoTextBox.TabIndex = 6;
            // 
            // custCardNoLabel
            // 
            this.custCardNoLabel.AutoSize = true;
            this.custCardNoLabel.Location = new System.Drawing.Point(12, 90);
            this.custCardNoLabel.Name = "custCardNoLabel";
            this.custCardNoLabel.Size = new System.Drawing.Size(69, 13);
            this.custCardNoLabel.TabIndex = 5;
            this.custCardNoLabel.Text = "Card Number";
            // 
            // sendButton
            // 
            this.sendButton.BackColor = System.Drawing.Color.LightBlue;
            this.sendButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.sendButton.Location = new System.Drawing.Point(12, 143);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(277, 46);
            this.sendButton.TabIndex = 9;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = false;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // xmlCheckBox
            // 
            this.xmlCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.xmlCheckBox.Checked = true;
            this.xmlCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.xmlCheckBox.Location = new System.Drawing.Point(12, 114);
            this.xmlCheckBox.Name = "xmlCheckBox";
            this.xmlCheckBox.Size = new System.Drawing.Size(135, 23);
            this.xmlCheckBox.TabIndex = 0;
            this.xmlCheckBox.Text = "XmlMessageFormatter";
            this.xmlCheckBox.UseVisualStyleBackColor = true;
            this.xmlCheckBox.CheckedChanged += new System.EventHandler(this.xmlCheckBox_CheckedChanged);
            // 
            // binaryCheckBox
            // 
            this.binaryCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.binaryCheckBox.Location = new System.Drawing.Point(154, 114);
            this.binaryCheckBox.Name = "binaryCheckBox";
            this.binaryCheckBox.Size = new System.Drawing.Size(135, 23);
            this.binaryCheckBox.TabIndex = 10;
            this.binaryCheckBox.Text = "BinaryMessageFormatter";
            this.binaryCheckBox.UseVisualStyleBackColor = true;
            this.binaryCheckBox.CheckedChanged += new System.EventHandler(this.binaryCheckBox_CheckedChanged);
            // 
            // cleanButton
            // 
            this.cleanButton.Location = new System.Drawing.Point(12, 195);
            this.cleanButton.Name = "cleanButton";
            this.cleanButton.Size = new System.Drawing.Size(277, 23);
            this.cleanButton.TabIndex = 11;
            this.cleanButton.Text = "Clean";
            this.cleanButton.UseVisualStyleBackColor = true;
            this.cleanButton.Click += new System.EventHandler(this.cleanButton_Click);
            // 
            // SendForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 227);
            this.Controls.Add(this.cleanButton);
            this.Controls.Add(this.binaryCheckBox);
            this.Controls.Add(this.xmlCheckBox);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.custCardNoTextBox);
            this.Controls.Add(this.custCardNoLabel);
            this.Controls.Add(this.custEmailTextBox);
            this.Controls.Add(this.custEmailLabel);
            this.Controls.Add(this.custNameLabel);
            this.Controls.Add(this.customerLabel);
            this.Controls.Add(this.custNameTextBox);
            this.Name = "SendForm";
            this.Text = "MSMQ Send";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox custNameTextBox;
        private System.Windows.Forms.Label customerLabel;
        private System.Windows.Forms.Label custNameLabel;
        private System.Windows.Forms.Label custEmailLabel;
        private System.Windows.Forms.TextBox custEmailTextBox;
        private System.Windows.Forms.TextBox custCardNoTextBox;
        private System.Windows.Forms.Label custCardNoLabel;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.CheckBox xmlCheckBox;
        private System.Windows.Forms.CheckBox binaryCheckBox;
        private System.Windows.Forms.Button cleanButton;
    }
}

