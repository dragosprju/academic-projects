using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Agenda
{
    public partial class Form : System.Windows.Forms.Form
    {
        int accountCreationStep = 0;

        public Form()
        {
            InitializeComponent();
            DatabaseWrapper.EstablishConnection();
            //LoggedOut.login("dragos", "dragos.1");
            //toggleTaskLayout();
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            DatabaseWrapper.CloseConnection();
        }

        private void resetIntroLayout()
        {
            usernameLabel.Text = "Nume utilizator: ";
            passwordLabel.Text = "Parolă: ";
            usernameTextBox.Text = "";
            passwordTextBox.Text = "";
            passwordTextBox.UseSystemPasswordChar = true;
            connectCheckButton.Text = "Conectează-te";
            createDeleteButton.Text = "Creează cont";
        }

        private void secondStageLayout()
        {
            usernameLabel.Text = "Prenume: ";
            passwordLabel.Text = "Nume: ";
            usernameTextBox.Text = "";
            passwordTextBox.Text = "";
            passwordTextBox.UseSystemPasswordChar = false;
            connectCheckButton.Text = "Continuare >";
            createDeleteButton.Text = "Anulează";
        }

        private void thirdStageLayout()
        {
            usernameLabel.Text = "Nume utilizator: ";
            passwordLabel.Text = "Parolă: ";
            usernameTextBox.Text = "";
            passwordTextBox.Text = "";
            passwordTextBox.UseSystemPasswordChar = true;
            connectCheckButton.Text = "Creează >";
        }

        private void refreshTaskList()
        {
            taskList.Items.Clear();
            List<Task> tasks = LoggedIn.getTasks();
            foreach (Task task in tasks)
            {
                taskList.Items.Add(task);
            }
        }

        private void toggleTaskLayout()
        {
            resetIntroLayout();
            usernameLabel.Text = "Connected as " + LoggedIn.connectedAccount.name + " " + LoggedIn.connectedAccount.surname.Substring(0, 1) + ".";
            passwordLabel.Visible = false;
            usernameTextBox.Visible = false;
            passwordTextBox.Visible = false;

            taskList.Visible = true;
            taskTextBox.Visible = true;
            addButton.Visible = true;
            searchButton.Visible = true;

            createDeleteButton.Size = new System.Drawing.Size(75, 23);
            connectCheckButton.Size = new System.Drawing.Size(75, 23);
            createDeleteButton.Location = new System.Drawing.Point(createDeleteButton.Location.X + 25, createDeleteButton.Location.Y);
            connectCheckButton.Location = new System.Drawing.Point(connectCheckButton.Location.X + 50, connectCheckButton.Location.Y);

            createDeleteButton.Text = "Șterge";
            connectCheckButton.Text = "Bifează";

            refreshTaskList();
            taskList.Focus();
        }
        private void connectCheckButton_Click(object sender, EventArgs e)
        {
            if (connectCheckButton.Text != "Bifează")
            {
                if (accountCreationStep == 0)
                {
                    bool success = LoggedOut.login(usernameTextBox.Text, passwordTextBox.Text);

                    if (success)
                    {
                        toggleTaskLayout();
                    }
                    else
                    {
                        resetIntroLayout();
                    }
                }
                else if (accountCreationStep == 1)
                {
                    LoggedOut.setFirstPart(usernameTextBox.Text, passwordTextBox.Text);
                    thirdStageLayout();
                    accountCreationStep = 2;
                }
                else if (accountCreationStep == 2)
                {
                    LoggedOut.setSecondPart(usernameTextBox.Text, passwordTextBox.Text);
                    LoggedOut.createAccount();

                    resetIntroLayout();
                    accountCreationStep = 0;
                }
            }
            else
            {
                if (taskList.SelectedItem != null)
                {
                    int toReselect = taskList.SelectedIndex;
                    LoggedIn.checkTask(((Task)taskList.SelectedItem).ID);
                    refreshTaskList();
                    taskList.SelectedIndex = toReselect;
                    taskList.Focus();
                }      
            }
        }

        private void createDeleteButton_Click(object sender, EventArgs e)
        {
            if (createDeleteButton.Text != "Șterge")
            {
                if (accountCreationStep == 0)
                {
                    secondStageLayout();
                    accountCreationStep = 1;
                }
                else if (accountCreationStep == 1 || accountCreationStep == 2)
                {
                    LoggedOut.cancel();
                    resetIntroLayout();
                    accountCreationStep = 0;
                }
            }
            else
            {
                if (taskList.SelectedItem != null)
                {
                    LoggedIn.deleteTask(((Task)taskList.SelectedItem).ID);
                    refreshTaskList();
                }
            }

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Task toAdd = LoggedIn.extractTaskFromCommand(taskTextBox.Text);
            LoggedIn.addTask(toAdd);
            refreshTaskList();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            Task toSearch = LoggedIn.extractTaskFromCommand(taskTextBox.Text);
            taskList.Items.Clear();
            List<Task> tasks = LoggedIn.selectTasks(toSearch);
            foreach (Task task in tasks)
            {
                taskList.Items.Add(task);
            }
        }

        private void taskList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                int savedIndex = taskList.SelectedIndex;
                LoggedIn.deleteTask(((Task)taskList.SelectedItem).ID);
                refreshTaskList();
                if (savedIndex < taskList.Items.Count)
                {
                    taskList.SelectedIndex = savedIndex;
                }
                else if (taskList.Items.Count > 0)
                {
                    taskList.SelectedIndex = taskList.Items.Count - 1;
                }
            }
            else if (e.KeyCode == Keys.Space)
            {
                connectCheckButton_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter || (e.KeyCode == Keys.F && e.Modifiers == Keys.Control))
            {
                taskTextBox.Focus();
            }
            else
            {
                Form_KeyUp(sender, e);
            }
        }

        private void taskTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                addButton_Click(null, null);
                taskTextBox.Text = "";
            }
            else if (e.KeyCode == Keys.F && e.Modifiers == Keys.Control)
            {
                searchButton_Click(null, null);
            }
            else
            {
                Form_KeyUp(sender, e);
            }
        }

        private void Form_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                taskList.Focus();
                if (taskList.SelectedItem == null)
                {
                    taskList.SelectedIndex = 0;
                }    
                else if (taskList.SelectedIndex < taskList.Items.Count - 1)
                {
                    taskList.SelectedIndex += 1;
                }            
            }
            else if (e.KeyCode == Keys.Up)
            {
                taskList.Focus();
                if (taskList.SelectedItem == null)
                {
                    taskList.SelectedIndex = taskList.Items.Count - 1;
                }
                else if(taskList.SelectedIndex > 0)
                {
                    taskList.SelectedIndex -= 1;
                }       
            }
        }

        private void connectCheckButton_KeyUp(object sender, KeyEventArgs e)
        {
            Form_KeyUp(sender, e);
        }

        private void createDeleteButton_KeyUp(object sender, KeyEventArgs e)
        {
            Form_KeyUp(sender, e);
        }

        private void addButton_KeyUp(object sender, KeyEventArgs e)
        {
            Form_KeyUp(sender, e);
        }

        private void searchButton_KeyUp(object sender, KeyEventArgs e)
        {
            Form_KeyUp(sender, e);
        }

        private void noEnterSound_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || (e.KeyCode == Keys.F && e.Modifiers == Keys.Control) || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

        }

        private void taskTextBox_Enter(object sender, EventArgs e)
        {
            //addButton.Text = "Adaugă";
        }

        private void taskList_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            if(taskList.SelectedItem != null)
            {
                addButton.Text = "Editează";
            }
            */
        }
    }
}
