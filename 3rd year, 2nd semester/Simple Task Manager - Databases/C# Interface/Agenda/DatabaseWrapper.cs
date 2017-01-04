using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Agenda
{
    static public class DatabaseWrapper
    {
        static private OracleConnection conn = null;
        static List<List<object>> lastResult = null;

        static public void EstablishConnection()
        {
            try
            {
                conn = new OracleConnection();
                conn.ConnectionString = "User Id=dragos;Password=dragos.1;Data Source=orcl";
                conn.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: Actual connection to database failed.\r\nException message: " + e.Message);
                conn = null;
            }
        }

        static public void CloseConnection()
        {
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
            }
        }

        static public OracleDataReader Execute(string command)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = command;
                cmd.CommandType = CommandType.Text;
                OracleDataReader reader = cmd.ExecuteReader();

                return reader;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Couldn't run command '" + command + "'.\r\nException message: " + ex.Message);
            }
            return null;
        }

        static public List<List<object>> Select(string command)
        {
            try
            {
                OracleDataReader reader = Execute(command);

                lastResult = new List<List<object>>();
                for (int i = 0; reader.Read(); i++)
                {
                    lastResult.Add(new List<object>());
                    for (int j = 0; j < reader.FieldCount; j++)
                    {
                        lastResult[i].Add(reader.GetValue(j));
                    }                    
                }

                return lastResult;
            }
            catch (Exception ex) {
                MessageBox.Show("Error: Couldn't run SELECT '" + command + "'.\r\nException message: " + ex.Message);
            }

            return null;

        }
    }
}
