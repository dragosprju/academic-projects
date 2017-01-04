namespace Agenda
{
    partial class Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form));
            this.usernameLabel = new System.Windows.Forms.Label();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.connectCheckButton = new System.Windows.Forms.Button();
            this.createDeleteButton = new System.Windows.Forms.Button();
            this.taskTextBox = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.taskList = new System.Windows.Forms.ListBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // usernameLabel
            // 
            resources.ApplyResources(this.usernameLabel, "usernameLabel");
            this.usernameLabel.Name = "usernameLabel";
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.AllowDrop = true;
            resources.ApplyResources(this.usernameTextBox, "usernameTextBox");
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.noEnterSound_KeyDown);
            // 
            // passwordLabel
            // 
            resources.ApplyResources(this.passwordLabel, "passwordLabel");
            this.passwordLabel.Name = "passwordLabel";
            // 
            // passwordTextBox
            // 
            resources.ApplyResources(this.passwordTextBox, "passwordTextBox");
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.UseSystemPasswordChar = true;
            this.passwordTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.noEnterSound_KeyDown);
            // 
            // connectCheckButton
            // 
            resources.ApplyResources(this.connectCheckButton, "connectCheckButton");
            this.connectCheckButton.Name = "connectCheckButton";
            this.connectCheckButton.UseVisualStyleBackColor = true;
            this.connectCheckButton.Click += new System.EventHandler(this.connectCheckButton_Click);
            this.connectCheckButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.noEnterSound_KeyDown);
            this.connectCheckButton.KeyUp += new System.Windows.Forms.KeyEventHandler(this.connectCheckButton_KeyUp);
            // 
            // createDeleteButton
            // 
            resources.ApplyResources(this.createDeleteButton, "createDeleteButton");
            this.createDeleteButton.Name = "createDeleteButton";
            this.createDeleteButton.UseVisualStyleBackColor = true;
            this.createDeleteButton.Click += new System.EventHandler(this.createDeleteButton_Click);
            this.createDeleteButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.noEnterSound_KeyDown);
            this.createDeleteButton.KeyUp += new System.Windows.Forms.KeyEventHandler(this.createDeleteButton_KeyUp);
            // 
            // taskTextBox
            // 
            resources.ApplyResources(this.taskTextBox, "taskTextBox");
            this.taskTextBox.Name = "taskTextBox";
            this.taskTextBox.Enter += new System.EventHandler(this.taskTextBox_Enter);
            this.taskTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.noEnterSound_KeyDown);
            this.taskTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.taskTextBox_KeyUp);
            // 
            // addButton
            // 
            resources.ApplyResources(this.addButton, "addButton");
            this.addButton.Name = "addButton";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            this.addButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.noEnterSound_KeyDown);
            this.addButton.KeyUp += new System.Windows.Forms.KeyEventHandler(this.addButton_KeyUp);
            // 
            // taskList
            // 
            resources.ApplyResources(this.taskList, "taskList");
            this.taskList.FormattingEnabled = true;
            this.taskList.Name = "taskList";
            this.taskList.SelectedIndexChanged += new System.EventHandler(this.taskList_SelectedIndexChanged);
            this.taskList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.noEnterSound_KeyDown);
            this.taskList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.taskList_KeyUp);
            // 
            // searchButton
            // 
            resources.ApplyResources(this.searchButton, "searchButton");
            this.searchButton.Name = "searchButton";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            this.searchButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.noEnterSound_KeyDown);
            this.searchButton.KeyUp += new System.Windows.Forms.KeyEventHandler(this.searchButton_KeyUp);
            // 
            // Form
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.taskList);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.taskTextBox);
            this.Controls.Add(this.createDeleteButton);
            this.Controls.Add(this.connectCheckButton);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.usernameTextBox);
            this.Controls.Add(this.usernameLabel);
            this.Name = "Form";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_FormClosing);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Button connectCheckButton;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Button createDeleteButton;
        private System.Windows.Forms.TextBox taskTextBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.ListBox taskList;
        private System.Windows.Forms.Button searchButton;
    }
}

