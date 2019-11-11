using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    interface IReservationSqlDAO
    {
        //method to place reservation
        int PlaceReservation(int reservationSiteId, string reservationName, DateTime reservationStartDate, DateTime reservationEndDate);
        //method to convert the SQL data being read in from the Reservation table
        Reservation ConvertReaderToReservation(SqlDataReader reader);
    }
}
