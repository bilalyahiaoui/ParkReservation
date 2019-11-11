using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DAL
{
    public class ReservationSqlDAO
    {
        private string connectionString;
        public ReservationSqlDAO(string ConnectionString)
        {
            connectionString = ConnectionString;
        }

        public int PlaceReservation(int reservationSiteId, string reservationName, DateTime reservationStartDate, DateTime reservationEndDate)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand($"INSERT INTO reservation VALUES (@site_id, @name, @from_date, @to_date, @currentDateTime);select scope_identity();", conn);
                    cmd.Parameters.AddWithValue("@site_id", reservationSiteId);
                    cmd.Parameters.AddWithValue("@name", reservationName);
                    cmd.Parameters.AddWithValue("@from_date", reservationStartDate);
                    cmd.Parameters.AddWithValue("@to_date", reservationEndDate);
                    cmd.Parameters.AddWithValue("@currentDateTime", DateTime.Now);

                    int reservationIdConfirmation = Convert.ToInt32(cmd.ExecuteScalar());
                    return reservationIdConfirmation;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Sorry, a SQL error has occured.");
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private Reservation ConvertReaderToReservation(SqlDataReader reader)
        {
            Reservation reservation = new Reservation();
            reservation.ReservationId = Convert.ToInt32(reader["reservation_id"]);
            reservation.SiteId = Convert.ToInt32(reader["site_id"]);
            reservation.Name = Convert.ToString(reader["name"]);
            reservation.FromDate = Convert.ToDateTime(reader["from_date"]);
            reservation.ToDate = Convert.ToDateTime(reader["to_date"]);
            reservation.CreateDate = Convert.ToDateTime(reader["create_date"]);

            return reservation;
        }
    }
}
