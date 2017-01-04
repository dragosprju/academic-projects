using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab9_ADO2
{
    public partial class Form1 : Form
    {

        //private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["Lab9_ADO2.Properties.Settings.Database1ConnectionString"].ConnectionString;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.AutorCarte' table. You can move, or remove it, as needed.
            this.autorCarteTableAdapter.Fill(this.database1DataSet.AutorCarte);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connstring))
            {
                SqlCommand disableBooksConstraint = new SqlCommand("ALTER TABLE Carte NOCHECK CONSTRAINT FK_Carte_Autor", connection);
                SqlCommand enableBooksConstraint = new SqlCommand("ALTER TABLE Carte CHECK CONSTRAINT FK_Carte_Autor", connection);
                SqlCommand deleteAuthorsCommand = new SqlCommand("DELETE FROM Autor", connection);
                SqlCommand deleteBooksCommand = new SqlCommand("DELETE FROM Carte", connection);

                connection.Open();

                disableBooksConstraint.ExecuteNonQuery();      
                deleteAuthorsCommand.ExecuteNonQuery();               
                deleteBooksCommand.ExecuteNonQuery();
                enableBooksConstraint.ExecuteNonQuery();

                foreach (DataGridViewRow row in tabel.Rows)
                {
                    if (row.Cells[0].Value == null) continue;

                    SqlCommand insertAuthorCommand = new SqlCommand("INSERT INTO Autor VALUES(@ID_Autor, @Nume_Autor)", connection);
                    SqlCommand insertBooksCommand = new SqlCommand("INSERT INTO Carte VALUES(@ID_Carte, @ID_Autor, @Nume_Carte)", connection);

                    int ID_Autor = Convert.ToInt32(row.Cells[0].Value.ToString());
                    int ID_Carte = Convert.ToInt32(row.Cells[1].Value.ToString());
                    string Nume_Carte = row.Cells[2].Value.ToString();
                    string Nume_Autor = row.Cells[3].Value.ToString();

                    insertAuthorCommand.Parameters.AddWithValue("@ID_Autor", ID_Autor);
                    insertAuthorCommand.Parameters.AddWithValue("@Nume_Autor", Nume_Autor);
                    insertBooksCommand.Parameters.AddWithValue("@ID_Carte", ID_Carte);
                    insertBooksCommand.Parameters.AddWithValue("@ID_Autor", ID_Autor);
                    insertBooksCommand.Parameters.AddWithValue("@Nume_Carte", Nume_Carte);

                    insertAuthorCommand.ExecuteNonQuery();
                    insertBooksCommand.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
