using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DAL
{
    public class SiteSqlDAO
    {
        private string connectionString;
        public SiteSqlDAO(string ConnectionString)
        {
            connectionString = ConnectionString;
        }

        public List<Site> GetUserInputtedSite(int userCampgroundId, DateTime startDate, DateTime endDate)
        {
            List<Site> sites = new List<Site>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand($"Select TOP 5 site.site_id, site.campground_id, site.max_occupancy, site.max_rv_length, " +
                        $"site.site_number, site.accessible, site.utilities, campground.daily_fee FROM site Join campground on campground.campground_id = @userCampgroundId Join reservation on reservation.site_id = site.site_id where site.campground_id =" +
                        $" @userCampgroundId and from_date not Between @startDate and @endDate and to_date not Between @startDate and @endDate and reservation.site_id " +
                        $"not in (select site_id from reservation where from_date Between @startDate and @endDate and to_date Between @startDate and @endDate) group by" +
                        $" site.site_id, site.campground_id, site.max_occupancy, site.max_rv_length, site.site_number, site.accessible, site.utilities , campground.daily_fee", conn);
                    cmd.Parameters.AddWithValue("@userCampgroundId", userCampgroundId);
                    cmd.Parameters.AddWithValue("@startDate", startDate.Date);
                    cmd.Parameters.AddWithValue("@endDate", endDate.Date);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Site site = ConvertReaderToSite(reader);
                        sites.Add(site);
                    }
                    return sites;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Sorry, a SQL error has occured.");
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private Site ConvertReaderToSite(SqlDataReader reader)
        {
            Site site = new Site();
            site.SiteID = Convert.ToInt32(reader["site_id"]);
            site.CampgroundId = Convert.ToInt32(reader["campground_id"]);
            site.SiteNumber = Convert.ToInt32(reader["site_number"]);
            site.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
            site.Accessible = Convert.ToInt32(reader["accessible"]);
            site.MaxRvLength = Convert.ToInt32(reader["max_rv_length"]);
            site.Utilities = Convert.ToInt32(reader["utilities"]);
            site.Cost = Convert.ToInt32(reader["daily_fee"]);

            return site;
        }


    }
}
