using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DAL
{
    public class ParkSqlDAO
    {
        private string connectionString;
        public ParkSqlDAO(string ConnectionString)
        {
            connectionString = ConnectionString;
        }

        public List<Park> GetParks()
        {
            List<Park> parks = new List<Park>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand($"SELECT * from park", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Park park = ConvertReaderToPark(reader);
                        parks.Add(park);
                    }
                    return parks;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Sorry, a SQL error has occured.");
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public Park GetUserPark(string inputtedPark)
        {
            Park park = new Park() ;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand($"SELECT * from park where name = @inputtedPark", conn);
                    cmd.Parameters.AddWithValue("@inputtedPark", inputtedPark);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        park =  ConvertReaderToPark(reader);
                    }
                    return park;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Sorry, a SQL error has occured.");
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private Park ConvertReaderToPark(SqlDataReader reader)
        {
            Park park = new Park();
            park.ParkId = Convert.ToInt32(reader["park_id"]);
            park.Name = Convert.ToString(reader["name"]);
            park.Location= Convert.ToString(reader["location"]);
            park.EstablishDate= Convert.ToDateTime(reader["establish_date"]);
            park.Area = Convert.ToInt32(reader["area"]);
            park.Visitors = Convert.ToInt32(reader["visitors"]);
            park.Description = Convert.ToString(reader["description"]);

            return park;
        }
    }


}

