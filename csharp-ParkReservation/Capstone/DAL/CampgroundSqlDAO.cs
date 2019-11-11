using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DAL
{
    public class CampgroundSqlDAO
    {
        private string connectionString;
        public CampgroundSqlDAO(string ConnectionString)
        {
            connectionString = ConnectionString;
        }

        public List<Campground> GetCampgroundsFromPark(string inputtedPark)
        {
            List<Campground> campgrounds = new List<Campground>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand($"SELECT * from campground where park_id  = (select park_id from park where name = @inputtedPark)", conn);
                    cmd.Parameters.AddWithValue("@inputtedPark", inputtedPark);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Campground campground = ConvertReaderToCampground(reader);
                        campgrounds.Add(campground);
                    }
                    return campgrounds;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Sorry, a SQL error has occured.");
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private Campground ConvertReaderToCampground(SqlDataReader reader)
        {
            Campground campground = new Campground();
            campground.CampgroundId = Convert.ToInt32(reader["campground_id"]);
            campground.ParkId = Convert.ToInt32(reader["park_id"]);
            campground.Name = Convert.ToString(reader["name"]);
            campground.OpenFromMonth = Convert.ToInt32(reader["open_from_mm"]);
            campground.OpenToMonth = Convert.ToInt32(reader["open_to_mm"]);
            campground.DailyFee = Convert.ToDecimal(reader["daily_fee"]);
            return campground;
        }
    }
}
