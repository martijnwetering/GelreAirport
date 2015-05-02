using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GelreAirport.Data
{
    public class GelreAirportDataAccesLayer
    {
        private readonly string _sqlConnectionString;
        private SqlConnection _sqlCon = null;

        public GelreAirportDataAccesLayer(string connectionString)
        {
            _sqlConnectionString = connectionString;
        }

        public List<int> GetCounters()
        {
            List<int> counters = new List<int>();
 
            OpenConnection();
            var sqlQuery = string.Format("SELECT * FROM Balie");

            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, _sqlCon))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        counters.Add((int) reader["balienummer"]);
                    }
                    reader.Close();
                }

                return counters;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            
        }

        private void OpenConnection()
        {
            _sqlCon = new SqlConnection(_sqlConnectionString);
            _sqlCon.Open();
        }

        private void CloseConnection()
        {
            _sqlCon.Close();
        }

    }
}
