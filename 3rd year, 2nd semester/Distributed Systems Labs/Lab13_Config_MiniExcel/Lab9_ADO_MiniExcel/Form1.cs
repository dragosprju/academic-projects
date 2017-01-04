using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lab9_ADO_MiniExcel
{
    public partial class Form1 : Form
    {
        private int noColumns = 0;
        private const string fileName = "out.txt";

        public Form1()
        {
            InitializeComponent();
            readData();
        }

        private void recreateColumns()
        {
            int currColumns = tabel.Columns.Count;

            if (noColumns > currColumns)
            {
                // We add columns
                for (int i = currColumns; i < noColumns; i++)
                {
                    char columnName = 'A';
                    columnName += (char)i;
                    tabel.Columns.Add(columnName.ToString(), columnName.ToString());
                }
            }
            else
            {
                // We delete columns
                for (int i = currColumns; i > noColumns ; i--)
                {
                    tabel.Columns.RemoveAt(i - 1);
                }
            }
        }

        private void saveData()
        {
            string lines = "";

            lines += tabel.Columns.Count + "\r\n";

            for (int i = 0; i < tabel.Rows.Count; i++)
            {
                for (int j = 0; j < noColumns; j++)
                {
                    lines += tabel.Rows[i].Cells[j].Value + "\r\n";
                }                
            }

            System.IO.StreamWriter file = new System.IO.StreamWriter(fileName);
            file.WriteLine(lines);

            file.Close();
        }

        private void readData()
        {
            string[] lines = System.IO.File.ReadAllLines(fileName);

            noColumns = Convert.ToInt32(lines[0]);
            recreateColumns();

            DataGridViewRow emptyRow = (DataGridViewRow)tabel.Rows[0].Clone();
            int row = 0;
            int column = 0;
            List<string> newRow = new List<string>();
            for (int i = 1; i < lines.Length - 2; i++)
            {
                newRow.Add(lines[i].ToString());
                //tabel.Rows[row].Cells[column].Value = lines[i].ToString();
                if (column < noColumns - 1)
                {
                    column++;
                }
                else
                {
                    column = 0;
                    row++;
                    tabel.Rows.Add(newRow.ToArray());
                    newRow.Clear();
                }
            }
        }

        private void calculateData()
        {
            for (int i = 0; i < tabel.Rows.Count; i++)
            {
                for (int j = 0; j < noColumns; j++)
                {
                    string cellValue;
                    if (String.IsNullOrWhiteSpace(Convert.ToString(tabel.Rows[i].Cells[j].Value)))
                    {
                        continue;
                    }
                    else
                    {
                        cellValue = tabel.Rows[i].Cells[j].Value.ToString();
                    }
                    if (cellValue.StartsWith("=SUM("))
                    {
                        string column1;
                        string column2;
                        string row1;
                        string row2;

                        column1 = cellValue.Substring(5, 1);
                        row1 = cellValue.Substring(6, 1);
                        column2 = cellValue.Substring(8, 1);
                        row2 = cellValue.Substring(9, 1);

                        int colNo1 = column1[0] - 'A';
                        int rowNo1 = row1[0] - '1';
                        int colNo2 = column2[0] -'A';
                        int rowNo2 = row2[0] - '1';

                        int sum = 0;
                        for (int m = rowNo1; m <= rowNo2; m++)
                        {
                            for (int n = colNo1; n <= colNo2; n++)
                            {
                                int result;
                                if (Int32.TryParse(Convert.ToString(tabel.Rows[m].Cells[n].Value), out result)) {
                                    sum += result;
                                }
                            }
                        }
                        tabel.Rows[i].Cells[j].Value = sum.ToString();
                    }
                }
            }
        }

        private void addColumnButton_Click(object sender, EventArgs e)
        {
            noColumns++;
            recreateColumns();
        }

        private void deleteColumnButton_Click(object sender, EventArgs e)
        {
            if (noColumns > 0) noColumns--;
            recreateColumns();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveData();
        }

        private void tabel_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            calculateData();
        }
    }
}
